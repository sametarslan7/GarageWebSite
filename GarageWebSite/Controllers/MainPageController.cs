using GarageWebSite.Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GarageWebSite.Controllers
{
    public class MainPageController : Controller
    {
        Context c = new Context();
        // GET: MainPage
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult Contact()
        {
            var value=c.Contacts.ToList();
            return PartialView(value);
        }
        public PartialViewResult Service()
        {
            var value = c.Services.ToList();
            return PartialView(value);
        }
    }
}