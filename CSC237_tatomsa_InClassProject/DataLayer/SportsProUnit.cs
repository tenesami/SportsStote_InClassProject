using CSC237_tatomsa_InClassProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSC237_tatomsa_InClassProject.DataLayer
{
    public class SportsProUnit : ISportsProUnit
    {
        private SportsProContext context { get; set; }
        public SportsProUnit(SportsProContext ctx) => context = ctx;
        
        private Repository<Product> productRepo;
        public Repository<Product> Products
        {
            get
            {
                if (productRepo == null)
                    productRepo = new Repository<Product>(context);
                return productRepo;
            }
        }

        private Repository<Technician> technicianRepo;
        public Repository<Technician> Technicians
        {
            get
            {
                if (technicianRepo == null)
                    technicianRepo = new Repository<Technician>(context);
                return technicianRepo;
            }
        }

        private Repository<Customer> customerRepo;
        public Repository<Customer> Customers
        {
            get
            {
                if (customerRepo == null)
                    customerRepo = new Repository<Customer>(context);
                return customerRepo;
            }
        }

        private Repository<Country> countryRepo;
        public Repository<Country> Countries
        {
            get
            {
                if (countryRepo == null)
                    countryRepo = new Repository<Country>(context);
                return countryRepo;
            }
        }

        private Repository<Registration> registrationRepo;
        public Repository<Registration> Registrations
        {
            get
            {
                if (registrationRepo == null)
                    registrationRepo = new Repository<Registration>(context);
                return registrationRepo;
            }
        }

        private Repository<Incident> incidentRepo;
        public Repository<Incident> Incidents
        {
            get
            {
                if (incidentRepo == null)
                    incidentRepo = new Repository<Incident>(context);
                return incidentRepo;
            }
        }

        public void Save() => context.SaveChanges();
    }
}
