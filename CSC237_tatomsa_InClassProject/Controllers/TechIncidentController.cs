using CSC237_tatomsa_InClassProject.DataLayer;
using CSC237_tatomsa_InClassProject.Models;
using CSC237_tatomsa_InClassProject.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSC237_tatomsa_InClassProject.Controllers
{
    public class TechIncidentController : Controller            
    {
        private SportsProUnit data { get; set; }
        
        public TechIncidentController(SportsProContext ctx)
        {
            data = new SportsProUnit(ctx);
        }
        [HttpGet]
        public IActionResult Get() 
        {
            ViewBag.Technicians = data.Technicians.List(new QueryOptions<Technician>
            {
                OrderBy = c => c.Name
            });

            int techID = HttpContext.Session.GetInt32("techID") ?? 0;
            Technician technician; //create empty technician

            if (techID == 0)
            { //if it is null we intanciate new technician
                technician = new Technician();
            }
            else
            {//go to Db context and grap from technicianId and grap z frist and default one.
                technician = data.Technicians.Get(techID);
                    
            }
            return View(technician);
        }

        [HttpPost]
        public IActionResult List(Technician technician)
        {
            HttpContext.Session.SetInt32("techID", technician.TechnicianID);
            
        // check technician are selected  
            if(technician.TechnicianID == 0)
            {
                TempData["message"] = "You must select a techician.";
                return RedirectToAction("Get");
            }
            else
            {
                return RedirectToAction("List", new { id = technician.TechnicianID});
            }               
        }
            
        //To recive list view and desplay the information
        [HttpGet]
        public IActionResult List(int id)
        {
            var model = new TechIncidentViewModel
            {
                Technician = data.Technicians.Get(id),
                Incidents = data.Incidents.List(new QueryOptions<Incident>
                {
                    Includes = "Customer, Product",
                    OrderBy = i => i.DateOpened,
                    WhereClauses = new WhereClauses<Incident>
                    {
                        {i => i.TechnicianID == id },
                        {i => i.DateClosed  == null }
                    }
                })


            };
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            int techID = HttpContext.Session.GetInt32("techID") ?? 0; //check and get the tech id 
            var model = new TechIncidentViewModel
            {
                Technician = data.Technicians.Get(techID),

                Incident = data.Incidents.Get(new QueryOptions<Incident>
                {
                    Includes = "Customer, Product",
                    Where = i => i.IncidentID == id
                })
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(IncidentViewModel model)
        {
            Incident i = data.Incidents.Get(model.Incident.IncidentID);
            i.Description = model.Incident.Description;
            i.DateClosed = model.Incident.DateClosed;

            data.Incidents.Update(i);
            data.Save();

            int? techID = HttpContext.Session.GetInt32("techID");
            return RedirectToAction("List", new { id = techID });
        } 
    }
}
