using MediatR;

namespace Application.Features.OrderChanges.Commands.PointChange;

public record OrderPointChangeCommand(int OrderId, int PointId) : IRequest<OrderPointChangeResponse>;
