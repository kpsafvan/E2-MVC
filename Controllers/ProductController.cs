using E2.Data;
using E2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.EntityFrameworkCore;
//using System.Web.Mvc;

namespace E2.Controllers
{
    
    //[System.Web.Mvc.HandleError(View = "Errors")]
    
    [Route("Product")]
    public class ProductController : Controller
    {
        private readonly E2DbContext _e2DbContext;

        public ProductController(E2DbContext e2DbContext)
        {
            _e2DbContext= e2DbContext;
        } 
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            
            var cookie = Request.Cookies["CurrentUser"];
            if (cookie==null)
            {
                
               // string xMessage = " Please Login";
                //Response.WriteAsync("<script>alert('" + xMessage + "')</script>");
                context.Result= RedirectToAction("asklogin", "Home");
                //Response.WriteAsJsonAsync
                //RedirectToRoutePermanent(routeName: "Home/Index");
                //RedirectToAction(nameof(Start));
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
                if(!login)
                {

                    context.Result = RedirectToAction("asklogin", "Home");
                }
            }
            
        }
       // [LoginActionFilter()]
        [Route("Start")]
        public ActionResult Start() 
        {
            return View();
        }
      //  [ExceptionFilter("Error")]
        [Route("Show")]
        public ActionResult Show()
        {
           // int a = 0;
            //int b = 1 / a;
            var prod = _e2DbContext.Products.Include(c=>c.Cat).Where(c=>!c.IsDeleted).AsNoTracking().ToList();
            return View(prod);
        }
        [Route("Find/{id}")]
        public ActionResult Find(long id)
        {
            var product = _e2DbContext.Products.AsNoTracking().Include(c=>c.Cat).FirstOrDefault(x=>x.ProductId==id);
            if (product.IsDeleted == true)
            {
                return NotFound();
                //Message of not found to be put on a view or in the function to show messages
            }
            /*
            var prod = _e2DbContext.Products.FromSqlRaw<ProductModel>("Find" + '"' + id.ToString() + '"').ToList();
            if (prod[0].IsDeleted == true)
            {
                return NotFound();
                //Message of not found to be put on a view or in the function to show messages
            }*/
            return View(product);
        }
        [Route("Add")]
        public ActionResult Add()
        {
            var cat = _e2DbContext.Categories.AsNoTracking().ToList();
            //Decide if category is to be implemented with long or string(Decided) implement long show string
            ViewBag.Categories = new SelectList(cat, "CategoryId", "Name");
            return View();
        }
        [HttpPost("Create")]
        public ActionResult Create(ProductModel ProductAdd)
        {
            //var errors = ModelState.Values.SelectMany(v => v.Errors);
            long UserId = Convert.ToInt64(Request.Cookies["CurrentUser"]);
            
            if (ModelState.IsValid)
            {
                //Category validation
                var cat = _e2DbContext.Categories.AsNoTracking().ToList();
                var cats = (from p in cat
                           select p.CategoryId
                           ).ToList();
               
                if (cats.Contains(ProductAdd.Category))
                {
                    //Added when dates were made nullable
                    if(ProductAdd.ManDate.HasValue && ProductAdd.ExpDate.HasValue )
                    {
                        //converting Dt? to Dt Explicitly
                        int s = DateTime.Compare((DateTime)ProductAdd.ManDate, (DateTime)ProductAdd.ExpDate);
                        //Checking if Mandate is after expdate
                        if (s > 0)
                        {
                            Response.Redirect("Product/Show");
                            //Redirect.ToRoute(Redirect)
                            //return View("Show");
                        }
                        else
                        {
                            //Need to find a better way , code is repeated
                            ProductAdd.IsDeleted = false;
                            ProductAdd.CreatedBy = UserId;
                            ProductAdd.CreatedDate = DateTime.Now;
                            ProductAdd.LastModifiedBy = UserId;
                            ProductAdd.LastModifiedDate = DateTime.Now;
                            _e2DbContext.Products.Add(ProductAdd);
                            _e2DbContext.SaveChanges(); 
                            return View(ProductAdd);
                        }
                    }
                    ProductAdd.Cat = _e2DbContext.Categories.Find(ProductAdd.Category);
                    ProductAdd.IsDeleted = false;
                    ProductAdd.CreatedBy = UserId;
                    ProductAdd.CreatedDate = DateTime.Now;
                    ProductAdd.LastModifiedBy = UserId;
                    ProductAdd.LastModifiedDate = DateTime.Now;
                    _e2DbContext.Products.Add(ProductAdd);
                    _e2DbContext.SaveChanges();
                        //suggested change - no need to return product when not added
                }
            }
            
            //var prop = _e2DbContext.Products.Include(c => c.Cat).FirstOrDefault(x => x.ProductId == ProductAdd.ProductId);
            return View(ProductAdd);
        }
        [Route("Signin")]
        public ActionResult Signin()
        {
            //var prod = _e2DbContext.Products.FromSqlRaw<ProductModel>("Find" + '"' + id.ToString() + '"').ToList();
            //return View(prod[0]);
            return View();
        }
        [Route("Edit/{id}")]
        public ActionResult Edit(long id)
        {
            var product = _e2DbContext.Products.Find(id);
            var cat = _e2DbContext.Categories.AsNoTracking().ToList();
            ViewBag.Categories = new SelectList(cat, "CategoryId", "Name");
            // var prod = _e2DbContext.Products.FromSqlRaw<ProductModel>("Find" + '"' + id.ToString() + '"').ToList();
            return View(product);
           // return View(prod[0]);
        }
        [HttpPost("EditDB")]
        public ActionResult EditDB(ProductModel edit)
        {
            long UserId = Convert.ToInt64(Request.Cookies["CurrentUser"]);
            var product = _e2DbContext.Products.Include(c=>c.Cat).FirstOrDefault(x=>x.ProductId==edit.ProductId);
            if (product == null)
            {
                return NotFound();
            }
            product.Cat = _e2DbContext.Categories.Find(edit.Category);
            product.Name=edit.Name;
            product.Brand=edit.Brand;
            product.Made = edit.Made;
            product.ManDate=edit.ManDate;
            product.ExpDate = edit.ExpDate;
            product.Description=edit.Description;
            product.Price=edit.Price;
            product.Category = edit.Category;
            //Special
            product.LastModifiedBy = UserId;
            product.LastModifiedDate = DateTime.Now;
            //product.CreatedDate = edit.CreatedDate;
            //product.CreatedBy = edit.CreatedBy;
            //product.IsDeleted = edit.IsDeleted;
            _e2DbContext.SaveChanges();
            return View(product);
        }
        [Route("DeleteCheck/{id}")]

