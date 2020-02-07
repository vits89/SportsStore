using System.Collections.Generic;

namespace SportsStore.WebMvcApp.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Zip { get; set; }

        public IEnumerable<CartLineViewModel> Lines { get; set; }
    }
}
