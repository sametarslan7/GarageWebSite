using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GarageWebSite.Models.Classes
{
    public class Brand
    {
        [Key]
        public int BrandId { get; set; } // Primary Key
        public string BrandName { get; set; }

        // Navigation Properties
        public ICollection<Car> Cars { get; set; } // One brand can have multiple cars
    }
}