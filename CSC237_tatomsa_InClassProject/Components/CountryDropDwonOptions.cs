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
    public class CountryDropDwonOptions : ViewComponent
    {
        private IRepository<Country> data { get; set; }
        public CountryDropDwonOptions(IRepository<Country> rep) => data = rep;
        public IViewComponentResult Invoke(string value, string defaultText, string defaultValue)
        {
            var countries = data.List(new QueryOptions<Country>
            {
                OrderBy = c => c.Name
            });
            var vm = new DropDownOptionsViewModel
            { 
                SelectdValue = value,
                DefaultValue = defaultValue,
                DefaultText = defaultText,
                Items = countries.ToDictionary(
                    c => c.CountryID, c => c.Name)
            };
            return View(DropdownOptions.PartialViewPath, vm);
        }
    }
}
