using Microsoft.EntityFrameworkCore;
using mvc1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvc1.Data
{
   public class ApplicationDbContext : DbContext
   {
      public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
      {

      }
      
      // add-migration AddEntityToDatabase
      // update-database

      public DbSet<Skier> Skier { get; set; }
      public DbSet<Tourney> Tourney { get; set; }
   }
}
