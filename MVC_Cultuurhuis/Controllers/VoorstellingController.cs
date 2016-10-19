using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC_Cultuurhuis.Models;

namespace MVC_Cultuurhuis.Controllers
{
    public class VoorstellingController : Controller
    {
        private CultuurHuisMVCEntities db = new CultuurHuisMVCEntities();

        // GET: Voorstelling
        public ActionResult Index()
        {
            var voorstellingen = db.Voorstellingen.Include(v => v.Genre);
            return View(voorstellingen.ToList());
        }

        // GET: Voorstelling/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voorstelling voorstelling = db.Voorstellingen.Find(id);
            if (voorstelling == null)
            {
                return HttpNotFound();
            }
            return View(voorstelling);
        }

        // GET: Voorstelling/Create
        public ActionResult Create()
        {
            ViewBag.GenreNr = new SelectList(db.Genres, "GenreNr", "Naam");
            return View();
        }

        // POST: Voorstelling/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VoorstellingsNr,Titel,Uitvoerders,Datum,GenreNr,Prijs,VrijePlaatsen")] Voorstelling voorstelling)
        {
            if (ModelState.IsValid)
            {
                db.Voorstellingen.Add(voorstelling);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GenreNr = new SelectList(db.Genres, "GenreNr", "Naam", voorstelling.GenreNr);
            return View(voorstelling);
        }

        // GET: Voorstelling/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voorstelling voorstelling = db.Voorstellingen.Find(id);
            if (voorstelling == null)
            {
                return HttpNotFound();
            }
            ViewBag.GenreNr = new SelectList(db.Genres, "GenreNr", "Naam", voorstelling.GenreNr);
            return View(voorstelling);
        }

        // POST: Voorstelling/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VoorstellingsNr,Titel,Uitvoerders,Datum,GenreNr,Prijs,VrijePlaatsen")] Voorstelling voorstelling)
        {
            if (ModelState.IsValid)
            {
                db.Entry(voorstelling).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GenreNr = new SelectList(db.Genres, "GenreNr", "Naam", voorstelling.GenreNr);
            return View(voorstelling);
        }

        // GET: Voorstelling/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voorstelling voorstelling = db.Voorstellingen.Find(id);
            if (voorstelling == null)
            {
                return HttpNotFound();
            }
            return View(voorstelling);
        }

        // POST: Voorstelling/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Voorstelling voorstelling = db.Voorstellingen.Find(id);
            db.Voorstellingen.Remove(voorstelling);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
