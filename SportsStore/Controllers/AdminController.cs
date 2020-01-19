using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IProductRepository _repository;

        public AdminController(IProductRepository repository)
        {
            _repository = repository;
        }

        public ActionResult Index()
        {
            return View(_repository.Products);
        }

        public ActionResult Edit(int productId)
        {
            return View(_repository.Products.FirstOrDefault(p => p.ProductID == productId));
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _repository.SaveProduct(product);

                TempData["message"] = $"{product.Name} has been saved";

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(product);
            }
        }

        public ActionResult Create()
        {
            ViewBag.Title = "Create product";

            return View(nameof(Edit), new Product());
        }

        [HttpPost]
        public ActionResult Delete(int productId)
        {
            var product = _repository.DeleteProduct(productId);

            if (product != null)
            {
                TempData["message"] = $"{product.Name} was deleted";
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public ActionResult SeedDatabase()
        {
            SeedData.EnsurePopulated(HttpContext.RequestServices);

            return RedirectToAction(nameof(Index));
        }
    }
}
