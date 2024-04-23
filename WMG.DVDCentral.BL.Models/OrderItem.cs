using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMG.DVDCentral.BL.Models
{
    public class OrderItem
    {
        [DisplayName("Order Item Id")]
        public int Id {  get; set; }
        [DisplayName("Order Id")]
        public int OrderId { get; set; }
        public int Quantity { get; set; }
        [DisplayName("Movie Id")]
        public int MovieId { get; set; }
        [DisplayName("Price")]
        public float Cost { get; set; }
    }
}
