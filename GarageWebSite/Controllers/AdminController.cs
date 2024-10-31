using GarageWebSite.Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GarageWebSite.Controllers
{
    public class AdminController : Controller
    {
        Context c= new Context();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AdminList()
        {
            var value = c.Admins.ToList();
            return View(value);
        }
        public ActionResult BrandList()
        {
            var value = c.Brands.ToList();
            return View(value);
        }
        public ActionResult FuelTypeList()
        {
            var value = c.FuelTypes.ToList();
            return View(value);
        }
        public ActionResult ServiceList()
        {
            var value = c.Services.ToList();
            return View(value);
        }
        public ActionResult ContactList()
        {
            var value = c.Contacts.ToList();
            return View(value);
        }
    }
}