using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Electronic_Infastructure.SignalRHub
{
    //public class MessageHub : Hub<IMessageHubClient>
    //{
    //    public async Task SendOffersToUser(List<string> message)
    //    {
    //        await Clients.All.SendOffersToUser(message);
    //    }

    //    public async Task SendOrderApprovalStatus(string userId, string message)
    //    {
    //        await Clients.User(userId).SendOrderApprovalStatus(userId, message);

    //    }
    //}
    
    public class MessageHub : Hub
    {
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task SendOffersToUser(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
       
        public async Task SendOrderApprovalStatus(string userId, string message)
        {
            await Clients.User(userId).SendAsync("ReceiveUserMessage", message);

        }
    }

}
