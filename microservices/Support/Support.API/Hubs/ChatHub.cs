using Microsoft.AspNetCore.SignalR;

namespace Support.API.Hubs;

public class ChatHub : Hub
{
    public async Task JoinUserGroup(string userId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, $"user_{userId}");
    }

    public async Task JoinOperatorsGroup()
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, "Operators");
    }
}