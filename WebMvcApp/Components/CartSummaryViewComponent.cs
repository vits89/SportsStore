using Microsoft.AspNetCore.Mvc;
using SportsStore.AppCore.Interfaces;

namespace SportsStore.WebMvcApp.Components
{
    public class CartSummaryViewComponent : ViewComponent
    {
        private readonly ICartService _cartService;

        public CartSummaryViewComponent(ICartService cartService)
        {
            _cartService = cartService;
        }

        public IViewComponentResult Invoke()
        {
            return View(_cartService);
        }
    }
}
