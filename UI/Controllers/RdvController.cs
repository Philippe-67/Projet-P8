using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
    public class RdvController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
