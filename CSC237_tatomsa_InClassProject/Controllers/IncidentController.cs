using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSC237_tatomsa_InClassProject.DataLayer;
using CSC237_tatomsa_InClassProject.Models;
using CSC237_tatomsa_InClassProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CSC237_tatomsa_InClassProject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class IncidentController : Controller
    {
        private IRepository<Incident> data { get; set; }

        public IncidentController(IRepository<Incident> rep)
        {
            data = rep;
        }

        [Route("[controller]s")]
        public IActionResult List(string filter = "all")
        {
            IncidentListViewModel model = new IncidentListViewModel
            {
                Filter = filter
            };
            var options = new QueryOptions<Incident>
            {
                Includes = "Customer, Product",
                OrderBy = i => i.DateOpened
            };


            if (filter == "unassigned")
                options.Where = i => i.TechnicianID == null;

            if (filter == "Open")
                options.Where = i => i.DateClosed == null;

            IEnumerable<Incident> incidents = data.List(options);
            model.Incidents = incidents;

            return View(model);
        }
        public IActionResult Filter(string id)
        {
            return RedirectToAction("List", new { Filter = id });
        }
        
        [HttpGet]
        public IActionResult Add()
        {
            IncidentViewModel model = new IncidentViewModel
            {
                Incident = new Incident(),
                Action = "Add"
            };

            return View("AddEdit", model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            IncidentViewModel model = new IncidentViewModel
            {
                Incident = data.Get(id),
                Action = "Edit"
            };

            return View("AddEdit", model);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var incident = data.Get(id);
            return View(incident);
        }

        [HttpPost]
        public IActionResult Delete(Incident incident)
        {
            data.Delete(incident);
            data.Save();
            return RedirectToAction("List");
        }

        [HttpPost]
        public IActionResult Save(Incident incident)
        {
            if (ModelState.IsValid)
            {
                if (incident.IncidentID == 0)
                {
                    data.Insert(incident);
                }
                else
                {
                    data.Update(incident);
                }
                data.Save();
                return RedirectToAction("List");
            }
            else
            {
                IncidentViewModel model = new IncidentViewModel
                {
                    Incident = incident
                };
                if (incident.IncidentID == 0)
                {
                    model.Action = "Add";
                }
                else
                {
                    model.Action = "Edit";
                }
                return View("AddEdit", model);

            }

        }
    }
}


