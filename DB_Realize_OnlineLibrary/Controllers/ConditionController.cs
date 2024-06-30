using DB_Realize_OnlineLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace DB_Realize_OnlineLibrary.Controllers
{
    public class ConditionController : Controller
    {
        LibraryContext _db;
        IWebHostEnvironment _environment;
        public ConditionController(LibraryContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _environment = hostEnvironment;
        }
        public IActionResult Index()
        {
            var conditions = _db.Conditions.ToList();
            return View(conditions);
        }
    }
}
