using DB_Realize_OnlineLibrary.Models;
using Microsoft.AspNetCore.Authorization;
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
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(Publisher publisher)
        {
            _db.Publishers.Add(publisher);
            _db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        
    }
}
