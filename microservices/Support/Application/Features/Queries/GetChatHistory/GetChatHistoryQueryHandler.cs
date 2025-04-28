
using Domain.DTOs;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Queries.GetChatHistory;

public class GetChatHistoryHandler(IChatRepository repo) : IRequestHandler<GetChatHistoryQuery, GetChatHistoryResponse>
{
    public async Task<GetChatHistoryResponse> Handle(
        GetChatHistoryQuery request,
        CancellationToken cancellationToken)
    {
        var history = await repo.GetHistoryAsync(request.UserId);
        var historyDto = history.Select(msg => new ChatMessageDTO(
     Text: msg.Text,
     CreatedAt: msg.CreatedAt,
     IsRead: msg.IsRead,
     IsClientResponse: msg.IsClientResponse
 ));
        return new GetChatHistoryResponse(historyDto);
    }
       
}