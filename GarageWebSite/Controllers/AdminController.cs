using GarageWebSite.Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;


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
        
        // ADMIN
        public ActionResult AdminList()
        {
            var value = c.Admins.ToList();
            return View(value);
        }

        //CARS
        public ActionResult CarList(string brand, string year, string fuel)
        {
            // Cars tablosuna FuelType, Photos ve Brand dahil edilerek gerekli verileri tek seferde getiriyoruz.
            var carsQuery = c.Cars
                .Include(c => c.FuelType)
                .Include(c => c.Photos)
                .Include(c => c.Brand)
                .AsQueryable(); // AsQueryable ile sorgu oluşturuyoruz.

            // Sorguyu çalıştırıyoruz
            var cars = carsQuery.ToList();

            // ViewModel oluşturma
            var carViewModels = cars.Select(car => new CarViewModel
            {
                Car = car,
                FuelTypeName = car.FuelType.FuelName, // Yakıt türünü doğrudan alıyoruz
                Photos = car.Photos.ToList(), // Araca ait tüm fotoğrafları alıyoruz
                BrandName = car.Brand.BrandName // Araca ait markayı alıyoruz
            }).ToList();
            return View(carViewModels);
        }

        //BRAND
        public ActionResult BrandList()
        {
            var value = c.Brands.ToList();
            return View(value);
        }

        //FUELTYPE
        public ActionResult FuelTypeList()
        {
            var value = c.FuelTypes.ToList();
            return View(value);
        }

        //SERVICE
        public ActionResult ServiceList()
        {
            var value = c.Services.ToList();
            return View(value);
        }

        //CONTACT
        public ActionResult ContactList()
        {
            var value = c.Contacts.ToList();
            return View(value);
        }
    }
}