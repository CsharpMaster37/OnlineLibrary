using DB_Realize_OnlineLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace DB_Realize_OnlineLibrary.Controllers
{
    public class UserController : Controller
    {
        private readonly SignInManager<Reader> _signInManager;
        private readonly UserManager<Reader> _userManager;
        LibraryContext _db;
        IWebHostEnvironment _environment;

        public UserController(SignInManager<Reader> signInManager, UserManager<Reader> userManager, LibraryContext db, IWebHostEnvironment hostEnvironment)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _db = db;
            _environment = hostEnvironment;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUser model)
        {
            if (ModelState.IsValid)
            {
                var user = new Reader { UserName = model.Username, PasswordHash = model.Password, Name = model.Name, DateofBirth = model.DateofBirth };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUser model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, isPersistent: false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(model);
            }
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return NotFound();
            if(user.GroupId != null)
                ViewBag.GroupName = _db.GroupReaders.FirstOrDefault(g => g.Id == user.GroupId);
            return View(user);
        }
    }
}
