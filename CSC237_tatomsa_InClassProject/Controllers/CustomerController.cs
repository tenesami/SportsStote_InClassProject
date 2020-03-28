using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSC237_tatomsa_InClassProject.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CSC237_tatomsa_InClassProject.Controllers
{
    public class CustomerController : Controller
    {
        private SportsProContext context { get; set; }
        public CustomerController(SportsProContext ctx)
        {
            context = ctx;
        }

        [Route("customers")]
        public IActionResult List()
        {
            //buid query expression 
            List<Customer> customers = context.Customers
                .OrderBy(c => c.LastName).ToList();
            return View(customers);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";

            ViewBag.Countries = context.Countries.ToList();

            return View("AddEdit", new Customer());
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";

            ViewBag.Countries = context.Countries.ToList();

            var customer = context.Customers.Find(id); //use customer add to the delete
            return View("AddEdit");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var customer = context.Customers.Find(id);
            return View(customer);
        }

        [HttpPost]
        public IActionResult Delete(Customer customer)
        {
            context.Customers.Remove(customer);
            context.SaveChanges();
            return RedirectToAction("List");
        }
        [HttpPost]
        public IActionResult Save(Customer customer)
        {
            if (customer.CustomerID == 0)
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
                    context.Customers.Add(customer);
                }
                else
                {
                    context.Customers.Update(customer);
                }
                context.SaveChanges();
                return RedirectToAction("List");
            }
            else
            {
                ViewBag.Coutries = context.Countries.ToList();
                return View("AddEdit", customer);
            }

        }

    }
}