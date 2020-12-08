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

      // GET - EDIT
      public IActionResult Edit(int? id)
      {
         if (id == null || id == 0)
         {
            return NotFound();
         }
         var obj = _db.Tourney.Find(id); // .Find() ==> PK
         if (obj == null)
         {
            return NotFound();
         }
         return View(obj);
      }

      //POST - EDIT
      [HttpPost]
      [ValidateAntiForgeryToken]
      public IActionResult Edit(Tourney obj)
      {
         if (ModelState.IsValid)
         {
            _db.Tourney.Update(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
         }
         return View(obj);

      }

      // GET - DELETE
      public IActionResult Delete(int? id)
      {
         if (id == null || id == 0)
         {
            return NotFound();
         }
         var obj = _db.Tourney.Find(id); // .Find() ==> PK
         if (obj == null)
         {
            return NotFound();
         }
         return View(obj);
      }

      //POST - DELETE
      [HttpPost]
      [ValidateAntiForgeryToken]
      public IActionResult DeletePost(int? id)
      {
         var obj = _db.Tourney.Find(id);
         if (obj == null)
         {
            return NotFound();
         }
         _db.Tourney.Remove(obj);
         _db.SaveChanges();
         return RedirectToAction("Index");

      }
   }
}