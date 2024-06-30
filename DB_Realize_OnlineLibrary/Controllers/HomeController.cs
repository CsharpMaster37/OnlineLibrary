using DB_Realize_OnlineLibrary.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

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
            //var zanr1 = new Genre() { Name = "Учебники" };
            //var zanr2 = new Genre() { Name = "Фэнтези" };
            //var condition1 = new Condition() { Name = "Отличное" };
            //var condition2 = new Condition() { Name = "Хорошее" };
            //var condition3 = new Condition() { Name = "Плохое" };
            //var condition4 = new Condition() { Name = "Ужасное" };
            //var publisher1 = new Publisher() { Name = "Издательство1" };
            //var publisher2 = new Publisher() { Name = "Издательство2" };
            //var book1 = new Book
            //{
            //    Author = "Троелсен Э.",
            //    Name = "Язык программирования C#",
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
            //    Author = "Скиена С.",
            //    Name = "Наука о данных",
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
            //    Author = "Демидович Б.Н.",
            //    Name = "Сборник задач по математическому анализу",
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
            //    Author = "Фрай Макс",
            //    Name = "Чужак",
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
            //    Author = "Громыко О.",
            //    Name = "Верные враги",
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
            //    Author = "Лукьяненко С.",
            //    Name = "Спектр",
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

        public IActionResult Index()
        {
            var books = _db.Books.Include(g => g.Genre).Include(p => p.Publisher).Include(c => c.Condition).ToList();
            return View(books);
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
        public IActionResult Create()
        {
            ViewBag.Genre = new SelectList(_db.Genres, "Id", "Name");
            ViewBag.Publisher = new SelectList(_db.Publishers, "Id", "Name");
            ViewBag.Condition = new SelectList(_db.Conditions, "Id", "Name");
            return View();
        }

        [HttpPost]
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
        public IActionResult Rent(int id)
        {
            return RedirectToAction("Index");
        }
    }
}
