using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mvc1.Data;
using mvc1.Models;

namespace mvc1.Controllers
{
    public class SkierController : Controller
    {
      private readonly ApplicationDbContext _db;

      public SkierController(ApplicationDbContext db)
      {
         _db = db;
      }
        public IActionResult Index()
        {
         IEnumerable<Skier> skierList = _db.Skier;
            return View(skierList);
        }

      // GET - CREATE
      public IActionResult Create()
      {
         return View();
      }

      //POST - CREATE
      [HttpPost]
      [ValidateAntiForgeryToken]
      public IActionResult Create(Skier obj)
      {
         if (ModelState.IsValid)
         {
            _db.Skier.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
         }
         return View(obj);

      }
   }
}