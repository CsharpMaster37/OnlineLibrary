using DB_Realize_OnlineLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Policy;

namespace DB_Realize_OnlineLibrary.Controllers
{
    public class GroupsController : Controller
    {
        LibraryContext _db;
        IWebHostEnvironment _environment;
        private readonly UserManager<Reader> _userManager;
        public GroupsController(LibraryContext db, IWebHostEnvironment hostEnvironment, UserManager<Reader> userManager)
        {
            _db = db;
            _environment = hostEnvironment;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var groups = _db.GroupReaders.ToList();
            var userId = _userManager.GetUserId(User);
            var user = _db.Users.FirstOrDefault(x => x.Id == userId);
            ViewBag.InGroup = user.GroupId;
            return View(groups);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public IActionResult Create(GroupReaders group)
        {
            var userId = _userManager.GetUserId(User);
            var user = _db.Users.FirstOrDefault(x => x.Id == userId);
            _db.GroupReaders.Add(group);
            _db.SaveChanges();
            user.GroupId = group.Id;
            _db.Entry(user).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index", "Groups");
        }

        [HttpGet]
        [Authorize]
        public IActionResult Leave(int? id)
        {
            if (id == null)
                return NotFound();
            var group = _db.GroupReaders.FirstOrDefault(f => f.Id == id);
            if (group == null)
                return NotFound();
            return View(group);
        }
        [Authorize]
        [HttpPost]
        public IActionResult Leave()
        {
            var userId = _userManager.GetUserId(User);
            var user = _db.Users.FirstOrDefault(x => x.Id == userId);
            user.GroupId = null;
            _db.Entry(user).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index", "Groups");
        }

        [HttpGet]
        [Authorize]
        public IActionResult Join(int? id)
        {
            if (id == null)
                return NotFound();
            var group = _db.GroupReaders.FirstOrDefault(f => f.Id == id);
            if (group == null)
                return NotFound();
            return View(group);
        }
        [Authorize]
        [HttpPost]
        public IActionResult Join(GroupReaders group)
        {
            var userId = _userManager.GetUserId(User);
            var user = _db.Users.FirstOrDefault(x => x.Id == userId);
            user.GroupId = group.Id;
            _db.Entry(user).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index", "Groups");
        }

        [HttpGet]
        public IActionResult Composition(int? id)
        {
            var users = _db.Users.Where(x => x.GroupId == id).ToList();
            var group = _db.GroupReaders.FirstOrDefault(x => x.Id == id);
            ViewBag.NameGroup = group.Name;
            return View(users);
        }
    }
}
