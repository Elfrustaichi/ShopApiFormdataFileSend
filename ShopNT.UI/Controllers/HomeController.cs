using Microsoft.AspNetCore.Mvc;
using ShopNT.UI.Filters;
using System.Diagnostics;

namespace ShopNT.UI.Controllers
{
    [ServiceFilter(typeof(AuthFilter))]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}