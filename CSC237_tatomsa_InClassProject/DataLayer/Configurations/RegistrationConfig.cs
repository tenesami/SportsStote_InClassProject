using CSC237_tatomsa_InClassProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSC237_tatomsa_InClassProject.Configurations
{
    public class RegistrationConfig : IEntityTypeConfiguration<Registration>
    {
        public void Configure(EntityTypeBuilder<Registration> builder)
        {
            /*
            Many-to-many relationship for registration table
            */

            //Compasite primery key           
            builder.HasKey(r => new { r.CustomerID, r.ProductID });

            //One to many relationship b/n customer and registration

            builder.HasOne(r => r.Customer)
                .WithMany(c => c.Registrations)
                .HasForeignKey(r => r.CustomerID);

            //One to many relationship b/n Product and registration

            builder.HasOne(r => r.Product)
                .WithMany(c => c.Registrations)
                .HasForeignKey(r => r.ProductID);

            builder.HasData(
               new Registration
               {
                   CustomerID = 1002,
                   ProductID = 1
               },
               new Registration
               {
                   CustomerID = 1002,
                   ProductID = 3
               },
               new Registration
               {
                   CustomerID = 1010,
                   ProductID = 2
               }

               );

        }
    }
}
