using System.Collections.Generic;

namespace SportsStore.ViewModels
{
    public class ProductsViewModel
    {
        public IEnumerable<ProductViewModel> Products { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
    }
}
