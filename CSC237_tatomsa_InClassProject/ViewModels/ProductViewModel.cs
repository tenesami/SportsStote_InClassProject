using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSC237_tatomsa_InClassProject.ViewModels
{
    public class ProductViewModel
    {
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public decimal YearlyPrice { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
