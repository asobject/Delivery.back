

using BuildingBlocks.Enums;

namespace Domain.DTOs;

public record OrderStatusChangeDTO(int OrderId,OrderStatus Status, DateTime ChangeAt);