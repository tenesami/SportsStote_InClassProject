using CSC237_tatomsa_InClassProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSC237_tatomsa_InClassProject.DataLayer
{
    public interface ISportsProUnit
    {
        IRepository<Product> Products { get; }
        IRepository<Technician> Technicians { get; }
        IRepository<Customer> Customers { get; }
        IRepository<Country> Countries { get; }
        IRepository<Registration> Registrations { get; }
        IRepository<Incident> Incidents { get; }

        void Save();
    }
}
