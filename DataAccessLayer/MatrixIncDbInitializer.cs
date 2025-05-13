using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public static class MatrixIncDbInitializer
    {
        public static void Initialize(MatrixIncDbContext context)
        {
            // Look for any customers.
            if (context.Customers.Any())
            {
                return;   // DB has been seeded
            }
            
            var customers = new Customer[]
            {
                new Customer { Name = "Kevin", Email = "kevinhuijten@gmail.com", Address = "Heerlen"},
            };
            context.Customers.AddRange(customers);

            var orders = new Order[]
            {
                new Order { Customer = customers[0], OrderDate = DateTime.Parse("2021-01-01")},
                new Order { Customer = customers[0], OrderDate = DateTime.Parse("2021-02-01")},
                new Order { Customer = customers[0], OrderDate = DateTime.Parse("2021-02-01")},
                new Order { Customer = customers[0], OrderDate = DateTime.Parse("2021-03-01")}
            };  
            context.Orders.AddRange(orders);
            

            var parts = new Part[]
            {
                new Part { Name = "Tandwiel", Description = "Overdracht van rotatie in bijvoorbeeld de motor of luikmechanismen", ImageURL = "https://media.hornbach.nl/hb/packshot/as.46555900.jpg", Price = 4.95},
                new Part { Name = "M5 Boutje", Description = "Bevestiging van panelen, buizen of interne modules", ImageURL = "https://ijzershop.nl/1739/verzinkte-zeskantbout-m5-x-70.jpg", Price = 5.50},
                new Part { Name = "Hydraulische cilinder", Description = "Openen/sluiten van zware luchtsluizen of bewegende onderdelen", ImageURL = "https://hytres.com/wp-content/uploads/d3220200c-1.jpg", Price = 9.95},
                new Part { Name = "Koelvloeistofpomp", Description = "Koeling van de motor of elektronische systemen.", ImageURL = "https://www.hogetex.com/media/catalog/product/cache/abe8e32530358e970f9de6550ae2191b/H/G/HGTC2545_28717_1_2.jpg", Price = 18.50}
            };
            context.Parts.AddRange(parts);

            context.SaveChanges();

            context.Database.EnsureCreated();
        }
    }
}
