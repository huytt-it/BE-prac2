using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.MongoDbCollections;
using Services.Services;

namespace BE_prac2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductServices _productServices;

        public ProductController(IProductServices productServices)
        {
            _productServices = productServices;
        }


        [HttpGet]
        public ActionResult<List<Product>> Get() =>
            _productServices.Get();

        [HttpGet("{id:length(24)}", Name = "GetProduct")]
        public ActionResult<Product> Get(string id)
        {
            var product = _productServices.Get(id);
            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        [HttpPost]
        public ActionResult CreateProduct(Product inProduct)
        {
            _productServices.CreateProduct(inProduct);
            return NoContent();
        }

        [HttpPut("{id:length(24)}")]
        public ActionResult UpdateProduct(string id, Product inProduct)
        {
            var product = _productServices.Get(id);
            if (product == null)
            {
                return NotFound();
            }
            _productServices.UpdateProduct(id, inProduct);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public ActionResult DeleteProduct(string id)
        {
            var product = _productServices.Get(id);
            if (product == null)
            {
                return NotFound();
            }
            _productServices.DeleteProduct(id);
            return NoContent();
        }
    }
}
