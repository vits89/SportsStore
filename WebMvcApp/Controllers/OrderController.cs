using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using SportsStore.AppCore.Entities;
using SportsStore.AppCore.Interfaces;
using SportsStore.WebMvcApp.ViewModels;

namespace SportsStore.WebMvcApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _repository;
        private readonly ICartService _cartService;
        private readonly IMapper _mapper;

        public OrderController(IOrderRepository repository, ICartService cartService, IMapper mapper)
        {
            _repository = repository;
            _cartService = cartService;
            _mapper = mapper;
        }

        [Authorize]
        public ActionResult List()
        {
            var orderVms = _mapper.Map<IEnumerable<OrderViewModel>>(_repository.Orders.Where(o => !o.Shipped));

            return View(orderVms);
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
            return View(new AddOrderViewModel());
        }

        [HttpPost]
        public ActionResult Checkout(AddOrderViewModel orderVm)
        {
            if (_cartService.Lines.Count() == 0)
            {
                ModelState.AddModelError(key: string.Empty, "Sorry, your cart is empty!");
            }

            if (ModelState.IsValid)
            {
                var order = _mapper.Map<Order>(orderVm);

                order.Lines = _cartService.Lines.ToArray();

                _repository.SaveOrder(order);

                return RedirectToAction(nameof(Completed));
            }
            else
            {
                return View(orderVm);
            }
        }

        public ActionResult Completed()
        {
            _cartService.Clear();

            return View();
        }
    }
}
