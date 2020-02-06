using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using SportsStore.Models;
using SportsStore.ViewModels;

namespace SportsStore.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public AdminController(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public ActionResult Index()
        {
            return View(_mapper.Map<IEnumerable<ProductViewModel>>(_repository.Products));
        }

        public ActionResult Edit(int productId)
        {
            var product = _repository.Products.FirstOrDefault(p => p.ProductID == productId);
            var productVm = _mapper.Map<UpdateProductViewModel>(product);

            return View(productVm);
        }

        [HttpPost]
        public ActionResult Edit(UpdateProductViewModel productVm)
        {
            if (ModelState.IsValid)
            {
                var product = _mapper.Map<Product>(productVm);

                _repository.SaveProduct(product);

                TempData["message"] = $"{product.Name} has been saved";

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(productVm);
            }
        }

        public ActionResult Create()
        {
            ViewBag.Title = "Create product";

            return View(nameof(Edit), new UpdateProductViewModel());
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
