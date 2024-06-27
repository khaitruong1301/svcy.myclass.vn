using CinimaServer.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using CinimaServer.Tools;
namespace CinemaServer.Api
{
    public class ProductApiController : ApiController
    {
        private StoreDBEntities db = new StoreDBEntities();

        // GET: api/ProductApi/getall
        [HttpGet]
        [Route("api/ProductApi/getall")]
        public IEnumerable<Product> GetAllProducts(string keyword="")
        {
            if(keyword !="")
            {
                keyword = FuncUtilities.BestLower(keyword);
                return db.Products.ToList().Where(n => FuncUtilities.BestLower(n.name).Contains(keyword));
            }
            return db.Products;
        }

        // GET: api/ProductApi/get/{id}
        [HttpGet]
        [Route("api/ProductApi/get/{id}")]
        [ResponseType(typeof(Product))]
        public async Task<IHttpActionResult> GetProductById(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return Content(HttpStatusCode.BadRequest, "ID sản phẩm không hợp lệ!");
            }

            var product = await db.Products.SingleOrDefaultAsync(p => p.id == id);
            if (product == null)
            {
                return Content(HttpStatusCode.NotFound, "Không tìm thấy sản phẩm!");
            }

            return Ok(product);
        }

        // PUT: api/ProductApi/update/{id}
        [HttpPut]
        [Route("api/ProductApi/update/{id}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> UpdateProduct(string id, Product product)
        {
            if (id != product.id)
            {
                return Content(HttpStatusCode.BadRequest, "ID sản phẩm không hợp lệ!");
            }

            db.Entry(product).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return Content(HttpStatusCode.NotFound, "Không tìm thấy sản phẩm!");
                }
                else
                {
                    throw;
                }
            }

            return Content(HttpStatusCode.OK, db.Products.SingleOrDefault(n => n.id == product.id));
        }

        // POST: api/ProductApi/create
        [HttpPost]
        [Route("api/ProductApi/create")]
        [ResponseType(typeof(Product))]
        public async Task<IHttpActionResult> CreateProduct(Product product)
        {
            if(db.Products.SingleOrDefault(n=>n.id == product.id) != null)
            {
                return Content(HttpStatusCode.Created, "Id đã tồn tại");
            }
            db.Products.Add(product);
            await db.SaveChangesAsync();

            return Content(HttpStatusCode.OK, db.Products.SingleOrDefault(n => n.id == product.id));
        }

        // DELETE: api/ProductApi/delete/{id}
        [HttpDelete]
        [Route("api/ProductApi/delete/{id}")]
        [ResponseType(typeof(Product))]
        public async Task<IHttpActionResult> DeleteProduct(string id)
        {
            var product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return Content(HttpStatusCode.NotFound, "Không tìm thấy sản phẩm!");
            }

            db.Products.Remove(product);
            await db.SaveChangesAsync();

            return Content(HttpStatusCode.OK,"Xoá sản phẩm thành công !");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductExists(string id)
        {
            return db.Products.Count(e => e.id == id) > 0;
        }
    }
}
