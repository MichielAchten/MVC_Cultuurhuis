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
    public class ReservatieController : Controller
    {
        private CultuurHuisMVCEntities db = new CultuurHuisMVCEntities();

        // GET: Reservatie
        public ActionResult Index()
        {
            var reservaties = db.Reservaties.Include(r => r.Klant).Include(r => r.Voorstelling);
            return View(reservaties.ToList());
        }

        // GET: Reservatie/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservatie reservatie = db.Reservaties.Find(id);
            if (reservatie == null)
            {
                return HttpNotFound();
            }
            return View(reservatie);
        }

        // GET: Reservatie/Create
        public ActionResult Create()
        {
            ViewBag.KlantNr = new SelectList(db.Klanten, "KlantNr", "Voornaam");
            ViewBag.VoorstellingsNr = new SelectList(db.Voorstellingen, "VoorstellingsNr", "Titel");
            return View();
        }

        // POST: Reservatie/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReservatieNr,KlantNr,VoorstellingsNr,Plaatsen")] Reservatie reservatie)
        {
            if (ModelState.IsValid)
            {
                db.Reservaties.Add(reservatie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KlantNr = new SelectList(db.Klanten, "KlantNr", "Voornaam", reservatie.KlantNr);
            ViewBag.VoorstellingsNr = new SelectList(db.Voorstellingen, "VoorstellingsNr", "Titel", reservatie.VoorstellingsNr);
            return View(reservatie);
        }

        // GET: Reservatie/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservatie reservatie = db.Reservaties.Find(id);
            if (reservatie == null)
            {
                return HttpNotFound();
            }
            ViewBag.KlantNr = new SelectList(db.Klanten, "KlantNr", "Voornaam", reservatie.KlantNr);
            ViewBag.VoorstellingsNr = new SelectList(db.Voorstellingen, "VoorstellingsNr", "Titel", reservatie.VoorstellingsNr);
            return View(reservatie);
        }

        // POST: Reservatie/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReservatieNr,KlantNr,VoorstellingsNr,Plaatsen")] Reservatie reservatie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservatie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KlantNr = new SelectList(db.Klanten, "KlantNr", "Voornaam", reservatie.KlantNr);
            ViewBag.VoorstellingsNr = new SelectList(db.Voorstellingen, "VoorstellingsNr", "Titel", reservatie.VoorstellingsNr);
            return View(reservatie);
        }

        // GET: Reservatie/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservatie reservatie = db.Reservaties.Find(id);
            if (reservatie == null)
            {
                return HttpNotFound();
            }
            return View(reservatie);
        }

        // POST: Reservatie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reservatie reservatie = db.Reservaties.Find(id);
            db.Reservaties.Remove(reservatie);
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
