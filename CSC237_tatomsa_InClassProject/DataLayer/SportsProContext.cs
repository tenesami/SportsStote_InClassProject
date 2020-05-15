using CSC237_tatomsa_InClassProject.Configurations;
using CSC237_tatomsa_InClassProject.DataLayer.SeedData;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSC237_tatomsa_InClassProject.Models
{
    public class SportsProContext : IdentityDbContext<User>
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

        protected override void OnModelCreating(ModelBuilder modelbuilder) //compasit key Entitiyframe work core //composite key in entity framework core                                                                          
        {
            base.OnModelCreating(modelbuilder);

            //to tell the primery key
            modelbuilder.ApplyConfiguration(new SeedProduct());

            modelbuilder.ApplyConfiguration(new SeedTechnician());

            modelbuilder.ApplyConfiguration(new SeedCountries());

            modelbuilder.ApplyConfiguration(new SeedCustomers());

            modelbuilder.ApplyConfiguration(new SeedIncidents());

            modelbuilder.ApplyConfiguration(new SeedRegistrations());

            //Many to many relationship for Registratiom table
            modelbuilder.ApplyConfiguration(new RegistrationConfig());
        }

        public static async Task CreateAdminUser(IServiceProvider serviceProvider)
        {
            UserManager<User> userManager =
                serviceProvider.GetRequiredService<UserManager<User>>();
            RoleManager<IdentityRole> roleManager =
                serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string username = "admin";
            string password = "P@ssw0rd";
            string roleName = "Admin";

            //if Role Dosen't exites create it
            if (await roleManager.FindByNameAsync(roleName)== null)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }

            //If username doesn't exit, create it and add it to role
            if (await userManager.FindByNameAsync(username) == null)
            {
                User user = new User { UserName = username };
                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, roleName);
                }
            }
        }
    }
}