﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripTracker.BackService.Models;

namespace TripTracker.BackService.Data
{
    public class TripContext : DbContext
    {
        public TripContext(DbContextOptions<TripContext> options):base(options)
        {          
        }
        public TripContext() { }

        public DbSet<Trip> trips { get; set; }


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //}
        public static void SeedData(IServiceProvider serviceProvider)
        {
            using (
                    var serviceScope = serviceProvider
                    .GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope
                          .ServiceProvider.GetService<TripContext>();

                context.Database.EnsureCreated();

                if (context.trips.Any()) return;
                context.trips.AddRange(new Trip[]
                {

                new Trip
                {
                    Id = 1,
                    Name = "MVP Summit",
                    StartDate = new DateTime(2018, 3, 5),
                    EndDate = new DateTime(2018, 3, 8)
                },
            new Trip
            {
                Id = 2,
                Name = "DevIntersection Orlando 2018",
                StartDate = new DateTime(2018, 3, 25),
                EndDate = new DateTime(2018, 3, 28)
            },
            new Trip
            {
                Id = 3,
                Name = "Build 2018",
                StartDate = new DateTime(2018, 5, 7),
                EndDate = new DateTime(2018, 5, 9)
            }
                });
                context.SaveChanges();
            }
        }
    }
}
