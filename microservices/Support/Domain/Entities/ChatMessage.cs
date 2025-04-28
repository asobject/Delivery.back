using BuildingBlocks.Interfaces.Entities;

namespace Domain.Entities;

public class ChatMessage : IEntity
{
    public string UserId { get; set; } = null!;
    public string Text { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public bool IsRead { get; set; }
    public bool IsClientResponse { get; set; }
    public int Id { get; set; }
}