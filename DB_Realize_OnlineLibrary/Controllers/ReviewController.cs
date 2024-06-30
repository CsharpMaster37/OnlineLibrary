using DB_Realize_OnlineLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DB_Realize_OnlineLibrary.Controllers
{
    public class ReviewController : Controller
    {
        LibraryContext _db;
        UserManager<Reader> _userManager;
        public ReviewController(LibraryContext context, UserManager<Reader> userManager)
        {
            _db = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Reviews(int id)
        {
            var book = _db.Books.Include(g => g.Genre).Include(p => p.Publisher).Include(c => c.Condition).FirstOrDefault(f => f.Id == id);
            var reviews = _db.Reviews.Where(r => r.BookId == id).ToList();
            double TotalRating = 0;
            if (reviews.Count() > 0)
            {
                foreach (var review in reviews)
                    TotalRating += review.Rating;
                TotalRating /= reviews.Count();
            }
            ViewBag.TotalRating = TotalRating;
            ViewBag.Book = book;
            ViewBag.Reviews = reviews;
            if (User.Identity.IsAuthenticated)
            {
                Reader user = await _userManager.GetUserAsync(User);
                ViewBag.CheckRead = checkBookForUser(id).GetAwaiter().GetResult();
            }
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(Review review)
        {
            if (review == null)
                return NotFound();
            if (review.Rating == 0)
                review.Rating = 5;
            Reader user = await _userManager.GetUserAsync(User);
            review.DatePosted = DateTime.Now;
            review.ReaderId = user.Id;
            review.UserName = user.UserName;
            var existingReview = _db.Reviews.Where(r => r.ReaderId == review.ReaderId).FirstOrDefault(r => r.BookId == review.BookId);
            if (existingReview != null)
            {
                existingReview.DatePosted = review.DatePosted;
                existingReview.Text = review.Text;
                existingReview.Rating = review.Rating;
                _db.Entry(existingReview).State = EntityState.Modified;
            }
            else
                _db.Reviews.Add(review);
            _db.SaveChanges();
            return RedirectToAction("Reviews", new { id = review.BookId });
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int? id)
        {
            Review review = _db.Reviews.Include(r => r.Book).FirstOrDefault(review => review.Id == id);
            return View(review);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(Review review)
        {
            if (review != null)
            {
                _db.Entry(review).State = EntityState.Deleted;
                _db.SaveChanges();
            }
            return RedirectToAction("Reviews", new { id = review.BookId });
        }


        public async Task<bool> checkBookForUser(int id)
        {
            Reader user = await _userManager.GetUserAsync(User);
            List<HistoryItem> BooksInHistory = _db.History.Where(r => r.ReaderId == user.Id).ToList();
            List<int> readId = new List<int>();
            foreach (var item in BooksInHistory)
            {
                readId.Add(item.BookId);
            }
            return readId.Contains(id);
        }
    }
}
