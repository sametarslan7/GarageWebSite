using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GarageWebSite.Models.Classes
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public string adress { get; set; }
    }
}