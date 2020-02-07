using System.Linq;
using SportsStore.AppCore.Entities;

namespace SportsStore.AppCore.Interfaces
{
    public interface IOrderRepository
    {
        IQueryable<Order> Orders { get; }

        void SaveOrder(Order order);
    }
}
