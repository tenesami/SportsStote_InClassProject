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
    public class ProductDropDownOptions : ViewComponent
    {
        private IRepository<Product> data { get; set; }
        public ProductDropDownOptions(IRepository<Product> rep) => data = rep;

        public IViewComponentResult Invoke(string value, string defaultText, string defaultValue)
        {
            var products = data.List(new QueryOptions<Product>
            {
                OrderBy = p => p.Name
            });
            var vm = new DropDownOptionsViewModel
            {
                SelectdValue = value,
                DefaultValue = defaultValue,
                DefaultText = defaultText,
                Items = products.ToDictionary(
                    p => p.ProductID.ToString(), p => p.Name)
            };
            return View(DropdownOptions.PartialViewPath, vm);
        }
    }
}
