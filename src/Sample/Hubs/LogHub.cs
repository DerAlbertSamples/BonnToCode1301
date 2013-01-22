using Microsoft.AspNet.SignalR;

namespace Sample.Hubs
{
       [Microsoft.AspNet.SignalR.Hubs.HubName("log")]
    public class LogHub : Hub
    {
           public void AddMessage(string message)
           {
               this.Clients.All.addMessage(message);
           }

    }
}