using DB_Realize_OnlineLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace DB_Realize_OnlineLibrary.Controllers
{
    public class PublisherController : Controller
    {
        LibraryContext _db;
        IWebHostEnvironment _environment;
        public PublisherController(LibraryContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _environment = hostEnvironment;
        }
        public IActionResult Index()
        {
            var publishers = _db.Publishers.ToList();
            return View(publishers);
        }

        
    }
}
