using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using SportsStore.AppCore.Interfaces;
using SportsStore.WebMvcApp.ViewModels;

namespace SportsStore.WebMvcApp.Controllers
{
    public class ProductController : Controller
    {
        private const int PAGE_SIZE = 5;

        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public ProductController(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public ActionResult List(string category, int page = 1)
        {
            var products = _repository.Products
                .Where(p => category == null || p.Category == category)
                .OrderBy(p => p.ProductID)
                .Skip((page - 1) * PAGE_SIZE)
                .Take(PAGE_SIZE);

            return View(new ProductsViewModel
            {
                Products = _mapper.Map<IEnumerable<ProductViewModel>>(products),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PAGE_SIZE,
                    TotalItems = category != null ?
                        _repository.Products.Where(p => p.Category == category).Count() :
                        _repository.Products.Count()
                },
                CurrentCategory = category
            });
        }
    }
}
