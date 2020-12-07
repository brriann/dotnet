using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
// EF 6
//using System.Data.Entity;

//
// EF SEEDING
//
// https://docs.microsoft.com/en-us/ef/ef6/modeling/code-first/workflows/new-database
// https://www.entityframeworktutorial.net/efcore/create-model-for-existing-database-in-ef-core.aspx
//
// EF 6 vs EF Core
//
// https://docs.microsoft.com/en-us/ef/efcore-and-ef6/
// https://docs.microsoft.com/en-us/ef/core/
//
// EF CORE MIGRATIONS
// https://docs.microsoft.com/en-us/ef/core/managing-schemas/ensure-created
// 
// DOTNET-EF CLI TOOLS TO CREATE MIGRATIONS
// https://docs.microsoft.com/en-us/ef/core/cli/dotnet
//
// Powershell, in-line with .csproj file
// dotnet ef migrations add InitialCreate
// dotnet ef database update
//
// https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/applying?tabs=dotnet-core-cli
//
// Configure SQL Server conn string
// https://stackoverflow.com/questions/38878140/how-can-i-implement-dbcontext-connection-string-in-net-core

namespace CodeFirstNewDatabaseSample
{
   class Program
   {
      static void Main(string[] args)
      {
         using (var db = new BloggingContext())
         {
            var sql = db.Database.GenerateCreateScript();
            Console.WriteLine(sql);

            // Create and save a new Blog
            Console.Write("Enter a name for a new Blog: ");
            var name = Console.ReadLine();

            var blog = new Blog { Name = name };
            db.Blogs.Add(blog);
            db.SaveChanges();

            var query = db.Blogs.OrderBy(b => b.Name);

            Console.WriteLine("All blogs in the database:");
            foreach (var item in query)
            {
               Console.WriteLine(item.Name);
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
         }
      }
   }

   public class Blog
   {
      public int BlogId { get; set; }
      public string Name { get; set; }

      public virtual List<Post> Posts { get; set; }
   }

   public class Post
   {
      public int PostId { get; set; }
      public string Title { get; set; }
      public string Content { get; set; }

      public int BlogId { get; set; }
      public virtual Blog Blog { get; set; }
   }

   public class BloggingContext : DbContext
   {
      const string connStringLocalDev = "Server=DESKTOP-I4R8UR2;Database=seed1;User Id=bfosa;Password=bfosa;Trusted_Connection=True;";

      public BloggingContext()
      {
         // OK for initial dev. don't use for Migrations.
         // this.Database.EnsureCreated();
      }

      public DbSet<Blog> Blogs { get; set; }
      public DbSet<Post> Posts { get; set; }

      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      {
         optionsBuilder.UseSqlServer(connStringLocalDev);
      }
   }
}