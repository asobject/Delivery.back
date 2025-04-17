

using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class OrderStatusChangeRepository(ApplicationDbContext context) : Repository<OrderStatusChange>(context), IOrderStatusChangeRepository
{
    public async Task<IEnumerable<OrderStatusChange>> GetStatusChangesAsync(int orderId)
    {
        return await _dbSet.AsNoTracking().Where(o => o.OrderId == orderId).ToListAsync();
    }
}
