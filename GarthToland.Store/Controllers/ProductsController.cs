using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GarthToland.Store.Models;
using GarthToland.Store.Services;
using Microsoft.AspNetCore.Mvc;

namespace GarthToland.Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // GET api/products
        [HttpGet]
        public ActionResult<IEnumerable<Product>> Index()
        {
            var products = _productService.GetProducts();

            return products;
        }
    }
}