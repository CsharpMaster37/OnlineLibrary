using DB_Realize_OnlineLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DB_Realize_OnlineLibrary.Controllers
{
    public class FinesController : Controller
    {
        LibraryContext _db;
        IWebHostEnvironment _environment;
        private readonly UserManager<Reader> _userManager;
        public FinesController(LibraryContext db, IWebHostEnvironment hostEnvironment, UserManager<Reader> userManager)
        {
            _db = db;
            _environment = hostEnvironment;
            _userManager = userManager;
        }
        [Authorize]
        public IActionResult Index()
        {
            var userId = _userManager.GetUserId(User);
            var fineList = _db.Fines.Where(x => x.ReaderId == userId).Include(b => b.Book).ToList();
            return View(fineList);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Pay(int? id)
        {
            if (id == null)
                return NotFound();
            var fine = _db.Fines.Include(f => f.Book).FirstOrDefault(f => f.Id == id);
            if (fine == null)
                return NotFound();
            return View(fine);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Pay(Fine fine)
        {
            if (fine != null)
            {
                fine.Status = "Оплачено";
                _db.Entry(fine).State = EntityState.Modified;
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
