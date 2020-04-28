using CSC237_tatomsa_InClassProject.DataLayer;
using CSC237_tatomsa_InClassProject.Models;
using CSC237_tatomsa_InClassProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CSC237_tatomsa_InClassProject.Components
{
    public class CustomerDropDownOptions : ViewComponent
    {
        private IRepository<Customer> data { get; set; }
        public CustomerDropDownOptions(IRepository<Customer> rep) => data = rep;

        public IViewComponentResult Invoke(string value, string defaultText, string defaultValue)
        {
            var customers = data.List(new QueryOptions<Customer>
            {
                OrderBy = c => c.FirstName
            });
            var vm = new DropDownOptionsViewModel
            {
                SelectdValue = value,
                DefaultValue = defaultValue,
                DefaultText = defaultText,
                Items = customers.ToDictionary(
                    p => p.CustomerID.ToString(), p => p.FullName)
            };
            return View(DropdownOptions.PartialViewPath, vm);
        }
    }
}
