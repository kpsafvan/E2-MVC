using E2.Data;
using E2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;

namespace E2.Controllers
{
    [Route("Category")]
    public class CategoryController : Controller
    {
        private readonly E2DbContext _e2DbContext;
        public CategoryController(E2DbContext e2dbContext)
        {
            _e2DbContext=e2dbContext;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var cookie = Request.Cookies["CurrentUser"];
            if (cookie == null)
            {
                context.Result = RedirectToAction("asklogin", "Home");
            }
            else
            {
                bool login = false;
                var Users = _e2DbContext.Users.AsNoTracking().ToList();
                foreach (var user in Users)
                {
                    if (user.UserId.ToString() == cookie.ToString())
                    {
                        login = true;
                    }
                }
                if (!login)
                {
                    context.Result = RedirectToAction("asklogin", "Home");
                }
            }
        }

        [Route("Show")]
        public ActionResult Show()
        {
            
            var cat = _e2DbContext.Categories.AsNoTracking().ToList();
            string[] par = new string[cat.Count+1];
            if (cat != null)
            {   
                foreach(var c in cat)
                {
                    if(c.Parent==0)
                    {
                        par[c.CategoryId] = "No Parent";
                    }
                    else
                    {
                        string i = (from d in cat
                                 where d.CategoryId == c.Parent
                                 select d.Name).First();
                        par[c.CategoryId] = i;
                    }
                }
 
                ViewData["Parents"]= par;
                return View(cat);
            }
            return View(cat);
        }
        [Route("Add")]
        public ActionResult Add()
        {
            ViewBag.Categories = _e2DbContext.Categories.AsNoTracking().ToList();
            return View();
        }
        [Route("Create")]
        public ActionResult Create(CategoryModel Cat)
        {
            long UserId = Convert.ToInt64(Request.Cookies["CurrentUser"]);

            if (ModelState.IsValid)
            {
                Cat.isDeleted = false;
                Cat.CreatedDate= DateTime.Now;
                Cat.CreatedBy = UserId;
                Cat.LastModifiedBy= UserId;
                Cat.LastModifiedDate= DateTime.Now;
                
                _e2DbContext.Categories.Add(Cat);   
                _e2DbContext.SaveChanges();
               
            }
            return View(Cat);

        }
        //[Route("Details")]
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CategoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
