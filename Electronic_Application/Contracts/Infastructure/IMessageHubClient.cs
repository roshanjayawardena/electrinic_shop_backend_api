using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Electronic_Application.Contracts.Infastructure
{
    public interface IMessageHubClient
    {
        Task SendOffersToUser(List<string> message);
        Task SendOrderApprovalStatus(string userId, string message);
    }
}
