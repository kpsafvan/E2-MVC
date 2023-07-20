using E2.Data;
using E2.Models;
using E2.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace E2.Controllers
{
    public class HomeController : Controller
    {
        private readonly E2DbContext _e2DbContext;
        public bool loggedin = false;

        public HomeController(E2DbContext e2DbContext)
        {
            _e2DbContext = e2DbContext;
        }
        private readonly ILogger<HomeController> _logger;

      /*  public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }*/

        [HttpPost("Login")]
        public ActionResult Login(UserModel Credentials)
        {
            string key = "CurrentUser";
            long value = Credentials.UserId;
            CookieOptions obj = new CookieOptions();

            //obj.Expires = DateTime.Now.AddMinutes(1);
            Response.Cookies.Append(key, value.ToString());
            var usert = _e2DbContext.Users.ToList();
            
            //Response.Cookies.Delete(key);
            //Response.Cookies myCookie = new Cookie("CurrentUser");
            //myCookie.Expires = DateTime.Now.AddDays(-1d);

            //Response.Cookie.Append(key, value, options);
            //Response.Cookies["CurrentUser"].Expires = DateTime.Now.AddDays(-1);
            loggedin= true;
            //string CookieValue = Request.Cookies[key];
            ViewData["User"] =  (from p in usert
                                 where p.UserId== value
                                 select p.UserName).First();
            //return RedirectToAction("Start", "Product");
            //return RedirectToAction(Show);
            //var user = CookieValue;
            return View();
        }
        public IActionResult Users()
        {
            var users = _e2DbContext.Users.AsNoTracking().ToList();
            return View(users);
        }
        public IActionResult asklogin()
        {
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
       


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            //Activity.Current.
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}