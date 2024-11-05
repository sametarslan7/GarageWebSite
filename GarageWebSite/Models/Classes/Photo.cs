using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GarageWebSite.Models.Classes
{
    public class Photo
    {
        [Key]
        public int PhotoId { get; set; } // Primary Key

        [ForeignKey("Car")] // Car ile ilişkilendirme
        public int CarId { get; set; } // Foreign Key to Cars

        public string PhotoUrl { get; set; }

        // Navigation Property
        public Car Car { get; set; }
    }
}