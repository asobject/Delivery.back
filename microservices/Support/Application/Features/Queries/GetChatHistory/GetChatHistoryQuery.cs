
using MediatR;

namespace Application.Features.Queries.GetChatHistory;

public record GetChatHistoryQuery(string UserId) : IRequest<GetChatHistoryResponse>;
