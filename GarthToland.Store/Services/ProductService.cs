using GarthToland.Store.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace GarthToland.Store.Services
{
    public interface IProductService
    {
        List<Product> GetProducts();
    }

    public class ProductService : IProductService
    {
        private readonly string DATA_FILE_NAME = "Data/products.json";

        public List<Product> GetProducts()
        {
            using (StreamReader r = new StreamReader(DATA_FILE_NAME))
            {
                string json = r.ReadToEnd();
                var cartItems = JsonConvert.DeserializeObject<Cart>(json);

                return cartItems.Products;                
            }
        }
    }
}
