using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GarageWebSite.Models.Classes
{
    public class Service
    {
        [Key]
        public int Id { get; set; }
        public string serviceName { get; set; }
        public string serviceDescription { get; set; }
        public string servicePhotoUrl { get; set; }
    }
}