using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SportsStore.AppCore.Interfaces;
using SportsStore.WebMvcApp.ViewModels;

namespace SportsStore.WebMvcApp.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductRepository _repository;
        private readonly ICartService _cartService;

        public CartController(IProductRepository repository, ICartService cartService)
        {
            _repository = repository;
            _cartService = cartService;
        }

        public ActionResult Index(string returnUrl)
        {
            return View(new CartViewModel
            {
                Cart = _cartService,
                ReturnUrl = returnUrl
            });
        }

        public ActionResult AddToCart(int productId, string returnUrl)
        {
            var product = _repository.Products.FirstOrDefault(p => p.ProductID == productId);

            if (product != null)
            {
                _cartService.AddItem(product);
            }

            return RedirectToAction(nameof(Index), new { returnUrl });
        }

        public ActionResult RemoveFromCart(int productId, string returnUrl)
        {
            var product = _repository.Products.FirstOrDefault(p => p.ProductID == productId);

            if (product != null)
            {
                _cartService.RemoveLine(product);
            }

            return RedirectToAction(nameof(Index), new { returnUrl });
        }
    }
}
