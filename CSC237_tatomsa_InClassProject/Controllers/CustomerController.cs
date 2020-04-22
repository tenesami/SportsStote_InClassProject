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
        private ISportsProUnit data { get; set; }
        public CustomerController(ISportsProUnit unit)
        {
            data = unit;
        }

        [Route("customers")]
        public IActionResult List()
        {
            var customers = data.Customers.List(new QueryOptions<Customer>
            {
                OrderBy = c => c.LastName
            }); 
            
            return View(customers);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";

            ViewBag.Countries = GetCountryList();

            return View("AddEdit", new Customer());
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";

            ViewBag.Countries = GetCountryList();

            var customer = data.Customers.Get(id);
            return View("AddEdit", customer);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var customer = data.Customers.Get(id);

            return View(customer);
        }

        [HttpPost]
        public IActionResult Delete(Customer customer)
        {
            data.Customers.Delete(customer);
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
                string msg = Check.EmailExists(data.Customers, customer.Email);
                if (!string.IsNullOrEmpty(msg))
                {
                    ModelState.AddModelError(nameof(Customer.Email), msg);
                }
            }

            if (ModelState.IsValid)
            {
                if (customer.CustomerID == 0)
                {
                    data.Customers.Insert(customer);
                }
                else
                {
                    data.Customers.Update(customer);
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
        //Private Helper method 
        IEnumerable<Country> GetCountryList() =>
            data.Countries.List(new QueryOptions<Country>
            { 
                OrderBy = c => c.Name
            });

    }
}