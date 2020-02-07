using System.Linq;
using SportsStore.AppCore.Entities;
using SportsStore.AppCore.Interfaces;

namespace SportsStore.Infrastructure.Data
{
    public class EFProductRepository : IProductRepository
    {
        private readonly AppDbContext _dbContext;

        public IQueryable<Product> Products => _dbContext.Products;

        public EFProductRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void SaveProduct(Product product)
        {
            if (product.ProductID > 0)
            {
                var existingProduct = _dbContext.Products.FirstOrDefault(p => p.ProductID == product.ProductID);

                if (existingProduct != null)
                {
                    existingProduct.Name = product.Name;
                    existingProduct.Description = product.Description;
                    existingProduct.Price = product.Price;
                    existingProduct.Category = product.Category;
                }
            }
            else
            {
                _dbContext.Products.Add(product);
            }

            _dbContext.SaveChanges();
        }

        public Product DeleteProduct(int productId)
        {
            var product = _dbContext.Products.FirstOrDefault(p => p.ProductID == productId);

            if (product != null)
            {
                _dbContext.Products.Remove(product);

                _dbContext.SaveChanges();
            }

            return product;
        }
    }
}
