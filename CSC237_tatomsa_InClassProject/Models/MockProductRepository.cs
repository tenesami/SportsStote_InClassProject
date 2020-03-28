using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSC237_tatomsa_InClassProject.Models
{
    public class MockProductRepository : IProductRepository
    {
        // Create temporary list of products
        List<Product> products;

        public MockProductRepository()
        {
            if (products == null)
                InitializeProducts();
        }

        private void InitializeProducts()
        {
            products = new List<Product>
            {
                new Product
                {
                    ProductID = 1,
                    ProductCode = "DRAFT10",
                    Name = "Draft Manager 1.0",
                    YearlyPrice = 4.99M,
                    ReleaseDate = DateTime.Parse("2017-02-01")
                },
                new Product
                {
                    ProductID = 2,
                    ProductCode = "DRAFT20",
                    Name = "Draft Manager 2.0",
                    YearlyPrice = 5.99M,
                    ReleaseDate = DateTime.Parse("2019-07-15 00:00:00.000")
                },
                new Product
                {
                    ProductID = 3,
                    ProductCode = "LEAG10",
                    Name = "League Scheduler 1.0",
                    YearlyPrice = 4.99M,
                    ReleaseDate = DateTime.Parse("2016-05-01 00:00:00.000")
                },
                new Product
                {
                    ProductID = 4,
                    ProductCode = "LEAGD10",
                    Name = "League Scheduler Deluxe 1.0",
                    YearlyPrice = 7.99M,
                    ReleaseDate = DateTime.Parse("2016-08-01 00:00:00.000")
                },
                new Product
                {
                    ProductID = 5,
                    ProductCode = "TEAM10",
                    Name = "Team Manager 1.0",
                    YearlyPrice = 4.99M,
                    ReleaseDate = DateTime.Parse("2017-05-01 00:00:00.000")
                },
                new Product
                {
                    ProductID = 6,
                    ProductCode = "TRNY10",
                    Name = "Tournament Master 1.0",
                    YearlyPrice = 4.99M,
                    ReleaseDate = DateTime.Parse("2015-12-01 00:00:00.000")
                },
                new Product
                {
                    ProductID = 7,
                    ProductCode = "TRNY20",
                    Name = "Tournament Master 2.0",
                    YearlyPrice = 5.99M,
                    ReleaseDate = DateTime.Parse("2018-02-15 00:00:00.000")
                }
            };
        }

        public IEnumerable<Product> GetProducts()
        {
            // Return products list
            return from p in products
                   orderby p.Name
                   select p;
        }

        public IEnumerable<Product> DeleteProduct(Product product)
        {
            // Iterate through list to delete product
            int location = -1;
            foreach (var p in products)
            {
                if (p.ProductID == product.ProductID)
                {
                    location++;
                    products.RemoveAt(location);
                    break;
                }
                location++;
            }

            // Return product list
            return from p in products
                   orderby p.Name
                   select p;
        }

        public Product GetProductById(int productId)
        {
            return products.FirstOrDefault(p => p.ProductID == productId);
        }
    }
}
