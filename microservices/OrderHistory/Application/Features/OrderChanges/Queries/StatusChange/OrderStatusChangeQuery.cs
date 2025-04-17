using MediatR;

namespace Application.Features.OrderChanges.Queries.StatusChange;

public record OrderStatusChangeQuery(int OrderId) : IRequest<OrderStatusChangeResponse>;