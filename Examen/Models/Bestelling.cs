using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Examen.Models
{
    public class Bestelling
    {
        [Key]
        public int Id { get; set; }

        public int Tafel { get; set; }

        [ForeignKey("ReserveringId")]
        public virtual Reservering Reservering { get; set; }

        [Display(Name = "Reservering")]
        public int ReserveringId { get; set; }

        public List<Bestelregel> Bestelregel { get; set; }

        public string GenerateProductList()
        {
            var ProductList = "";
            if(Bestelregel != null) {
                foreach (var bestelregel in Bestelregel)
                {
                    ProductList += bestelregel.Product.Name + ", ";
                }
            }
            return ProductList;
        }
    }
}