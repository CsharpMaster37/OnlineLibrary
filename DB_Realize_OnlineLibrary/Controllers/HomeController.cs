using DB_Realize_OnlineLibrary.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel;

namespace DB_Realize_OnlineLibrary.Controllers
{
    public class HomeController : Controller
    {
        LibraryContext _db;
        IWebHostEnvironment _environment;
        private readonly UserManager<Reader> _userManager;
        const int ImageWidth = 368;
        const int ImageHeight = 500;

        public HomeController(LibraryContext db, IWebHostEnvironment hostEnvironment, UserManager<Reader> userManager)
        {
            _db = db;
            _environment = hostEnvironment;
            _userManager = userManager;
            //var zanr1 = new Genre() { Name = "��������" };
            //var zanr2 = new Genre() { Name = "�������" };
            //var condition1 = new Condition() { Name = "��������" };
            //var condition2 = new Condition() { Name = "�������" };
            //var condition3 = new Condition() { Name = "������" };
            //var condition4 = new Condition() { Name = "�������" };
            //var publisher1 = new Publisher() { Name = "������������1" };
            //var publisher2 = new Publisher() { Name = "������������2" };
            //var book1 = new Book
            //{
            //    Author = "�������� �.",
            //    Name = "���� ���������������� C#",
            //    Quantity = 12,
            //    Year = 2020,
            //    Price = 4230,
            //    Genre = zanr1,
            //    Publisher = publisher1,
            //    Days = 7,
            //    Condition = condition1
            //};
            //var book2 = new Book
            //{
            //    Author = "������ �.",
            //    Name = "����� � ������",
            //    Quantity = 22,
            //    Year = 2021,
            //    Price = 1230,
            //    Genre = zanr1,
            //    Publisher = publisher1,
            //    Days = 7,
            //    Condition = condition1
            //};
            //var book3 = new Book
            //{
            //    Author = "��������� �.�.",
            //    Name = "������� ����� �� ��������������� �������",
            //    Quantity = 55,
            //    Year = 2020,
            //    Price = 975,
            //    Genre = zanr1,
            //    Publisher = publisher2,
            //    Days = 7,
            //    Condition = condition1
            //};
            //var book4 = new Book
            //{
            //    Author = "���� ����",
            //    Name = "�����",
            //    Quantity = 27,
            //    Year = 2019,
            //    Price = 450,
            //    Genre = zanr2,
            //    Publisher = publisher2,
            //    Days = 7,
            //    Condition = condition1
            //};
            //var book5 = new Book
            //{
            //    Author = "������� �.",
            //    Name = "������ �����",
            //    Quantity = 12,
            //    Year = 2020,
            //    Price = 520,
            //    Genre = zanr2,
            //    Publisher = publisher1,
            //    Days = 9,
            //    Condition = condition1
            //};
            //var book6 = new Book
            //{
            //    Author = "���������� �.",
            //    Name = "������",
            //    Quantity = 33,
            //    Year = 2018,
            //    Price = 330,
            //    Genre = zanr2,
            //    Publisher = publisher2,
            //    Days = 10,
            //    Condition = condition2
            //};
            //_db.Genres.Add(zanr1);
            //_db.Genres.Add(zanr2);
            //_db.Publishers.Add(publisher1);
            //_db.Publishers.Add(publisher2);
            //_db.Conditions.Add(condition1);
            //_db.Conditions.Add(condition2);
            //_db.Conditions.Add(condition3);
            //_db.Conditions.Add(condition4);
            //_db.Books.AddRange(new[] { book1, book2, book4, book5, book6, book3 });
            //_db.SaveChanges();
        }

