using System.Collections.Generic;
using System.Linq;
using SportsStore.AppCore.Entities;
using SportsStore.AppCore.Interfaces;

namespace SportsStore.AppCore.Services
{
    public class CartService : ICartService
    {
        public List<CartLine> Lines { get; set; } = new List<CartLine>();

        public virtual void AddItem(Product product, int quantity = 1)
        {
            var line = Lines.Where(p => p.Product.ProductID == product.ProductID).FirstOrDefault();

            if (line != null)
            {
                line.Quantity += quantity;
            }
            else
            {
                Lines.Add(new CartLine
                {
                    Product = product,
                    Quantity = quantity
                });
            }
        }

        public virtual void RemoveLine(Product product)
        {
            Lines.RemoveAll(l => l.Product.ProductID == product.ProductID);
        }

        public virtual void Clear()
        {
            Lines.Clear();
        }

        public virtual decimal ComputeTotalValue()
        {
            return Lines.Sum(l => l.Product.Price * l.Quantity);
        }
    }
}
