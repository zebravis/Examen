using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Examen.Models
{
    public class Bestelregel
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("BestellingId")]
        public virtual Bestelling Bestelling { get; set; }

        [Display(Name = "Bestelling")]
        public int BestellingId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        [Display(Name = "Product")]
        public int ProductId { get; set; }

        public int Aantal { get; set; }
    }
}
