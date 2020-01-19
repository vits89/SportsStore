using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.ViewModels;

namespace SportsStore.Controllers
{
    public class ProductController : Controller
    {
        private const int PAGE_SIZE = 5;

        private readonly IProductRepository _repository;

        public ProductController(IProductRepository repository)
        {
            _repository = repository;
        }

        public ActionResult List(string category, int page = 1)
        {
            return View(new ProductsViewModel
            {
                Products = _repository.Products
                    .Where(p => category == null || p.Category == category)
                    .OrderBy(p => p.ProductID)
                    .Skip((page - 1) * PAGE_SIZE)
                    .Take(PAGE_SIZE),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PAGE_SIZE,
                    TotalItems = category != null ? _repository.Products.Where(p => p.Category == category).Count() : _repository.Products.Count()
                },
                CurrentCategory = category
            });
        }
    }
}
