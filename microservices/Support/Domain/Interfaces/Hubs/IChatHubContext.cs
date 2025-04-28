

namespace Domain.Interfaces.Hubs;

public interface IChatHubContext
{
    Task SendToUserGroup(string userId, string method, object message);
    Task SendToOperators(string method, object message);
    Task NotifyMessageRead(string userId, DateTime timestamp);
}