using DB_Realize_OnlineLibrary.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

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
            //var book1 = new Book
            //{
            //    Author = "�������� �.",
            //    Name = "���� ���������������� C#",
            //    Quantity = 12,
            //    Year = 2020,
            //    Price = 4230,
            //    Genre = zanr1,
            //    Condition = "��������"
            //};
            //var book2 = new Book
            //{
            //    Author = "������ �.",
            //    Name = "����� � ������",
            //    Quantity = 22,
            //    Year = 2021,
            //    Price = 1230,
            //    Genre = zanr1,
            //    Condition = "��������"
            //};
            //var book3 = new Book
            //{
            //    Author = "��������� �.�.",
            //    Name = "������� ����� �� ��������������� �������",
            //    Quantity = 55,
            //    Year = 2020,
            //    Price = 975,
            //    Genre = zanr1,
            //    Condition = "��������"
            //};
            //var book4 = new Book
            //{
            //    Author = "���� ����",
            //    Name = "�����",
            //    Quantity = 27,
            //    Year = 2019,
            //    Price = 450,
            //    Genre = zanr2,
            //    Condition = "��������"
            //};
            //var book5 = new Book
            //{
            //    Author = "������� �.",
            //    Name = "������ �����",
            //    Quantity = 12,
            //    Year = 2020,
            //    Price = 520,
            //    Genre = zanr2,
            //    Condition = "��������"
            //};
            //var book6 = new Book
            //{
            //    Author = "���������� �.",
            //    Name = "������",
            //    Quantity = 33,
            //    Year = 2018,
            //    Price = 330,
            //    Genre = zanr2,
            //    Condition ="�������"
            //};
            //_db.Genres.Add(zanr1);
            //_db.Genres.Add(zanr2);
            //_db.Books.AddRange(new[] { book1, book2, book4, book5, book6, book3 });
            //_db.SaveChanges();
        }

        public IActionResult Index()
        {
            var books = _db.Books.Include(b => b.Genre).ToList();
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
    }
}
