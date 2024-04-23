using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMG.DVDCentral.BL.Models
{
    public class Order
    {
        [DisplayName("Order #")]
        public int Id { get; set; }
        [DisplayName("Customer Id")]
        public int CustomerId { get; set; }
        [DisplayName("Order Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime OrderDate { get; set; }
        [DisplayName("Ship Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime ShipDate { get; set; }
        [DisplayName("User Id")]
        public int UserId { get; set; }
        [DisplayName("Order Items")]
        public List<OrderItem> OrderItems { get; set;} = new List<OrderItem>();
        [DisplayName("SubTotal")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public float TotalCost { get; set; }
        [DisplayName("Tax")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public float Tax { get; set; }
        [DisplayName("Total")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public float Total { get; set; }

        public void CalculateTotalCost()
        {
            TotalCost = OrderItems.Sum(oi => oi.Quantity * oi.Cost);
            Tax = TotalCost * 0.055f;
            Total = TotalCost + Tax;
        }

    }
        
}
