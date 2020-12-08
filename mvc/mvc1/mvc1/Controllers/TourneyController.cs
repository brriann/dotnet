using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mvc1.Data;
using mvc1.Models;

namespace mvc1.Controllers
{
    public class TourneyController : Controller
    {
      private readonly ApplicationDbContext _db;

      public TourneyController(ApplicationDbContext db)
      {
         _db = db;
      }
        public IActionResult Index()
        {
         IEnumerable<Tourney> tourneyList = _db.Tourney;
            return View(tourneyList);
        }

      // GET - CREATE
      public IActionResult Create()
      {
         return View();
      }

      //POST - CREATE
      [HttpPost]
      [ValidateAntiForgeryToken]
      public IActionResult Create(Tourney obj)
      {
         if (ModelState.IsValid)
         {
            _db.Tourney.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
         }
         return View(obj);

      }
   }
}