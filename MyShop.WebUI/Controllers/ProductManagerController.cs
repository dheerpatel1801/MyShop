using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using MyShop.DataAccess.InMemory;


namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        ProductRepository context;
        ProductCategoryRepository ProductCategories;

        public ProductManagerController()
        {
            context = new ProductRepository();
            ProductCategories = new ProductCategoryRepository();
        }
        // GET: ProductManager
        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();
            return View(products); 
        }
        public ActionResult Create()
        {
            ProductManagerViewModel ViewModel = new ProductManagerViewModel();
            ViewModel.Product = new Product();
            ViewModel.ProductCategories = ProductCategories.Collection();
            return View(ViewModel);
        }
        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                context.Insert(product);
                context.Commit();
                return RedirectToAction("Index");
            }
        }
        public ActionResult Edit(string Id)
        {
            Product product = context.Find(Id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                ProductManagerViewModel ViewModel = new ProductManagerViewModel();
                ViewModel.Product = new Product();
                ViewModel.ProductCategories = ProductCategories.Collection();
                return View(ViewModel);
            }
        }
        [HttpPost]
        public ActionResult Edit(Product product, string Id)
        {
            Product Editproduct = context.Find(Id);
            if (Editproduct == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(product);
                }
                Editproduct.Name = product.Name;
                Editproduct.Description = product.Description;
                Editproduct.Price = product.Price;
                Editproduct.Category = product.Category;
                Editproduct.Image = product.Image;
                context.Commit();
                return RedirectToAction("Index");
            }
        }
        public ActionResult Delete(String Id)
        {
            Product Deleteproduct = context.Find(Id);
            if (Deleteproduct == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(Deleteproduct);
            }
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(String Id)
        {
            Product Deleteproduct = context.Find(Id);
            if (Deleteproduct == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(Id);
                context.Commit();
                return RedirectToAction("Index");
            }
        }
    }
}