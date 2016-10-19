using MVC_Cultuurhuis.Models;
using MVC_Cultuurhuis.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Cultuurhuis.Controllers
{
    public class HomeController : Controller
    {
        private CultuurService db = new CultuurService();

        public ActionResult Index(int? id)
        {
            var voorstellingenInfo = new VoorstellingenInfo();
            voorstellingenInfo.Genres = db.GetAllGenres();
            voorstellingenInfo.Voorstellingen = db.GetAlleVoorstellingenVanGenre(id);
            voorstellingenInfo.Genre = db.GetGenre(id);
            return View(voorstellingenInfo);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Reserveren(int id)
        {
            Voorstelling gekozenVoorstelling = db.GetVoorstelling(id);
            return View(gekozenVoorstelling);
        }

        [HttpPost]
        public ActionResult Reserveer(int id)
        {
            uint aantalPlaatsen = uint.Parse(Request["aantalPlaatsen"]);
            var voorstellingInfo = db.GetVoorstelling(id);

            if (aantalPlaatsen > voorstellingInfo.VrijePlaatsen)
            {
                return RedirectToAction("Reserveer", "Home", new { id = id });
            }
            Session[id.ToString()] = aantalPlaatsen;

            return RedirectToAction("Mandje", "Home");
        }
    }
}