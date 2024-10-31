using GarageWebSite.Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;


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
        public PartialViewResult Car(string brand, string year, string fuel)
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

            return PartialView(carViewModels);
        }


    }
}