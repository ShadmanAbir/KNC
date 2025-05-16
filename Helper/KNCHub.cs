using Microsoft.AspNet.SignalR;

public class KNCHub : Hub
{
    public void Send(string name, string message)
    {
        Clients.All.broadcastMessage(name, message);
    }
}