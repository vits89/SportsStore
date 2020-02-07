using SportsStore.AppCore.Interfaces;

namespace SportsStore.WebMvcApp.ViewModels
{
    public class CartViewModel
    {
        public ICartService Cart { get; set; }
        public string ReturnUrl { get; set; }
    }
}
