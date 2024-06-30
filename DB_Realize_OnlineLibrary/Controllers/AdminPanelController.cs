using Microsoft.AspNetCore.Mvc;

namespace DB_Realize_OnlineLibrary.Controllers
{
    public class AdminPanelController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
