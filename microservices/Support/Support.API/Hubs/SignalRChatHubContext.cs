using Domain.Interfaces.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Support.API.Hubs;

public class SignalRChatHubContext(IHubContext<ChatHub> hubContext) : IChatHubContext
{
    public Task SendToUserGroup(string userId, string method, object message)
        => hubContext.Clients.Group($"user_{userId}").SendAsync(method, message);

    public Task SendToOperators(string method, object message)
        => hubContext.Clients.Group("Operators").SendAsync(method, message);
    public Task NotifyMessageRead(string userId, DateTime timestamp)
        => hubContext.Clients.Group($"user_{userId}").SendAsync("MessagesRead", timestamp);
}