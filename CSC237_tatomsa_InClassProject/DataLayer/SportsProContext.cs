using CSC237_tatomsa_InClassProject.Configurations;
using CSC237_tatomsa_InClassProject.DataLayer.SeedData;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSC237_tatomsa_InClassProject.Models
{
    public class SportsProContext : DbContext
    {
        public SportsProContext(DbContextOptions<SportsProContext> options)
            : base(options)
        { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Technician> Technicians { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Incident> Incidents { get; set; }
        public DbSet<Registration> Registrations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) //compasit key Entitiyframe work core //composite key in entity framework core                                                                          
        {                                                                   //to tell the primery key
            modelBuilder.ApplyConfiguration(new SeedProduct());

            modelBuilder.ApplyConfiguration(new SeedTechnician());

            modelBuilder.ApplyConfiguration(new SeedCountries());

            modelBuilder.ApplyConfiguration(new SeedCustomers());

            modelBuilder.ApplyConfiguration(new SeedIncidents());
           
            modelBuilder.ApplyConfiguration(new SeedRegistrations());

            //Many to many relationship for Registratiom table
            modelBuilder.ApplyConfiguration(new RegistrationConfig());
        }
    }
}