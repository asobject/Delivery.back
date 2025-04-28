

using Domain.DTOs;

namespace Application.Features.Queries.GetChatHistory;

public record GetChatHistoryResponse(IEnumerable<ChatMessageDTO> Messages);