using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Web;

namespace GarageWebSite.Models.Classes
{
    public class Car
    {
        [Key]
        public int Id { get; set; } // Primary Key
        public string Name { get; set; }
        public int BrandId { get; set; } // Foreign Key to Brands
        public int Km { get; set; }
        public int Year { get; set; }
        public double Power { get; set; }
        public int FuelId { get; set; } // Foreign Key to FuelTypes
        public int PhotoId { get; set; } // Foreign Key to Photos (One main photo)
        public string OwnerPhone { get; set; }
        public decimal Price { get; set; }

        // Navigation Properties
        public Brand Brand { get; set; }
        public FuelType FuelType { get; set; }
        public ICollection<Photo> Photos { get; set; } // One car can have multiple photos
    }
}