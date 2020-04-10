using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSC237_tatomsa_InClassProject.Models;
using CSC237_tatomsa_InClassProject.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CSC237_tatomsa_InClassProject.Controllers
{
    public class RegistrationController : Controller
    {
        private SportsProContext context{ get; set; }
        
        public RegistrationController(SportsProContext ctx) => context = ctx;

        public IActionResult GetCustomer()
        {
            ViewBag.Customers = context.Customers
                .OrderBy(c => c.LastName)
                .ToList();

            int? custID = HttpContext.Session.GetInt32("custID");
            Customer customer;
            if (custID == null || custID == 0)
                customer = new Customer();
            else
                customer = context.Customers.Find(custID);
                       
            return View(customer);
        }
        
        [HttpPost]
        public IActionResult List(Customer customer)
        {
            HttpContext.Session.SetInt32("custID", customer.CustomerID);
            if(customer.CustomerID == 0)
            {
                TempData["message"] = "You must select a Customer.";
                return RedirectToAction("GetCustomer");                
            }
            else
            {
                return RedirectToAction("List", new { Id = customer.CustomerID });
            }            
        }
        [HttpGet]
        public IActionResult List(int id)
        {
            RegistrationViewModel model = new RegistrationViewModel
            { 
              CustomerID = id,
              Customer = context.Customers.Find(id),
              Products = context.Products
              .OrderBy(p => p.Name)
              .ToList(),
              Registrations = context.Registrations
              .Include(r => r.Customer)
              .Include(r => r.Product)
              .Where(r => r.CustomerID == id)
              .ToList()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(int customerID, int productID )
        {
            Registration registration = new Registration
            {
                CustomerID = customerID, 
               ProductID = productID
            };
            context.Registrations.Remove(registration);
            context.SaveChanges();
           return RedirectToAction("List", new { ID = customerID });           
        }


        [HttpPost]
        public IActionResult Register(RegistrationViewModel model)
        {
            if(model.ProductID == 0)
            {
                TempData["message"] = "You must select a product.";
            }
            else
            {
                Registration registration = new Registration
                {
                    CustomerID = model.CustomerID,
                    ProductID = model.ProductID
                };
                context.Registrations.Add(registration);
                try
                {
                    context.SaveChanges();
                }
                catch(DbUpdateException ex)
                {
                    string msg = (ex.InnerException == null) ? ex.Message : ex.InnerException.Message;
                    if (msg.Contains("duplicate key"))
                        TempData["message"] = "this product is already registroed to this customer";
                    else
                        TempData["message"] = "Error accessing the database " + msg;
                }
            }
            return RedirectToAction("List", new { ID = model.CustomerID });
        }

    }
}
