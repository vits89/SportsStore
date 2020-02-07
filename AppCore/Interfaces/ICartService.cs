using System.Collections.Generic;
using SportsStore.AppCore.Entities;

namespace SportsStore.AppCore.Interfaces
{
    public interface ICartService
    {
        List<CartLine> Lines { get; set; }

        void AddItem(Product product, int quantity = 1);
        void RemoveLine(Product product);
        void Clear();
        decimal ComputeTotalValue();
    }
}
