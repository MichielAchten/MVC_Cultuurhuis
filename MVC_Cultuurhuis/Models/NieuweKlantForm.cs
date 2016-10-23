using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_Cultuurhuis.Models
{
    public class NieuweKlantForm
    {
        [Required(ErrorMessage = "Voornaam is verplicht in te vullen !")]
        public string Voornaam { get; set; }
        [Required(ErrorMessage="Familienaam is verpolicht in te vullen !")]
        public string Familienaam { get; set; }
        [Required(ErrorMessage = "Straat is verplicht in te vullen !")]
        public string Straat { get; set; }
        [Required(ErrorMessage = "Huisnr is verplicht in te vullen !")]
        public string Huisnr { get; set; }
        [Required(ErrorMessage = "Postcode is verplicht in te vullen !")]
        public string Postcode { get; set;}
        [Required(ErrorMessage = "Gemeente is verplicht in te vullen !")]
        public string Gemeente { get; set; }
        [Required(ErrorMessage = "Gebruikersnaam is verplicht in te vullen !")]
        [ValidationAttributes.Bestaatnogniet(ErrorMessage="Een klant met deze gebruikersnaam komt al voor in de database. Kies een andere naam.")]
        public string Gebruikersnaam { get { return this.Gebruikersnaam; } set { this.Gebruikersnaam = value; } }
        [Required(ErrorMessage= "Wachtwoord is erplicht in te vullen !")]
        [DataType(DataType.Password)]
        [Display(Name="Wachtwoord")]
        public string Paswoord { get; set; }
        [Required(ErrorMessage = "Wachtwoord moet bevestigd worden !")]
        [DataType(DataType.Password)]
        [Compare("Paswoord", ErrorMessage = "{0} verschilt van {1}. Probeer opnieuw.")]
        [Display(Name = "Wachtwoord bevestigen")]
        public string HerhaalPaswoord { get; set; }
    }
}