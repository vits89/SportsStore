using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _repository;
        private readonly Cart _cart;

        public OrderController(IOrderRepository repository, Cart cart)
        {
            _repository = repository;
            _cart = cart;
        }

        [Authorize]
        public ActionResult List()
        {
            return View(_repository.Orders.Where(o => !o.Shipped));
        }

        [HttpPost]
        [Authorize]
        public ActionResult MarkShipped(int orderId)
        {
            var order = _repository.Orders.FirstOrDefault(o => o.OrderID == orderId);

            if (order != null)
            {
                order.Shipped = true;

                _repository.SaveOrder(order);
            }

            return RedirectToAction(nameof(List));
        }

        public ActionResult Checkout()
        {
            return View(new Order());
        }

        [HttpPost]
        public ActionResult Checkout(Order order)
        {
            if (_cart.Lines.Count() == 0)
            {
                ModelState.AddModelError(key: string.Empty, "Sorry, your cart is empty!");
            }

            if (ModelState.IsValid)
            {
                order.Lines = _cart.Lines.ToArray();

                _repository.SaveOrder(order);

                return RedirectToAction(nameof(Completed));
            }
            else
            {
                return View(order);
            }
        }

        public ActionResult Completed()
        {
            _cart.Clear();

            return View();
        }
    }
}
