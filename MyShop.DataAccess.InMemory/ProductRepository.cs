using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
 
namespace MyShop.DataAccess.InMemory
{
    public class ProductRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<Product> products;
        
        public ProductRepository()
        {
            products = cache["products"] as List<Product>;
            if(products == null)
            {
                products = new List<Product>();
            }
        }
        public void Commit()
        {
            cache["products"] = products;
        }
        public void Insert(Product p)
        {
            products.Add(p);
        }
        public void Update(Product product)
        {
            Product UpdateProduct = products.Find(p => p.Id == product.Id);
            if(UpdateProduct != null)
            {
                UpdateProduct = product;
            }
            else
            {
                throw new Exception("Product not Found");
            }
        }
        public Product Find(String Id)
        {
            Product product = products.Find(p => p.Id == Id);
            if (product != null)
            {
                 return product;
            }
            else
            {
                throw new Exception("Product not Found");
            }
        }
        public IQueryable<Product> Collection()
        {
            return products.AsQueryable();
        }

        public void Delete(string Id)
        {
            Product DeleteProduct = products.Find(p => p.Id == Id);
            if(DeleteProduct != null)
            {
                products.Remove(DeleteProduct);
            }
            else
            {
                throw new Exception("Product not Found");
            }
        }
    }
}
