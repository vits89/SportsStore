using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Models.ViewModels;
using System.Linq;

namespace SportsStore.Controllers
{
    public class ProductController : Controller
    {
        public int pageSize = 4;
        private IProductRepository repository;

        public ProductController(IProductRepository repo) => repository = repo;

        public ViewResult List(string category, int page = 1) =>
            View(
                new ProductsListViewModel
                {
                    Products = repository.Products
                        .Where(p => category == null || p.Category == category)
                        .OrderBy(p => p.ProductID)
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize),
                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = page,
                        ItemsPerPage = pageSize,
                        TotalItems = category == null ? repository.Products.Count() : repository.Products.Where(e => e.Category == category).Count()
                    },
                    CurrentCategory = category
                }
            );
    }
}