        public IActionResult Index(string title, string author, string publisher)
        {
            var books = _db.Books.Include(g => g.Genre).Include(p => p.Publisher).Include(c => c.Condition).AsQueryable();

            if (!string.IsNullOrEmpty(title))
            {
                books = books.Where(b => b.Name.Contains(title));
            }

            if (!string.IsNullOrEmpty(author))
            {
                books = books.Where(b => b.Author.Contains(author));
            }

            if (!string.IsNullOrEmpty(publisher))
            {
                books = books.Where(b => b.Publisher.Name.Contains(publisher));
            }

            var bookList = books.ToList();

            List<int> history = new List<int>();
            bool isFine = false;

            if (User.Identity.IsAuthenticated)
            {
                var userId = _userManager.GetUserId(User);
                var History = _db.History.Where(c => c.ReaderId == userId && c.DateofDelivery == null).ToList();
                var Fines = _db.Fines.Where(c => c.ReaderId == userId && c.Status == "�� ��������").ToList();
                isFine = Fines.Count > 0;

                foreach (var item in History)
                {
                    history.Add(item.BookId);
                }
            }

            ViewBag.Rent = history;
            ViewBag.IsFine = isFine;

            // Pass filter values to the view
            ViewBag.TitleFilter = title;
            ViewBag.AuthorFilter = author;
            ViewBag.PublisherFilter = publisher;

            return View(bookList);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewBag.Genre = new SelectList(_db.Genres, "Id", "Name");
            ViewBag.Publisher = new SelectList(_db.Publishers, "Id", "Name");
            ViewBag.Condition = new SelectList(_db.Conditions, "Id", "Name");
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(Book book, IFormFile upload)
        {
            if (upload != null)
            {
                string fileName = Path.GetFileName(upload.FileName);
                var extFile = fileName.Substring(fileName.LastIndexOf("."));
                if (extFile.Contains("png") || extFile.Contains("bmp") || extFile.Contains("jpg") || extFile.Contains("jpeg"))
                {
                    var image = Image.Load(upload.OpenReadStream());
                    image.Mutate(x => x.Resize(ImageWidth, ImageHeight));
                    string path = "\\wwwroot\\images\\" + fileName;
                    var hostPath = _environment.ContentRootPath + path;
                    image.Save(hostPath);
                    book.ImageUrl = fileName;
                }
            }
            _db.Books.Add(book);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Book book = _db.Books.Include(g => g.Genre).Include(p => p.Publisher).Include(c => c.Condition).FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            ViewBag.Genre = new SelectList(_db.Genres, "Id", "Name");
            ViewBag.Publisher = new SelectList(_db.Publishers, "Id", "Name");
            ViewBag.Condition = new SelectList(_db.Conditions, "Id", "Name");
            return View(book);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(Book book, IFormFile upload)
        {
            if (upload != null)
            {
                string fileName = Path.GetFileName(upload.FileName);
                var extFile = fileName.Substring(fileName.LastIndexOf("."));
                if (extFile.Contains("png") || extFile.Contains("bmp") || extFile.Contains("jpg") || extFile.Contains("jpeg"))
                {
                    var image = Image.Load(upload.OpenReadStream());
                    image.Mutate(x => x.Resize(ImageWidth, ImageHeight));
                    string path = "\\wwwroot\\images\\" + fileName;
                    var hostPath = _environment.ContentRootPath + path;
                    image.Save(hostPath);
                    book.ImageUrl = fileName;
                }
            }

            _db.Entry(book).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize]
        public IActionResult Rent(int id)
        {
            var userId = _userManager.GetUserId(User);
            var book = _db.Books.FirstOrDefault(x => x.Id == id);
            var newRent = new HistoryItem
            {
                ReaderId = userId,
                BookId = id,
                DateofCapture = DateTime.Now,
                DateofDelivery = null,
                ExpectedDateOfDelivery = DateTime.Now.AddDays(book.Days)
            };
            book.Quantity--;
            _db.History.Add(newRent);
            _db.Entry(book).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();
            Book book = _db.Books.Include(f => f.Genre).FirstOrDefault(f => f.Id == id);
            if (book == null)
                return NotFound();
            return View(book);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(Book book)
        {
            if (book != null)
            {
                _db.Entry(book).State = EntityState.Deleted;
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
