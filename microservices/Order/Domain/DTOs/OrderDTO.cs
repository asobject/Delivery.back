

using BuildingBlocks.Enums;

namespace Domain.DTOs;

public record OrderDTO(int OrderId, Guid Tracker,int? CurrentPointId,string SenderAddress,string ReceiverAddress,OrderStatus Status);