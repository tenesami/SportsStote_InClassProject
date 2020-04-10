using CSC237_tatomsa_InClassProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSC237_tatomsa_InClassProject.ViewModels
{
    public class IncidentListViewModel
    {
        public string Filter { get; set; }
        public IEnumerable<Incident> Incidents { get; set; }
    }
}
