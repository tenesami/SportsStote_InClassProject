using CSC237_tatomsa_InClassProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSC237_tatomsa_InClassProject.DataLayer.SeedData
{
    public class SeedRegistrations : IEntityTypeConfiguration<Registration>
    {
        public void Configure(EntityTypeBuilder<Registration> builder)
        {
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
