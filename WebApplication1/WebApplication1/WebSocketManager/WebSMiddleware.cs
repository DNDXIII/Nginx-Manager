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

        WebSocket currentSocket = await context.WebSockets.AcceptWebSocketAsync();
        var socketId = Guid.NewGuid().ToString();

        _sockets.TryAdd(socketId, currentSocket);

        await Receive(currentSocket, async (result, buffer) =>
        {
            if (result.MessageType == WebSocketMessageType.Text)
            {
                ReceiveAsync(currentSocket, result, buffer);
                return;
            }

            else if (result.MessageType == WebSocketMessageType.Close)
            {
                await CloseSocket(currentSocket);
                return;
            }

        });

    }

    private async Task Receive(WebSocket socket, Action<WebSocketReceiveResult, byte[]> handleMessage)
    {
        var buffer = new byte[1024 * 4];

        while (socket.State == WebSocketState.Open)
        {
            var result = await socket.ReceiveAsync(buffer: new ArraySegment<byte>(buffer),
                                                   cancellationToken: CancellationToken.None);

            handleMessage(result, buffer);
        }
    }

    private string GetId(WebSocket socket)
    {
        return _sockets.FirstOrDefault(p => p.Value == socket).Key;
    }

    public void ReceiveAsync(WebSocket socket, WebSocketReceiveResult result, byte[] buffer)
    {
        string msg = Encoding.UTF8.GetString(buffer, 0, result.Count);

        SendStringAsync(msg, socket, new CancellationToken());
    }

    private async static void SendStringAsync(string data, WebSocket socket, CancellationToken ct = default(CancellationToken))
    {
        var buffer = Encoding.UTF8.GetBytes(data);
        var segment = new ArraySegment<byte>(buffer);

        await socket.SendAsync(segment, WebSocketMessageType.Text, true, ct);


    }

    private async Task CloseSocket(WebSocket socket)
    {
        await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", new CancellationToken());
        socket.Dispose();
        WebSocket dummy;
        _sockets.TryRemove(GetId(socket), out dummy);
    }

    private async Task deployConfigAsync(WebSocket socket)
    {
        var deploymentServers = _allRep.DeploymentServerRep.GetAll().Where(x => x.Active);

        if (deploymentServers.Count() == 0)
        {
            SendStringAsync("No servers to deploy to.", socket);
            await CloseSocket(socket);
            return;
        }

        var filePath = Path.GetTempFileName();
        string username = "azureuser";
        string password = "Collab.1234567890";


        //create the file
        SendStringAsync("Uploading configuration file.", socket);
        File.WriteAllText(filePath, _allRep.GeneralConfigRep.GetAll().First().GenerateConfig(_allRep));


        //create backup
        foreach (DeploymentServer d in deploymentServers)
        {
            using (var ssh = new SshClient(d.Address, d.Port, username, password))
            {
                ssh.Connect();
                if (!createBackup(ssh))
                {
                    SendStringAsync("Error creating backup on " + d.Name, socket);
                    await CloseSocket(socket);
                    return;
                }
                ssh.Disconnect();
                SendStringAsync("Backup created on " + d.Name, socket);
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
                SendStringAsync("Uploaded file to " + d.Name, socket);
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
                    SendStringAsync("Error occurred while trying to restart " + d.Name + " with the new configuration", socket);


                    cmd = sshclient.CreateCommand(@"sudo systemctl status nginx.service");
                    cmd.Execute();
                    SendStringAsync("Error occurred on " + d.Name + " read console for more details. " + cmd.Result, socket);


                    //restore 
                    foreach (DeploymentServer dd in deploymentServers)
                    {
                        using (var ssh = new SshClient(dd.Address, dd.Port, username, password))
                        {
                            ssh.Connect();
                            restoreBackup(ssh);
                            ssh.Disconnect();
                        }
                        SendStringAsync(dd.Name + " has been reverted.", socket);
                    }
                }
                else
                {
                    sshclient.Disconnect();
                    SendStringAsync(d.Name + " has been successfully restarted with the new configuration.", socket);
                }
            }
        }
        if (errorOcurred)
            SendStringAsync("The new configuration file could not be deployed.", socket);
        else
            SendStringAsync("The new configuration file has beens successfully deployed.", socket);

        await CloseSocket(socket);

        

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
