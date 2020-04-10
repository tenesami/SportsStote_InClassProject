using CSC237_tatomsa_InClassProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSC237_tatomsa_InClassProject.ViewModels
{
    public class TechIncidentViewModel
    {
        public Technician Technician { get; set; }
        public Incident Incident { get; set; }
        public IEnumerable<Incident> Incidents { get; set; }
    }
}
