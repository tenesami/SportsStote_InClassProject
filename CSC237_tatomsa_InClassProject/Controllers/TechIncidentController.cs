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
        private SportsProContext context { get; set; }
        
        public TechIncidentController(SportsProContext ctx)
        {
            context = ctx;
        }
        [HttpGet]
        public IActionResult Get() //Desplay the list of technicians used for drop down
            //& save it to viewBag.Technician
        {
            ViewBag.Technicians = context.Technicians
                 .OrderBy(c => c.Name)
                 .ToList();

            //Check when the page is obtaind if there is a technican ID has passed
            //into z session object and grap the techID if it is not one their we will
            //have it as null because int? is nulluble type  
            int? techID = HttpContext.Session.GetInt32("techID");
            Technician technician; //create empty technician

            if (techID == null)
            { //if it is null we intanciate new technician
                technician = new Technician();
            }
            else
            {//go to Db context and grap from technicianId and grap z frist and default one.
                technician = context.Technicians
                    .Where(t => t.TechnicianID == techID)
                    .FirstOrDefault();
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
                Technician = context.Technicians.Find(id),
                Incidents = context.Incidents
                    .Include(i => i.Customer)
                    .Include(i => i.Product)
                    .OrderBy(i => i.DateOpened)
                    .Where(i => i.TechnicianID == id)
                    .Where(i => i.DateClosed == null)
                    .ToList()
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            int? techID = HttpContext.Session.GetInt32("techID"); //check and get the tech id 
            var model = new TechIncidentViewModel
            {
                Technician = context.Technicians.Find(techID),

                Incident = context.Incidents
                    .Include(i => i.Customer)
                    .Include(i => i.Product)
                    .FirstOrDefault(i => i.IncidentID == id)
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(IncidentViewModel model)
        {
            Incident i = context.Incidents.Find(model.Incident.IncidentID);
            i.Description = model.Incident.Description;
            i.DateClosed = model.Incident.DateClosed;

            context.Incidents.Update(i);
            context.SaveChanges();

            int? techID = HttpContext.Session.GetInt32("techID");
            return RedirectToAction("List", new { id = techID });
        } 
    }
}
