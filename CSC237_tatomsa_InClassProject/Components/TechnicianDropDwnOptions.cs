using CSC237_tatomsa_InClassProject.DataLayer;
using CSC237_tatomsa_InClassProject.Models;
using CSC237_tatomsa_InClassProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSC237_tatomsa_InClassProject.Components
{
    public class TechnicianDropDwnOptions : ViewComponent
    {       
        public IRepository<Technician> data { get; set; }
        public TechnicianDropDwnOptions(IRepository<Technician> rep)
        {
            data = rep;
        }
        public IViewComponentResult Invoke(string value, string defaultText, string defaultValue)
        {
            var techs = data.List(new QueryOptions<Technician>
            { 
                OrderBy = t => t.Name
            });
            var vm = new DropDownOptionsViewModel
            {
                SelectdValue = value,
                DefaultValue =  defaultValue,
                DefaultText = defaultText,
                Items = techs.ToDictionary(
                    t => t.TechnicianID.ToString(), t => t.Name)               
            };
            return View(DropdownOptions.PartialViewPath, vm);
        }
    }
}
