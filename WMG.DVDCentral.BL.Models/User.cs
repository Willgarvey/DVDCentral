using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace WMG.DVDCentral.BL.Models
{
    public class User
    {
        public int Id { get; set; }
        [DisplayName("First Name")]
        public string? FirstName { get; set; }
        [DisplayName("Last Name")]
        public string? LastName { get; set;}
        [DisplayName("User Id")]
        public string? UserName { get; set; }
        public string? Password { get; set; }
        [DisplayName("Full Name")]
        public string FullName { get { return FirstName + " " + LastName; } }
        [DisplayName("Customer Name")]
        public int CustomerId { get; set; }
        public DateTime DateOfBirth { get; set; }

    }
}
