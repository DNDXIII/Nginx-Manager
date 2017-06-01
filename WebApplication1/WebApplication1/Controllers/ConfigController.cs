﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.DataAccess;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Cors;
using System.Diagnostics;
using Renci.SshNet;

namespace WebApplication1.Controllers
{
    [Route("api/config")]
    public class ConfigController : Controller
    {   
        private readonly AllRepositories _allRep;
        public ConfigController(AllRepositories allRep)
        {
            _allRep = allRep;
        }

        [EnableCors("Cors")]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new NGINXConfig().GenerateConfig(_allRep));
        }

        [EnableCors("Cors")]
        [HttpGet("download")]
        public void Download()
        {
            var config = new NGINXConfig();
            Response.ContentType = "text/plain";
            Response.Headers.Append("Content-Description", "File Transfer");
            Response.Headers.Append("Content-Disposition", "attachment; filename=configuration.conf");
            Response.WriteAsync(config.GenerateConfig(_allRep));
        }


        [EnableCors("Cors")]
        [HttpGet("test")]
        public IActionResult TestConfig()
        {
            //creates the text file
            StringBuilder config = new StringBuilder(_allRep.GeneralConfigRep.GetAll().First().GenerateConfig(_allRep));

            foreach (var sv in _allRep.ServerRep.GetAll())
                config.Replace(sv.Address, "127.0.0.1");

            var filePath = Path.GetTempFileName();
            System.IO.File.WriteAllText(filePath, config.ToString());



            //tests the file and writes the output 
            Process p = new Process();
            ProcessStartInfo pi = new ProcessStartInfo();
            pi.FileName = "cmd.exe";
            pi.Arguments = @"/C docker run -v " + filePath + @":/etc/nginx/nginx.conf -P -it nginx-image nginx -t";
            pi.RedirectStandardOutput = true;
            p.StartInfo = pi;
            p.Start();


            var s  = p.StandardOutput.ReadToEnd();

            p.WaitForExit();


            //check if it was a positive response
            if (p.ExitCode!=0)
                return StatusCode(500, s);
            return Ok(s);
        }

        [EnableCors("Cors")]
        [HttpGet("deploy")]
        public IActionResult DeployConfig()
        {
            IActionResult ret = StatusCode(500,"No servers to deploy to.");

            var deploymentServers = _allRep.DeploymentServerRep.GetAll().Where(x => x.Active);

            var filePath = Path.GetTempFileName();
            string username = "azureuser";
            string password = "Collab.1234567890";

           
            //create the file
            System.IO.File.WriteAllText(filePath, _allRep.GeneralConfigRep.GetAll().First().GenerateConfig(_allRep));


            //create backups
            foreach(DeploymentServer d in deploymentServers)
            {
                using (var ssh = new SshClient(d.Address, d.Port, username, password))
                {
                    ssh.Connect();
                    if(!createBackup(ssh))
                        return StatusCode(500, "Could not create a backup!");
                    ssh.Disconnect();
                }
                
            }

            //upload the file and deploy
            foreach (DeploymentServer d in deploymentServers)
            {
                using (var sftp = new SftpClient(d.Address, d.Port, username, password))
                {
                    sftp.Connect();
                    using (var uplfileStream = System.IO.File.OpenRead(filePath))
                    {
                        sftp.UploadFile(uplfileStream, "nginxTest.conf", true);
                    }
                    sftp.Disconnect();
                }

                //test the file again inside each server to deploy to
                using (var sshclient = new SshClient(d.Address, d.Port, username, password))
                {
                    sshclient.Connect();
                    sshclient.CreateCommand(@"sudo mv /home/azureuser/nginxTest.conf /etc/nginx/nginx.conf").Execute();
                    var cmd = sshclient.CreateCommand(@"sudo systemctl reload nginx");
                    cmd.Execute();
                    if (cmd.ExitStatus != 0)
                    {
                        //restore 
                        foreach (DeploymentServer dd in deploymentServers)
                        {
                            using (var ssh = new SshClient(dd.Address, dd.Port, username, password))
                            {
                                ssh.Connect();
                                restoreBackup(ssh);
                                ssh.Disconnect();
                            }
                        }
                        return StatusCode(500,"Error on " + d.Name+" :" + new StreamReader(cmd.ExtendedOutputStream).ReadToEnd());
                    }
                    else
                    {
                        sshclient.Disconnect();
                        ret= Ok(new StreamReader(cmd.ExtendedOutputStream).ReadToEnd());
                    }       
                }
            }
            return ret;
        }

        private bool createBackup(SshClient sshclient)
        {
            var cmd = sshclient.CreateCommand(@"sudo rm -rf /etc/nginx/backup && sudo mkdir /etc/nginx/backup && sudo cp /etc/nginx/nginx.conf /etc/nginx/backup");
            cmd.Execute();
            if (cmd.ExitStatus != 0)//creating backup failed
                return false;
            return true;
        }

        private void restoreBackup(SshClient sshclient)
        {
            sshclient.CreateCommand(@"sudo cp /etc/nginx/backup/nginx.conf /etc/nginx/nginx.conf").Execute();
        }
    }
}