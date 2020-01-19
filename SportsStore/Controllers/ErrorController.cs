using Microsoft.AspNetCore.Mvc;

namespace SportsStore.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Error()
        {
            return View();
        }
    }
}
