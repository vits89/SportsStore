using System.Linq;
using SportsStore.AppCore.Entities;

namespace SportsStore.AppCore.Interfaces
{
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }

        void SaveProduct(Product product);
        Product DeleteProduct(int productId);
    }
}
