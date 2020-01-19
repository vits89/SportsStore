using System.Collections.Generic;
using SportsStore.Models;

namespace SportsStore.ViewModels
{
    public class ProductsViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
    }
}
