using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GarageWebSite.Models.Classes
{
    public class CarViewModel
    {
        public Car Car { get; set; } // Car modelini temsil eder
        public string FuelTypeName { get; set; } // Yakıt türünün adını temsil eder
        public string BrandName { get; set; } // Yakıt türünün adını temsil eder
        public List<Photo> Photos { get; set; }
    }
}