using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Examen.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Display(Name = "Category")]
        public string Name { get; set; }
    }
}
