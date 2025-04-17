using MediatR;

namespace Application.Features.OrderChanges.Queries.PointChange;

public record OrderPointChangeQuery(int OrderId) : IRequest<OrderPointChangeResponse>;