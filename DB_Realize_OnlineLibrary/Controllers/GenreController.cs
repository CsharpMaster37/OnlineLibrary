using DB_Realize_OnlineLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace DB_Realize_OnlineLibrary.Controllers
{
    public class GenreController : Controller
    {
        LibraryContext _db;
        IWebHostEnvironment _environment;
        public GenreController(LibraryContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _environment = hostEnvironment;
        }
        public IActionResult Index()
        {
            var genres = _db.Genres.ToList();
            return View(genres);
        }
    }
}
