

using Domain.Entities;
using Domain.Interfaces;
using MongoDB.Driver;

namespace Infrastructure.Repositories;

public class MongoChatRepository(IMongoDatabase database) : IChatRepository
{
    private readonly IMongoCollection<ChatMessage> _collection = database.GetCollection<ChatMessage>("Messages");

    public async Task AddAsync(ChatMessage message)
        => await _collection.InsertOneAsync(message);

    public async Task<IEnumerable<ChatMessage>> GetHistoryAsync(string userId)
        => await _collection.Find(m => m.UserId == userId)
            .SortBy(m => m.CreatedAt)
            .ToListAsync();
    public async Task MarkAsReadAsync(string userId)
    {
        var filter = Builders<ChatMessage>.Filter.And(
            Builders<ChatMessage>.Filter.Eq(m => m.UserId, userId),
            Builders<ChatMessage>.Filter.Eq(m => m.IsRead, false)
        );

        var update = Builders<ChatMessage>.Update
            .Set(m => m.IsRead, true);

        await _collection.UpdateManyAsync(filter, update);
    }
    public async Task<IEnumerable<ChatMessage>> GetUnreadAsync()
    {
        return await _collection
            .Find(m => !m.IsRead)
            .ToListAsync();
    }
}