using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GarageWebSite.Models.Classes
{
    public class FuelType
    {
        [Key]
        public int FuelId { get; set; } // Primary Key
        public string FuelName { get; set; }

        // Navigation Properties
        public ICollection<Car> Cars { get; set; } // One fuel type can be used by multiple cars
    }
}
