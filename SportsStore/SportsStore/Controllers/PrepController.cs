using SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.Controllers
{
    public class PrepController : Controller
    {
        IRepository repo;
        
        public PrepController()
        {
            repo = new ProductRepository();
        }

        public ActionResult Index()
        {
            return View(repo.Products);
        }

        public async Task<ActionResult> DeleteProduct(int id)
        {
            await repo.DeleteProductAsync(id);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> SaveProduct(Product product)
        {
            await repo.SaveProductsAsync(product);
            return RedirectToAction("Index");
        }

        public ActionResult Orders()
        {
            return View(repo.Orders);
        }

        public async Task<ActionResult> DeleteOrder(int id)
        {
            await repo.DeleteOrderAsync(id);
            return RedirectToAction("Orders");
        }

        public async Task<ActionResult> SaveOrder(Order order)
        {
            await repo.SaveOrdersAsync(order);
            return RedirectToAction("Orders");
        }
    }
}