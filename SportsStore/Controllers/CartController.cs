using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.ViewModels;

namespace SportsStore.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductRepository _repository;
        private readonly Cart _cart;

        public CartController(IProductRepository repository, Cart cart)
        {
            _repository = repository;
            _cart = cart;
        }

        public ActionResult Index(string returnUrl)
        {
            return View(new CartViewModel
            {
                Cart = _cart,
                ReturnUrl = returnUrl
            });
        }

        public ActionResult AddToCart(int productId, string returnUrl)
        {
            var product = _repository.Products.FirstOrDefault(p => p.ProductID == productId);

            if (product != null)
            {
                _cart.AddItem(product);
            }

            return RedirectToAction(nameof(Index), new { returnUrl });
        }

        public ActionResult RemoveFromCart(int productId, string returnUrl)
        {
            var product = _repository.Products.FirstOrDefault(p => p.ProductID == productId);

            if (product != null)
            {
                _cart.RemoveLine(product);
            }

            return RedirectToAction(nameof(Index), new { returnUrl });
        }
    }
}
