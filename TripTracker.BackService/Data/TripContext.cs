using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripTracker.BackService.Models;

namespace TripTracker.BackService.Data
{
    public class TripContext : DbContext
    {
        public DbSet<Trip> trips {get;set;}
    }
}
