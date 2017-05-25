using System;
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
    public class TestConfigController : Controller
    {   
        private readonly AllRepositories _allRep;
        public TestConfigController(AllRepositories allRep)
        {
            _allRep = allRep;
        }

        [EnableCors("Cors")]
        [HttpGet("download")]
        public void Get()
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
            IActionResult ret;
            string host = "52.233.133.109";
            int port = 50001;
            string username = "azureuser";
            string password = "Collab.1234567890";
            var filePath = Path.GetTempFileName();
            //create the file


            /*TODO TEMP , ALTERAR !!!!!! apenas pq nao tenho ips como deve ser
            StringBuilder config = new StringBuilder(_allRep.GeneralConfigRep.GetAll().First().GenerateConfig(_allRep));
            foreach (var sv in _allRep.ServerRep.GetAll())
                config.Replace(sv.Address, "127.0.0.1");
            System.IO.File.WriteAllText(filePath, config.ToString());
            TODO TEMP, ALTERAR!!!!*/

            System.IO.File.WriteAllText(filePath, _allRep.GeneralConfigRep.GetAll().First().GenerateConfig(_allRep));

            //upload the file
            using (var sftp = new SftpClient(host, port, username, password))
            {
                sftp.Connect();
                using (var uplfileStream = System.IO.File.OpenRead(filePath))
                {
                    sftp.UploadFile(uplfileStream, "nginxTest.conf", true);
                }
                sftp.Disconnect();

            }

            //test the file again
            using (var sshclient = new SshClient(host, port, username, password))
            {
                sshclient.Connect();

                if (!createBackup(sshclient))
                    return StatusCode(500, "Could not create a backup");


                using (var cmd = sshclient.CreateCommand(@"sudo nginx -t -c  /home/azureuser/nginxTest.conf"))
                {
                    var s = cmd.Execute();

                    //if the test was unsuccessful remove the test file
                    if (cmd.ExitStatus != 0)
                    {
                        sshclient.CreateCommand(@"sudo rm /home/azureuser/nginxTest.conf").Execute();
                        ret = StatusCode(500, new StreamReader(cmd.ExtendedOutputStream).ReadToEnd());
                    }
                    //if it was successfull try to replace the actual file and reload
                    else
                    {
                        sshclient.CreateCommand(@"sudo mv /home/azureuser/nginxTest.conf /etc/nginx/nginx.conf").Execute();
                        var reload = sshclient.CreateCommand(@"sudo systemctl reload nginx");
                        reload.Execute();
                        if (reload.ExitStatus != 0)
                        {
                            //go back 
                            restoreBackup(sshclient);
                            ret = StatusCode(500, new StreamReader(reload.ExtendedOutputStream).ReadToEnd());
                        }
                        else
                            ret = Ok(new StreamReader(reload.ExtendedOutputStream).ReadToEnd());
                    }
                }
                sshclient.Disconnect();
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