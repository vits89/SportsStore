using System.Linq;
using Microsoft.EntityFrameworkCore;
using SportsStore.AppCore.Entities;
using SportsStore.AppCore.Interfaces;

namespace SportsStore.Infrastructure.Data
{
    public class EFOrderRepository : IOrderRepository
    {
        private readonly AppDbContext _dbContext;

        public IQueryable<Order> Orders => _dbContext.Orders.Include(o => o.Lines).ThenInclude(l => l.Product);

        public EFOrderRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void SaveOrder(Order order)
        {
            _dbContext.AttachRange(order.Lines.Select(l => l.Product));

            if (order.OrderID == 0)
            {
                _dbContext.Orders.Add(order);
            }

            _dbContext.SaveChanges();
        }
    }
}
