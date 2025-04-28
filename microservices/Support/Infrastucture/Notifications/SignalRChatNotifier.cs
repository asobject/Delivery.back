

using Domain.Entities;
using Domain.Interfaces.Hubs;
using Domain.Interfaces.Notifications;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.Notifications;

public class SignalRChatNotifier(IChatHubContext hubContext) : IChatNotifier
{
    public async Task NotifyNewMessage(ChatMessage message)
    {
        if (message.IsClientResponse)
            await hubContext.SendToUserGroup(message.UserId, "ReceiveMessage", message);
        else
            await hubContext.SendToOperators("NewMessage", message);
    }

    public async Task NotifyMessageRead(string userId)
        => await hubContext.NotifyMessageRead(userId, DateTime.UtcNow);
}