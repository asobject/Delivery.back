
using Domain.Entities;

namespace Domain.Interfaces.Notifications;

public interface IChatNotifier
{
    Task NotifyNewMessage(ChatMessage message);
    Task NotifyMessageRead(string userId);
}
