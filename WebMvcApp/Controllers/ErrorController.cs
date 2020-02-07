using Microsoft.AspNetCore.Mvc;

namespace SportsStore.WebMvcApp.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Error()
        {
            return View();
        }
    }
}
