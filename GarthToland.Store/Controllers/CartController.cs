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
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> Index()
        {
            var products = _cartService.GetProducts();

            return products;
        }

        [HttpPost("Add")]
        public ActionResult Add(Product product)
        {
            if (product.Quantity == 0)
            {
                return Json("Cannot add product as quantity is 0.");
            }

            _cartService.Add(product);

            return Ok();
        }

        [HttpDelete("Delete")]
        public ActionResult Delete(int id)
        {
            _cartService.Delete(id);

            return Ok();
        }

        [HttpGet("Total")]
        public ActionResult Total()
        {
            var total = _cartService.GetTotalPrice();

            return Json(total);
        }
    }
}