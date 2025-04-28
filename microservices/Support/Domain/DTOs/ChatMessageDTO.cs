
namespace Domain.DTOs;

public record ChatMessageDTO(string Text, DateTime CreatedAt, bool IsRead, bool IsClientResponse);