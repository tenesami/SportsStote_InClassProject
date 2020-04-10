using CSC237_tatomsa_InClassProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSC237_tatomsa_InClassProject.ViewModels
{
    public class IncidentViewModel
    {
        public Incident Incident { get; set; }
        public string Action { get; set; }
        public IEnumerable<Customer> Customers { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Technician> Technicians { get; set; }

    }
}
