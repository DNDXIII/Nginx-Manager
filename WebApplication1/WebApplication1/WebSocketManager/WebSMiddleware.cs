using Microsoft.AspNetCore.Http;
using Renci.SshNet;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApplication1.DataAccess;
using WebApplication1.Models;

public class WebSMiddleware
{
    private static ConcurrentDictionary<string, WebSocket> _sockets = new ConcurrentDictionary<string, WebSocket>();

    private readonly RequestDelegate _next;
    private readonly AllRepositories _allRep;
    public WebSMiddleware(RequestDelegate next, AllRepositories allRep)
    {
        _allRep = allRep;
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        if (!context.WebSockets.IsWebSocketRequest)
        {
            await _next.Invoke(context);
            return;
        }

        CancellationToken ct = context.RequestAborted;
        WebSocket currentSocket = await context.WebSockets.AcceptWebSocketAsync();
        var socketId = Guid.NewGuid().ToString();

        _sockets.TryAdd(socketId, currentSocket);


        deployConfig();

        WebSocket dummy;
        _sockets.TryRemove(socketId, out dummy);

        await currentSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", ct);
        currentSocket.Dispose();
    }

    private async static void SendStringAsync(string data, CancellationToken ct = default(CancellationToken))
    {
        var buffer = Encoding.UTF8.GetBytes(data);
        var segment = new ArraySegment<byte>(buffer);

        foreach (var s in _sockets)
        {
            if (s.Value.State != WebSocketState.Open)
            {
                continue;
            }

            await s.Value.SendAsync(segment, WebSocketMessageType.Text, true, ct);
        }

    }

    private void deployConfig()
    {
        var deploymentServers = _allRep.DeploymentServerRep.GetAll().Where(x => x.Active);

        var filePath = Path.GetTempFileName();
        string username = "azureuser";
        string password = "Collab.1234567890";


        //create the file
        SendStringAsync("Uploading configuration file.");
        File.WriteAllText(filePath, _allRep.GeneralConfigRep.GetAll().First().GenerateConfig(_allRep));


        //create backup
        foreach (DeploymentServer d in deploymentServers)
        {
            using (var ssh = new SshClient(d.Address, d.Port, username, password))
            {
                ssh.Connect();
                if (!createBackup(ssh))
                {
                    SendStringAsync("Error creating backup on " + d.Name );
                    return;
                }
                ssh.Disconnect();
                SendStringAsync("Backup created on " + d.Name);
            }

        }

        var errorOcurred = false;
        //upload the file and deploy
        foreach (DeploymentServer d in deploymentServers)
        {
            if (errorOcurred)
                break;

            using (var sftp = new SftpClient(d.Address, d.Port, username, password))
            {
                sftp.Connect();
                sftp.ChangeDirectory(@"/tmp");
                using (var uplfileStream = File.OpenRead(filePath))
                {
                    sftp.UploadFile(uplfileStream, "nginx.conf", true);
                }
                sftp.Disconnect();
                SendStringAsync("Uploaded file to " + d.Name);
            }

            //test the file again inside each server to deploy to
            using (var sshclient = new SshClient(d.Address, d.Port, username, password))
            {
                sshclient.Connect();
                sshclient.CreateCommand(@"sudo mv /tmp/nginx.conf /etc/nginx/nginx.conf").Execute();
                var cmd = sshclient.CreateCommand(@"sudo systemctl reload nginx");
                cmd.Execute();
                if (cmd.ExitStatus != 0)
                {
                    errorOcurred = true;
                    SendStringAsync("Error occurred while trying to restart " + d.Name + " with the new configuration");
                        
                    
                    cmd = sshclient.CreateCommand(@"sudo systemctl status nginx.service");
                    cmd.Execute();
                    SendStringAsync("Error occurred on "+d.Name+ " read console for more details. " + cmd.Result);
                    
    
                    //restore 
                    foreach (DeploymentServer dd in deploymentServers)
                    {
                        using (var ssh = new SshClient(dd.Address, dd.Port, username, password))
                        {
                            ssh.Connect();
                            restoreBackup(ssh);
                            ssh.Disconnect();
                        }
                        SendStringAsync(dd.Name + " has been reverted.");
                    }
                }
                else
                {
                    sshclient.Disconnect();
                    SendStringAsync(d.Name + " has been successfully restarted with the new configuration.");
                }
            }
        }
        if(errorOcurred)
            SendStringAsync("The new configuration file could not be deployed.");
        else
            SendStringAsync("The new configuration file has beens successfully deployed.");
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
