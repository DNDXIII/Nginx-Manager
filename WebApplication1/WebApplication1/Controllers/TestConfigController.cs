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
            pi.Arguments = @"/C docker run -v " + filePath + @":/etc/nginx/nginx.conf -P -it imkulikov/nginx-sticky nginx -t";
            Console.WriteLine(pi.Arguments);
            pi.RedirectStandardOutput = true;
            p.StartInfo = pi;
            p.Start();


            var s  = p.StandardOutput.ReadToEnd();

            p.WaitForExit();


            //check if it was a positive response
            if (p.ExitCode!=0)
                return BadRequest(s);
            return Ok(s);
        }

        [EnableCors("Cors")]
        [HttpGet("deploy")]
        public IActionResult DeployConfig()
        {
            string host = "192.1239.21.2";
            int port =22;
            string username= "pi";
            string password= "ilikepie";
            string filePath = @"D:\nb23160\Downloads\configurationTest.conf";

            //create the file
            System.IO.File.WriteAllText(filePath, _allRep.GeneralConfigRep.GetAll().First().GenerateConfig(_allRep));


            //upload the file
            using (var sftp = new SftpClient(host, port, username, password))
            {

                sftp.Connect();
                sftp.ChangeDirectory("/etc/nginx");
                using (var uplfileStream = System.IO.File.OpenRead(filePath))
                {
                    //rename the previous file
                    sftp.UploadFile(uplfileStream, filePath, true);
   
                }
                sftp.Disconnect();
                
            }

            //test the file again
            using (var sshclient = new SshClient(host, port, username, password))
            {
                sshclient.Connect();
                using (var cmd = sshclient.CreateCommand(@"nginx -t -c etc/nginx/nginxTest.conf"))
                {
                    cmd.Execute();

                    //if the test was unsuccessful restore the previous file
                    if (cmd.ExitStatus != 0)
                    {
                        //remover esta
                        return BadRequest(cmd.CommandText);
                    }
                    else
                    {
                        return Ok(cmd.CommandText);

                    }

                   

                }
                sshclient.Disconnect();
            }

            return Ok();
        }
        

    }
}