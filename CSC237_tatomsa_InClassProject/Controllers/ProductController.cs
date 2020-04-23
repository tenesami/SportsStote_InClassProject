using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSC237_tatomsa_InClassProject.DataLayer;
using CSC237_tatomsa_InClassProject.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CSC237_tatomsa_InClassProject.Controllers
{
    public class ProductController : Controller
    {
        private IRepository<Product> data { get; set; }
        public ProductController(IRepository<Product> rep)
        {
            data = rep;
        }

        [Route("products")]
        public ViewResult List()
        {
            ViewBag.Title = "Product List";

            var products = this.data.List(new QueryOptions<Product>
            { 
                OrderBy = p => p.ReleaseDate
            });

            return View(products);
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
            var product = data.Get(id);
            return View("AddEdit", product);
        }

        [HttpGet]
        public ViewResult Delete(int id)
        {
            var product = data.Get(id);
            return View(product);
        }

        [HttpPost]
        public RedirectToActionResult Delete(Product product)
        {
            data.Delete(product);
            data.Save();

            return RedirectToAction("List");
        }

        [HttpPost]
        public IActionResult Save(Product product)
        {
            string message;
            if (ModelState.IsValid)
            {
                if (product.ProductID == 0)
                {
                    data.Insert(product);
                    message = product.Name + " was added.";
                }
                else
                {
                    data.Update(product);
                    message = product.Name + "was updated.";
                }
                data.Save();
                TempData["message"] = message;
                return RedirectToAction("List");
            }
            else
            {
                if (product.ProductID == 0)
                {
                    ViewBag.Action = "Add";
                }
                else
                {
                    ViewBag.Action = "Edit";
                }
                return View(product);
            }
            
        }

    }


}
