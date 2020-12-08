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

      public DbSet<Skier> Skier { get; set; }
   }
}
