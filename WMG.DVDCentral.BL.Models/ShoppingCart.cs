using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMG.DVDCentral.BL.Models
{
    public class ShoppingCart
    {
        public List<Movie> Items { get; set; } = new List<Movie>();
        public int NumberOfItems { get { return Items.Count; } }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public double SubTotal { get { return Items.Sum(movie => movie.Cost); } }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public double Tax { get { return SubTotal * .055; } }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public double Total { get { return SubTotal + Tax; } }

    }
}

