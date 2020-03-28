using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSC237_tatomsa_InClassProject.Models
{
    public interface IIncidentRepository
    {

        IEnumerable<Incident> GetIncidents { get; }

        Incident GetIncidentById(int incidentId);
    }
}
