using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CSC237_tatomsa_InClassProject.Models
{
    public class Product
    {
        public int ProductID { get; set; }

        [Required]
        public string ProductCode { get; set; }

        [Required]
        public string Name { get; set; }

        [Range(0, 10000000)]
        [Column(TypeName = "decimal(8,2)")]
        public decimal YearlyPrice { get; set; }

        public DateTime ReleaseDate { get; set; }

        //Navigation Property linking entity I.e I Collection allow us to add end edit but IEnumerable is not
        public ICollection<Registration> Registrations { get; set; }


    }
}
