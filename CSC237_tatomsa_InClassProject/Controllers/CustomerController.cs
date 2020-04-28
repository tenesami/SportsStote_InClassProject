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
    public class CustomerController : Controller
    {
        private IRepository<Customer> data { get; set; }
        public CustomerController(IRepository<Customer> repo)
        {
            data = repo;
        }

        [Route("customers")]
        public IActionResult List()
        {
            var customers = data.List(new QueryOptions<Customer>
            {
                OrderBy = c => c.LastName
            }); 
            
            return View(customers);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
          
            return View("AddEdit", new Customer());
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
          
            var customer = data.Get(id);
            return View("AddEdit", customer);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var customer = data.Get(id);

            return View(customer);
        }

        [HttpPost]
        public IActionResult Delete(Customer customer)
        {
            data.Delete(customer);
            data.Save();
            return RedirectToAction("List");
        }

        [HttpPost]
        public IActionResult Save(Customer customer)
        {
            if (customer.CountryID == "xx")
            {
                ModelState.AddModelError(nameof(Customer.CountryID), "Required");
            }

            if (customer.CustomerID == 0 && TempData["okEmail"] == null) //Only check new customer dosen't check on edit
            {
                string msg = Check.EmailExists(data, customer.Email);
                if (!string.IsNullOrEmpty(msg))
                {
                    ModelState.AddModelError(nameof(Customer.Email), msg);
                }
            }

            if (ModelState.IsValid)
            {
                if (customer.CustomerID == 0)
                {
                    data.Insert(customer);
                }
                else
                {
                    data.Update(customer);
                }
                data.Save();
                return RedirectToAction("List");
            }
            else
            {
                if(customer.CustomerID == 0)
                {
                    ViewBag.Action = "Add";
                }
                else
                {
                    ViewBag.Action = "Edit"; 

                }
                return View("AddEdit", customer);
            }
        }       
    }
}