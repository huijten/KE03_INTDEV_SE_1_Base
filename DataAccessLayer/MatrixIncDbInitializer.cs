﻿using DataAccessLayer.Models;
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

            // TODO: Hier moet ik nog wat namen verzinnen die betrekking hebben op de matrix.
            // - Denk aan de m3 boutjes, moertjes en ringetjes.
            // - Denk aan namen van schepen
            // - Denk aan namen van vliegtuigen            
            var customers = new Customer[]
            {
                new Customer { Name = "Neo", Address = "123 Elm St" , Active=true},
                new Customer { Name = "Morpheus", Address = "456 Oak St", Active = true },
                new Customer { Name = "Trinity", Address = "789 Pine St", Active = true }
            };
            context.Customers.AddRange(customers);

            var orders = new Order[]
            {
                new Order { Customer = customers[0], OrderDate = DateTime.Parse("2021-01-01")},
                new Order { Customer = customers[0], OrderDate = DateTime.Parse("2021-02-01")},
                new Order { Customer = customers[1], OrderDate = DateTime.Parse("2021-02-01")},
                new Order { Customer = customers[2], OrderDate = DateTime.Parse("2021-03-01")}
            };  
            context.Orders.AddRange(orders);

            var products = new Product[]
            {
                new Product { Name = "Nebuchadnezzar", Description = "Het schip waarop Neo voor het eerst de echte wereld leert kennen", Price = 10000.00m },
                new Product { Name = "Jack-in Chair", Description = "Stoel met een rugsteun en metalen armen waarin mensen zitten om ingeplugd te worden in de Matrix via een kabel in de nekpoort", Price = 500.50m },
                new Product { Name = "EMP (Electro-Magnetic Pulse) Device", Description = "Wapentuig op de schepen van Zion", Price = 129.99m }
            };
            context.Products.AddRange(products);

            var parts = new Part[]
            {
                new Part { Name = "Tandwiel", Description = "Overdracht van rotatie in bijvoorbeeld de motor of luikmechanismen", ImageURL = "https://static.vecteezy.com/system/resources/previews/009/588/932/non_2x/gear-wheel-transparent-free-png.png"},
                new Part { Name = "M5 Boutje", Description = "Bevestiging van panelen, buizen of interne modules", ImageURL = "https://ijzershop.nl/1739/verzinkte-zeskantbout-m5-x-70.jpg"},
                new Part { Name = "Hydraulische cilinder", Description = "Openen/sluiten van zware luchtsluizen of bewegende onderdelen", ImageURL = "https://www.hydrauliek24.nl//Files/6/85000/85837/ProductPhotos/1000x525/420178570.jpg"},
                new Part { Name = "Koelvloeistofpomp", Description = "Koeling van de motor of elektronische systemen.", ImageURL = "https://www.hogetex.com/media/catalog/product/cache/abe8e32530358e970f9de6550ae2191b/H/G/HGTC2545_28717_1_2.jpg"}
            };
            context.Parts.AddRange(parts);

            context.SaveChanges();

            context.Database.EnsureCreated();
        }
    }
}
