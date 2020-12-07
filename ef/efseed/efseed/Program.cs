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


namespace CodeFirstNewDatabaseSample
{
   class Program
   {
      static void Main(string[] args)
      {
         using (var db = new BloggingContext())
         {
            // Create and save a new Blog
            Console.Write("Enter a name for a new Blog: ");
            var name = Console.ReadLine();

            var blog = new Blog { Name = name };
            db.Blogs.Add(blog);
            db.SaveChanges();

            // Display all Blogs from the database
            var query = from b in db.Blogs
                        orderby b.Name
                        select b;

            //var query2 = db.Blogs.OrderBy(b => b.Name);

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
      public DbSet<Blog> Blogs { get; set; }
      public DbSet<Post> Posts { get; set; }
   }
}