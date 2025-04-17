

using BuildingBlocks.Enums;

namespace BuildingBlocks.Messaging.Events;

public record OrderStatusChangeEvent(int OrderId,OrderStatus Status);