using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace AureliaSignalRTest.Hubs {
    public class ItemsHub : Hub {
        public async Task Update() {
            await Clients.All.SendAsync("Update");
        }

        public override Task OnConnectedAsync() {
            return base.OnConnectedAsync();
        }
    }
}