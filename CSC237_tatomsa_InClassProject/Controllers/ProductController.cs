using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSC237_tatomsa_InClassProject.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CSC237_tatomsa_InClassProject.Controllers
{
    public class ProductController : Controller
    {
        private SportsProContext context;
        public ProductController(SportsProContext ctx)
        {
            context = ctx;
        }

        [Route("products")]
        public ViewResult List()
        {
            ViewBag.Title = "Product List";
            List<Product> productList = context.Products.ToList();

            return View(productList);
        }

        [HttpGet]
        public ViewResult Add()
        {
            ViewBag.Action = "Add";
            return View("AddEdit", new Product());
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            var product = context.Products.Find(id);
            return View("AddEdit", product);
        }

        [HttpGet]
        public ViewResult Delete(int id)
        {
            var product = context.Products.Find(id);
            return View(product);
        }

        [HttpPost]
        public RedirectToActionResult Delete(Product product)
        {
            context.Products.Remove(product);
            context.SaveChanges();

            return RedirectToAction("List");
        }

        [HttpPost]
        public IActionResult Save(Product product)
        {
            string message;
            if (product.ProductID == 0)
            {
                ViewBag.Action = "Add";
            }
            else
            {
                ViewBag.Action = "Edit";
            }

            if (ModelState.IsValid)
            {
                if (ViewBag.Action == "Add")
                {
                    message = product.Name + " was added.";
                    context.Products.Add(product);
                }
                else
                {
                    message = product.Name + " was updated.";
                    context.Products.Update(product);
                }
                context.SaveChanges();
                TempData["message"] = message;
                return RedirectToAction("List");
            }
            else
            {
                return View("AddEdit", product);
            }

        }

    }


}
