using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SportsStore.AppCore.Interfaces;

namespace SportsStore.WebMvcApp.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private readonly IProductRepository _repository;

        public NavigationMenuViewComponent(IProductRepository repository)
        {
            _repository = repository;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["category"];

            return View(_repository.Products
                .Select(p => p.Category)
                .Distinct()
                .OrderBy(p => p));
        }
    }
}
