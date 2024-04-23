using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMG.DVDCentral.BL.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [DisplayName("Movie Title")]
        public string Title { get; set; }
        public string Description { get; set; }
        public int FormatId { get; set; }
        [DisplayName("Format")]
        public string FormatName { get; set; }
        public int DirectorId { get; set; }
        [DisplayName("Director Full Name")]
        public string DirectorName { get; set; }
        public int RatingId { get; set; }
        [DisplayName("Rating")]
        public string RatingName { get; set; }
        public float Cost { get; set; }
        [DisplayName("Quantity")]
        public int InStkQty { get; set; }
        [DisplayName("Image Path")]
        public string ImagePath { get; set; }
        public List<Genre> Genres { get; set; } = new List<Genre>();

    }
}
