

using BuildingBlocks.Enums;

namespace Domain.DTOs;

public record OrderStatusChangeDTO(Guid Tracker,OrderStatus Status, DateTime ChangeAt);