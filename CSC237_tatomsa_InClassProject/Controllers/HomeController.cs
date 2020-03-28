using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CSC237_tatomsa_InClassProject.Models;

namespace CSC237_tatomsa_InClassProject.Controllers
{
    public class HomeController : Controller
    {    
        //To specifay the root of the View application 
        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }
        //attribute route override the default routing system
        //that specified in the Startup page
        [Route("about")]
        public IActionResult About()
        {
            return View();
        }
    }
}
