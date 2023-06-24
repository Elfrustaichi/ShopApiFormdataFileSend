using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ShopNT.UI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}