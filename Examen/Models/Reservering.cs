using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Examen.Models
{
    public class Reservering
    {
        [Key]
        public int Id { get; set; }

        public DateTime Datum { get; set; }

        public int Tafel { get; set; }

        [Display(Name = "Naam Klant")]
        public string Klantnaam { get; set; }

        [Display(Name = "Telefoonnummer")]
        public string Telefoonnr { get; set; }

        [Display(Name = "Aantal Personen")]
        public int AantalPersonen { get; set; }

        [Display(Name = "Is geweest")]
        public bool Gebruikt { get; set; }

        public bool Betaald { get; set; }

        public List<Bestelling> Bestellingen { get; set; }
    }
}
