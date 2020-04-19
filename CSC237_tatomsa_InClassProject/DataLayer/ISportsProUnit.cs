using CSC237_tatomsa_InClassProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSC237_tatomsa_InClassProject.DataLayer
{
    public interface ISportsProUnit 
    {
        Repository<Product> Products { get; }
        Repository<Technician> Technicians { get; }
        Repository<Customer> Customers { get; }
        Repository<Country> Countries { get; }
        Repository<Registration> Registrations { get; }
        Repository<Incident> Incidents { get;  }

        void Save();
    }
}
