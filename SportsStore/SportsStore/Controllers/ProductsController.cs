using SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SportsStore.Controllers
{
    public class ProductsController : ApiController
    {
        public ProductsController()
        {
            Repository = (IRepository)GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IRepository));
        }

        public IEnumerable<Product> GetProducts()
        {
            var a = Repository.Products;
            return Repository.Products;
        }

        public IHttpActionResult GetProduct(int id)
        {
            Product result = Repository.Products.Where(prop => prop.Id == id).FirstOrDefault();
            return result == null ? (IHttpActionResult)BadRequest("No Product Found") : Ok(result);
        }

        [Authorize(Roles = "Administrators")]
        public async Task<IHttpActionResult> PostProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                await Repository.SaveProductAsync(product);
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [Authorize(Roles = "Administrators")]
        public async Task DeleteProduct(int id)
        {
            await Repository.DeleteProductAsync(id);
        }

        private IRepository Repository { get; set; }
    }
}
