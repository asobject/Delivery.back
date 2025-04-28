

using Domain.Entities;

namespace Domain.Interfaces;

public interface IChatRepository
{
    Task AddAsync(ChatMessage message);
    Task<IEnumerable<ChatMessage>> GetHistoryAsync(string userId);
    Task<IEnumerable<ChatMessage>> GetUnreadAsync();
    Task MarkAsReadAsync(string userId);
}