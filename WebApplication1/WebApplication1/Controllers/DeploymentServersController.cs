using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Renci.SshNet;
using System;
using WebApplication1.DataAccess;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{

    [Route("api/deploymentservers")]
    public class DeploymentServersController : AbstractController<DeploymentServer>
    {
        private AllRepositories _allRep;
        public DeploymentServersController(IRepository<DeploymentServer> repository, AllRepositories allrep) : base(repository)
        {
            _allRep = allrep;
         }

        [HttpPost("reload/{id}")]
        public IActionResult Restart(string id)
        {
            return ServerAction(id, "sudo systemctl reload nginx");
        }

        [HttpPost("start/{id}")]
        public IActionResult Start(string id)
        {
            return ServerAction(id, "sudo systemctl start nginx");

        }

        [HttpPost("shutdown/{id}")]
        public IActionResult Shutdown(string id)
        {
            return ServerAction(id, "sudo systemctl stop nginx");
        }


        //to avoid code repetition
        private IActionResult ServerAction(string id, string op)
        {
            var server = _allRep.DeploymentServerRep.GetById(id);
            using (var client = new SshClient(server.Address, server.Port, "azureuser", "Collab.1234567890"))
            {
                client.Connect();
                var cmd = client.CreateCommand(op);
                cmd.Execute();

                if (cmd.ExitStatus == 0)
                    return Ok();

                cmd = client.CreateCommand(@"sudo systemctl status nginx.service");
                cmd.Execute();
                client.Disconnect();

                return StatusCode(500, cmd.Result);
            }
        }
    }
}
