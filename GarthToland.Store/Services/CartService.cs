using GarthToland.Store.Models;
using System.Collections.Generic;
using System.Linq;

namespace GarthToland.Store.Services
{
    public interface ICartService
    {
        List<Product> GetProducts();
        double GetTotalPrice();
        void Add(Product product);
        void Delete(int id);
    }

    public class CartService : ICartService
    {
        private readonly string DATA_FILE_NAME = "Data/cart.json";
        private readonly IDataFileService _dataFileService;

        public CartService(IDataFileService dataFileService)
        {
            _dataFileService = dataFileService;
        }

        public List<Product> GetProducts()
        {
            var cartItems = _dataFileService.GetObjectFromJsonFile<Cart>(DATA_FILE_NAME);

            return cartItems.Products;
        }

        public double GetTotalPrice()
        {
            var products = GetProducts();

            var total = products.Sum(p => p.Price);

            return total;
        }

        public void Add(Product product)
        {
            var products = GetProducts();

            var hasProduct = products.Where(p => p.Id == product.Id).Count() > 0;

            if (product.Quantity != 0)
            {
                products.Add(product);

                var cartObject = new Cart()
                {
                    Products = products
                };

                _dataFileService.WriteObjectToFile(DATA_FILE_NAME, cartObject);
            }
        }

        public void Delete(int id)
        {
            var products = GetProducts();

            products.RemoveAll(p => p.Id == id);

            var cartObject = new Cart()
            {
                Products = products
            };

            _dataFileService.WriteObjectToFile(DATA_FILE_NAME, cartObject);
        }

        public void AddVoucher(string name)
        {
            var voucher = new Voucher { Name = name };

            _dataFileService.WriteObjectToFile(DATA_FILE_NAME, voucher);
        }
    }
}
