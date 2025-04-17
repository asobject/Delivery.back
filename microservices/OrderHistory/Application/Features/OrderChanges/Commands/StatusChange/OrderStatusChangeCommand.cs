using BuildingBlocks.Enums;
using MediatR;

namespace Application.Features.OrderChanges.Commands.StatusChange;

public record OrderStatusChangeCommand(int OrderId, OrderStatus Status) : IRequest<OrderStatusChangeResponse>;
