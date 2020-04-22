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
    public class ValidationController : Controller
    {
        private Repository<Customer> data { get; set; }
        public ValidationController(SportsProContext ctx) => data = new Repository<Customer>(ctx);

        public JsonResult CheckEmail(string emailAddress, int customerID)
        {
            if(customerID == 0) //Only check for new customers - don't check on edit
            {
                string msg = Check.EmailExists(data, emailAddress);
                if(!string.IsNullOrEmpty(msg))
                {
                    return Json(msg);
                }
            }
                TempData["OkEmail"] = true;
                return Json(true);
        }
    }
}
