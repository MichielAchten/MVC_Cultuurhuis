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

            if (Session.Keys.Count != 0)
            {
                ViewBag.mandjeTonen = true;
            }
            else
            {
                ViewBag.mandjeTonen = false;
            }

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

        public ActionResult Mandje()
        {
            decimal teBetalen = 0;
            List<MandjeItem> mandjeItems = new List<MandjeItem>();

            foreach (string nummer in Session)
            {
                int voorstellingsnummer;
                if (int.TryParse(nummer, out voorstellingsnummer))
                {
                    Voorstelling voorstelling = db.GetVoorstelling(voorstellingsnummer);
                    if (voorstelling != null)
                    {
                        MandjeItem mandjeItem = new MandjeItem(voorstellingsnummer, voorstelling.Titel,
                            voorstelling.Uitvoerders, voorstelling.Datum,
                            voorstelling.Prijs, Convert.ToInt16(Session[nummer]));
                        teBetalen += (mandjeItem.Plaatsen * mandjeItem.Prijs);

                        mandjeItems.Add(mandjeItem);
                    }
                }
            }
            ViewBag.teBetalen = teBetalen;
            return View(mandjeItems);
        }

        [HttpPost]
        public ActionResult Verwijderen()
        {
            foreach (var item in Request.Form.AllKeys)
            {
                if (Session[item] != null)
                {
                    Session.Remove(item);
                }
            }
            return RedirectToAction("Mandje", "Home");
        }

        public ActionResult Bevestiging()
        {
            //gebruiker opzoeken
            if (Request["zoek"] != null)
            {
                var naam = Request["naam"];
                var paswoord = Request["paswoord"];

                var klant = db.GetKlant(naam, paswoord);

                if (klant != null)
                {
                    Session["klant"] = klant;
                }
                else
                {
                    ViewBag.errorMessage = "Verkeerde gebruikersnaam of wachtwoord";
                }
                return View();
            }
            //nieuwe gebruiker
            if (Request["nieuw"] != null)
            {
                //redirect naar een nieuwe pagina
                return RedirectToAction("Nieuw", "Home");
            }
            //bevestig
            if (Request["bevestig"] != null)
            {

            }
            return View();
        }

        [HttpGet]
        public ActionResult Nieuw()
        {
            var nieuwForm = new NieuweKlantForm();
            return View(nieuwForm);
        }
    }
}