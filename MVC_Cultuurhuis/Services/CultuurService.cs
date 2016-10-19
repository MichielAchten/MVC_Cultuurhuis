using MVC_Cultuurhuis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Cultuurhuis.Services
{
    public class CultuurService
    {
        public List<Genre> GetAllGenres()
        {
            using (var db = new CultuurHuisMVCEntities())
            {
                return db.Genres.OrderBy(m => m.GenreNaam).ToList();
            }
        }

        public Genre GetGenre(int? id)
        {
            using (var db = new CultuurHuisMVCEntities())
            {
                return db.Genres.Find(id);
            }
        }

        public List<Voorstelling> GetAlleVoorstellingenVanGenre(int? id)
        {
            using (var db = new CultuurHuisMVCEntities())
            {
                var query = from voorstelling in db.Voorstellingen.Include("Genre")
                            where voorstelling.Datum >= DateTime.Today && voorstelling.GenreNr == id
                            orderby voorstelling.Datum
                            select voorstelling;
                return query.ToList();
            }
        }

        internal Voorstelling GetVoorstelling(int? id)
        {
            using (var db = new CultuurHuisMVCEntities())
            {
                return db.Voorstellingen.Find(id);
            }
        }
    }
}