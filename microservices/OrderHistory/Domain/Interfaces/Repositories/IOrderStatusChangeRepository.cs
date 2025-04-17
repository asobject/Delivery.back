

using BuildingBlocks.Interfaces.Repositories;
using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IOrderStatusChangeRepository : IRepository<OrderStatusChange>
{
    Task<IEnumerable<OrderStatusChange>> GetStatusChangesAsync(int orderId);
}
