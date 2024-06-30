using DB_Realize_OnlineLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DB_Realize_OnlineLibrary.Controllers
{
    public class MyBooksController : Controller
    {
        LibraryContext _db;
        IWebHostEnvironment _environment;
        private readonly UserManager<Reader> _userManager;
        public MyBooksController(LibraryContext db, IWebHostEnvironment hostEnvironment, UserManager<Reader> userManager)
        {
            _db = db;
            _environment = hostEnvironment;
            _userManager = userManager;
        }

        [Authorize]
        public IActionResult Index()
        {
            var userId = _userManager.GetUserId(User);
            var historyList = _db.History.Where(x => x.ReaderId == userId).Include(b => b.Book).ToList();
            return View(historyList);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Return(int? id)
        {
            if (id == null)
                return NotFound();
            var historyItem = _db.History.Include(f => f.Book).FirstOrDefault(f => f.Id == id);
            if (historyItem == null)
                return NotFound();
            return View(historyItem);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Return(HistoryItem historyItem)
        {
            if (historyItem != null)
            {
                historyItem.DateofDelivery = DateTime.Now;
                if (historyItem.DateofDelivery > historyItem.ExpectedDateOfDelivery)
                {
                    int overdue = ((DateTime)historyItem.DateofDelivery).Day - ((DateTime)historyItem.ExpectedDateOfDelivery).Day + 1;
                    var fine = new Fine()
                    {
                        ReaderId = historyItem.ReaderId,
                        BookId = historyItem.BookId,
                        IsMess = false,
                        OverdueDays = overdue,
                        Price = overdue*100,
                        Status = "Не оплачено"
                    };
                    _db.Fines.Add(fine);
                }
                var book = _db.Books.FirstOrDefault(x => x.Id == historyItem.BookId);
                book.Quantity++;
                _db.Entry(book).State = EntityState.Modified;
                _db.Entry(historyItem).State = EntityState.Modified;
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