        public ActionResult DeleteCheck(long id)
        {
            var prod = _e2DbContext.Products.Find(id);
            if (prod == null)
            {
                return NotFound();
            }
           

            //Inefficient , accessing two times . try to solve with just one access 
            var stock = _e2DbContext.Stocks.Where(c => c.ProductId == prod.ProductId).ToList();
            if (stock.Any())
            {
                var stocklist = (from c in stock
                                select c.StockId.ToString()).ToList();
                TempData["Stocktd"] = stocklist;
                //Session["sStocktd"] = stocklist; 
               // ISession
                TempData["id"] = id;
                return View("DeleteConfirmation");
            }
            else
            {
                
                  return RedirectToAction("Delete","Product",new {id=id }) ;
            }
            var prodss = _e2DbContext.Products.Include(c => c.Cat).AsNoTracking().ToList();
            return View("Show", prodss);
            //RedirectToAction("Show", "Product");
        }
            // POST: ProductController/Delete/5
        [Route("Delete/{id}")]
     
        public ActionResult Delete(long id)
        {
            long UserId = Convert.ToInt64(Request.Cookies["CurrentUser"]);
            //var Stocktd = TempData["Stocktd"] as List<long>;
            string[] sd = (string[])TempData["Stocktd"];
            //long[] Stocktd = from c in sd
            //             select Convert.ToInt64(c);
            if (sd != null)
            {
                long[] Stocktd = sd.Select(long.Parse).ToArray();
                //    long[] LongNum = StringNum.Select(long.Parse).ToArray();
                foreach (var stocktd in Stocktd)
                {
                    var Stock = _e2DbContext.Stocks.Find(stocktd);
                    if (Stock == null)
                    {
                        return NotFound();
                    }
                    Stock.isDeleted = true;
                    Stock.LastModifiedDate = DateTime.Now;
                    Stock.LastModifiedBy = UserId;
                }
                _e2DbContext.SaveChanges();
            }
            var prod = _e2DbContext.Products.Include(c=>c.Cat).FirstOrDefault(x=>x.ProductId==id);
            if(prod == null)
            {
                return NotFound();
            }
            prod.IsDeleted = true;
            prod.LastModifiedBy= UserId;
            prod.LastModifiedDate= DateTime.Now;
            //_e2DbContext.Products.Remove(prod);
            _e2DbContext.SaveChanges();
           // RedirectToAction("Show");
            return View(prod);
        }
        [Route("HDelete/{id}")]
        public ActionResult HDelete(long id)
        {
            long UserId = Convert.ToInt64(Request.Cookies["CurrentUser"]);
            var prod = _e2DbContext.Products.Find(id);
            if (prod == null)
            {
                return NotFound();
            }
            prod.IsDeleted = true;
            prod.LastModifiedBy = UserId;
            prod.LastModifiedDate=DateTime.Now;
            _e2DbContext.Products.Remove(prod);
            _e2DbContext.SaveChanges();
            //RedirectToAction("Show");
            return View(prod);
        }
    }
}
