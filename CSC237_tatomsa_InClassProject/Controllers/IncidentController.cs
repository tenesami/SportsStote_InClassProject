using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSC237_tatomsa_InClassProject.Models;
using CSC237_tatomsa_InClassProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CSC237_tatomsa_InClassProject.Controllers
{
    public class IncidentController : Controller
    {
        private SportsProContext context;

        public IncidentController(SportsProContext ctx)
        {
            context = ctx;
        }

        [Route("incidents")]
        public IActionResult List(string filter = "all")
        {
            IncidentListViewModel model = new IncidentListViewModel
            {               
                Filter = filter
            };
            IQueryable<Incident> query = context.Incidents
                .Include(i => i.Customer)
                .Include(i => i.Product)
                .OrderBy(i => i.DateOpened);
            if (filter == "unassigned")
                query = query.Where(i => i.TechnicianID == null);

            if (filter == "Open")
                query = query.Where(i => i.DateClosed == null);

            List<Incident> incidents = query.ToList();
            model.Incidents = incidents;

            return View(model);
        }
        public IActionResult Filter (string id)
        {
            return RedirectToAction("List", new { Filter = id });
        }
        //Helper method
        private IncidentViewModel GetViewModel()
        {
            IncidentViewModel model = new IncidentViewModel
            {
                Customers = context.Customers
                .OrderBy(c => c.FirstName)
                .ToList(),

                Products = context.Products
                    .OrderBy(c => c.Name)
                    .ToList(),

                  Technicians = context.Technicians.
                        OrderBy(c => c.Name)
                        .ToList()
            };

            return model;
        }
            
        [HttpGet]
        public IActionResult Add()
        {
            IncidentViewModel model = GetViewModel();
            model.Incident = new Incident();
            model.Action = "Add";

            return View("AddEdit", model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            IncidentViewModel model = GetViewModel();
            var incident = context.Incidents.Find(id);
            model.Incident = incident;
            model.Action = "Edit";          

            return View("AddEdit", model);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var incident = context.Incidents.Find(id);
            return View(incident);
        }

        [HttpPost]
        public IActionResult Delete(Incident incident)
        {
            context.Incidents.Remove(incident);
            context.SaveChanges();
            return RedirectToAction("List");
        }

        [HttpPost]
        public IActionResult Save(Incident incident)
        {
            if (incident.CustomerID == 0)
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
                    context.Incidents.Add(incident);
                }
                else
                {
                    context.Incidents.Update(incident);
                }
                context.SaveChanges();
                return RedirectToAction("List");
            }
            else
            {
                
                return View("AddEdit", incident);
            }

        }
    }
}