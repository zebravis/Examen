using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Examen.Models.ViewModels
{
    public class CreateProductViewModel
    {
        public List<Subcategory> SubcategoryList { get; set; }

        public int SubcategoryId { get; set; }

        public Product Product { get; set; }

    }
}
