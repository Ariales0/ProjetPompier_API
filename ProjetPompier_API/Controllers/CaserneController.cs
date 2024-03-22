using Microsoft.AspNetCore.Mvc;

namespace ProjetPompier_API.Controllers
{
    public class CaserneController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
