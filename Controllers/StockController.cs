using Microsoft.AspNetCore;
using E2.Data;
using E2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using E2.Migrations;
using System.Linq;
using System.Drawing.Text;

namespace E2.Controllers
{
    [Route("Stock")]
    public class StockController : Controller
    {
        private readonly E2DbContext _e2DbContext;
        public StockController(E2DbContext e2DbContext)
        {
            _e2DbContext = e2DbContext;
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
        // GET: StockController
        [Route("Show")]
        public ActionResult Show()
        {
            var stocks = _e2DbContext.Stocks.AsNoTracking().Where(p=>p.Product.ExpDate <= DateTime.Today || p.Product.ExpDate == null).Where(p=>!p.isDeleted).Include(c => c.Product).Include(x=>x.Location).ToList();
            //var stock = from p in stocks
              //          where p.Product.ExpDate <= DateTime.Today || p.Product.ExpDate==null
                //        select p;

            return View(stocks);
        }

        [Route("Find/{Id}")]
        public ActionResult Find(long id)
        {
            var stock = _e2DbContext.Stocks.AsNoTracking().Include(c=>c.Product).Include(x=>x.Location).FirstOrDefault(x=>x.StockId==id);
            return View(stock);
        }

        [Route("Add")]
        public ActionResult Add()
        {
            //var Ids = from U in _e2DbContext.Products
              //        select U.ProductId;
            //CHANGES MADE DURIG MEETING
            var prod = _e2DbContext.Products.AsNoTracking().Where(t=>!t.IsDeleted).ToList();
            //To eliminate deleted products
            /* var pro = from p in prod
                       where p.IsDeleted == false
                       select p;*/
            //var a = Ids.ToList();
            var loc = _e2DbContext.Location.ToList();
            ViewBag.locations = new SelectList(loc, "LocationId", "Name");
            ViewBag.Ids = new SelectList(prod, "ProductId", "Name");
            return View();
        }
        // GET: StockController/Create
        [HttpPost("Create")]
        public ActionResult Create(StockModel StockAdd)
        {
            long User = Convert.ToInt64(Request.Cookies["CurrentUser"]);
            //Below validation was replaced with where cleause inside the Ef query
            var prod = _e2DbContext.Products.Where(t =>!t.IsDeleted).FirstOrDefault(x=>x.ProductId==StockAdd.ProductId);
            //Serverside validation of Id
            //Checking if ID is of a non-deleted product entry
            /*long pro = Convert.ToInt64((from p in prod
                      where (p.IsDeleted== false) && (p.ProductId == StockAdd.ProductId)
                      select p.ProductId 
                      ).First());*/
            /*
            var pros = from p in prod
                       where p.IsDeleted == false
                       select p.ProductId;
            long pro = pros.First();*/
            if (ModelState.IsValid&&prod!=null)
            {
                StockAdd.isDeleted = false;
                StockAdd.CreatedDate=DateTime.Now;
                StockAdd.CreatedBy = User;
                StockAdd.LastModifiedDate=DateTime.Now;
                StockAdd.LastModifiedBy = User;
                _e2DbContext.Stocks.Add(StockAdd);  
                _e2DbContext.SaveChanges();
            }
            return View(StockAdd);
        }

        [Route("Edit/{id}")]
        public ActionResult Edit(long id)
        {
            var stock = _e2DbContext.Stocks.AsNoTracking().Include(c => c.Product).FirstOrDefault(x => x.StockId == id);
            var loc = _e2DbContext.Location.ToList();
            ViewBag.locations = new SelectList(loc, "LocationId", "Name");
            return View(stock);
        }
        [HttpPost("EditDB")]
        public ActionResult EditDB(StockModel edit)
        {
            long UserId = Convert.ToInt64(Request.Cookies["CurrentUser"]);
            var stock = _e2DbContext.Stocks.Include(c => c.Product).Include(x => x.Location).FirstOrDefault(x => x.StockId == edit.StockId);
            if (stock == null)
            {
                return NotFound();
            }
            stock.Quantity = edit.Quantity;
            stock.LocationId= edit.LocationId;
            stock.Location= edit.Location;
            //Special
            stock.LastModifiedBy = UserId;
            stock.LastModifiedDate = DateTime.Now;
            //product.CreatedDate = edit.CreatedDate;
            //product.CreatedBy = edit.CreatedBy;
            //product.IsDeleted = edit.IsDeleted;
            _e2DbContext.SaveChanges();
            return View(stock);
        }
        [Route("Delete/{Id}")]
        public ActionResult Delete(long id)
        {
            //var cookie = Request.Cookies["CurrentUser"];
            var User = Request.Cookies["CurrentUser"];
            var Stock = _e2DbContext.Stocks.Include(c => c.Product).Include(x=>x.Location).FirstOrDefault(x => x.StockId == id);
            if (Stock == null)
            {
                return NotFound();
            }
            Stock.isDeleted = true;
            Stock.LastModifiedDate = DateTime.Now;
            Stock.LastModifiedBy = Convert.ToInt64(User);
            _e2DbContext.SaveChanges();
            return View(Stock);
        }
        [Route("Simplify")]
        public ActionResult Simplify()
        {
            var User = Request.Cookies["CurrentUser"];
            var stock = _e2DbContext.Stocks.Where(x=>!x.isDeleted).Include(x => x.Location).Include(x => x.Product);
            var p_ids = (from i in stock
                         orderby i.StockId
                        select new { i.StockId, i.Quantity,i.ProductId }
                         ).ToArray();
            var l_ids = (from i in stock
                         orderby i.StockId
                         select new {  i.StockId,i.LocationId}).ToArray();

            long quantity=0;
            long stock1=0;
            long stock2=0;
            foreach (var item in stock)
            {
                for (int i = 0; i < p_ids.Length; i++)
                {
                    //check if both the stocks are same
                    if(item.StockId != l_ids[i].StockId && l_ids[i].StockId == p_ids[i].StockId )
                    {
                        //check if the location is same
                        if(item.LocationId == l_ids[i].LocationId)
                        {
                            //check if product is same
                            if (item.ProductId == p_ids[i].ProductId)
                            {
                                //quantity of the new item
                                quantity = item.Quantity + p_ids[i].Quantity;
                                stock1=item.StockId;
                                stock2 = p_ids[i].StockId;
                                //fuse();

                                break;
                               
                            }
                        }

                    } 
                }
            }
            _e2DbContext.SaveChanges();
            if(quantity > 0)
            {
                var stockd = _e2DbContext.Stocks.Find(stock1);
                stockd.isDeleted = true;
                stockd.LastModifiedDate = DateTime.Now;
                stockd.LastModifiedBy = Convert.ToInt64(User);
                _e2DbContext.SaveChanges();
                var stocku = _e2DbContext.Stocks.Find(stock2);
                stocku.Quantity = quantity; 
                _e2DbContext.SaveChanges();
            }

            return RedirectToAction("Show", "Stock");
        }
        // POST: StockController/Delete/5
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
