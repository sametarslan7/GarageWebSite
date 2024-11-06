using GarageWebSite.Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.IO;


namespace GarageWebSite.Controllers
{
    public class AdminController : Controller
    {
        Context c = new Context();
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
        [HttpGet]
        public ActionResult NewAdmin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NewAdmin(Admin a)
        {
            c.Admins.Add(a);
            c.SaveChanges();
            return RedirectToAction("AdminList");
        }

        public ActionResult DeleteAdmin(int id)
        {
            var dlt = c.Admins.Find(id);
            c.Admins.Remove(dlt);
            c.SaveChanges();
            return RedirectToAction("AdminList");
        }

        public ActionResult GetAdmin(int id)
        {
            var admin=c.Admins.Find(id);
            return View("GetAdmin",admin);
        }

        public ActionResult UpdateAdmin(Admin ad)
        {
            var upt = c.Admins.Find(ad.Id);

            upt.name = ad.name;
            upt.surname = ad.surname;
            upt.email = ad.email;
            upt.nickname = ad.nickname;
            upt.password= ad.password;
            c.SaveChanges();
            return RedirectToAction("AdminList");
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
        [HttpGet]
        public ActionResult NewCar()
        {
            ViewBag.Brands = c.Brands.ToList(); // Marka listesini ViewBag ile gönder
            ViewBag.FuelTypes = c.FuelTypes.ToList(); // Yakıt tipi listesini ViewBag ile gönder
            return View();
        }

        [HttpPost]
        public ActionResult NewCar(Car cr, IEnumerable<HttpPostedFileBase> Files)
        {
            using (var transaction = c.Database.BeginTransaction())
            {
                try
                {
                    // 1. Yeni araba ekleniyor
                    c.Cars.Add(cr);
                    c.SaveChanges(); // Car kaydını kaydediyoruz ve ID'yi alıyoruz

                    // Yeni eklenen arabanın ID'sini al
                    var carId = cr.Id;

                    // 2. Fotoğrafları kaydetme işlemi (Sadece URL'leri alıp veritabanına kaydediyoruz)
                    if (Files != null && Files.Any())
                    {
                        foreach (var file in Files)
                        {
                            if (file != null && file.ContentLength > 0)
                            {
                                // Dosya adını al
                                var fileName = Path.GetFileName(file.FileName);
                                var filePath = Path.Combine(Server.MapPath("~/images/cars"), fileName);

                                // Fotoğrafı sunucuya kaydet
                                file.SaveAs(filePath);

                                // Fotoğrafı Photos tablosuna ekle
                                var photo = new Photo
                                {
                                    CarId = carId,       // Car ile ilişkilendiriliyor
                                    PhotoUrl = "/images/cars/" + fileName  // Tam URL'yi PhotoUrl olarak kaydediyoruz
                                };

                                // Fotoğrafı Photos tablosuna ekle
                                c.Photos.Add(photo);
                            }
                        }

                        // Tüm fotoğraf kayıtlarını kaydet
                        c.SaveChanges();
                    }

                    // Her şey başarılıysa işlemi onayla
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    // Hata durumunda işlemi geri al
                    transaction.Rollback();
                    // Hata mesajı gösterme veya log kaydı yapabilirsiniz
                    ModelState.AddModelError("", "Bir hata oluştu. Lütfen tekrar deneyin. Hata: " + ex.Message);
                    return View(cr);
                }
            }

            return RedirectToAction("CarList");
        }
        public ActionResult DeleteCar(int id)
        {
            // Önce arabayı buluyoruz
            var dlt_car = c.Cars.Find(id);

            if (dlt_car != null)
            {
                // Bu arabaya ait tüm fotoğrafları buluyoruz
                var photos = c.Photos.Where(p => p.CarId == id).ToList();

                // Her bir fotoğrafı tek tek kaldırıyoruz
                foreach (var photo in photos)
                {
                    // Öncelikle sunucudan dosyayı siliyoruz (eğer gerekli ise)
                    var filePath = Server.MapPath(photo.PhotoUrl);
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath); // Fotoğraf dosyasını sil
                    }

                    // Fotoğrafı veritabanından siliyoruz
                    c.Photos.Remove(photo);
                }

                // Arabayı veritabanından siliyoruz
                c.Cars.Remove(dlt_car);

                // Tüm değişiklikleri kaydediyoruz
                c.SaveChanges();
            }

            return RedirectToAction("CarList");
        }

        public ActionResult GetCar(int id)
        {
            ViewBag.Brands = c.Brands.ToList(); // Marka listesini ViewBag ile gönder
            ViewBag.FuelTypes = c.FuelTypes.ToList(); // Yakıt tipi listesini ViewBag ile gönder

            var car = c.Cars.Find(id);

            if (car != null)
            {
                ViewBag.SelectedBrand = car.Brand.BrandId; // Mevcut BrandId'yi gönder
                ViewBag.SelectedFuelType = car.FuelType.FuelId; // Mevcut FuelTypeId'yi gönder
            }

            return View("GetCar", car);
        }

        public ActionResult UpdateCar(Car cr)
        {
            var upt = c.Cars.Find(cr.Id);
            upt.Name = cr.Name;
            upt.Name = cr.Name;
            upt.Km = cr.Km;
            upt.Year = cr.Year;
            upt.OwnerPhone = cr.OwnerPhone;
            upt.Price = cr.Price;
            upt.Brand = cr.Brand;
            upt.FuelType = cr.FuelType;
            upt.Power = cr.Power;
            c.SaveChanges();


            return RedirectToAction("CarList");
        }

        //BRAND
        public ActionResult BrandList()
        {
            var value = c.Brands.ToList();
            return View(value);
        }

        [HttpGet]
        public ActionResult NewBrand()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NewBrand(Brand b)
        {
            c.Brands.Add(b);
            c.SaveChanges();
            return RedirectToAction("BrandList");
        }

        public ActionResult DeleteBrand(int id)
        {
            var dlt = c.Brands.Find(id);
            c.Brands.Remove(dlt);
            c.SaveChanges();
            return RedirectToAction("BrandList");
        }

        public ActionResult GetBrand(int id)
        {
            var brand = c.Brands.Find(id);
            return View("GetBrand",brand);
        }
        public ActionResult UpdateBrand(Brand br)
        {
            var upt = c.Brands.Find(br.BrandId);
            upt.BrandName = br.BrandName;
            c.SaveChanges();
            return RedirectToAction("BrandList");
        }
        //PHOTO
        public ActionResult PhotoList()
        {
            // Cars tablosuna Photos  dahil edilerek gerekli verileri tek seferde getiriyoruz.
            var carsQuery = c.Cars
                .Include(c => c.Photos)
                .AsQueryable(); // AsQueryable ile sorgu oluşturuyoruz.

            // Sorguyu çalıştırıyoruz
            var cars = carsQuery.ToList();

            // ViewModel oluşturma
            var carViewModels = cars.Select(car => new CarViewModel
            {
                Car = car,
                Photos = car.Photos.ToList(), // Araca ait tüm fotoğrafları alıyoruz
            }).ToList();

            return View(carViewModels);
        }

        public ActionResult GetPhoto(int id)
        {
            // Belirtilen aracın fotoğraflarını sorgula
            var photos = c.Photos.Where(p => p.CarId == id).ToList();

            // Fotoğrafları View'e gönder
            return View("GetPhoto",photos);
        }

        public ActionResult DeletePhoto(int id)
        {
            var dlt = c.Photos.Find(id);
            c.Photos.Remove(dlt);
            c.SaveChanges();
            return RedirectToAction("PhotoList");
        }
        //FUELTYPE
        public ActionResult FuelTypeList()
        {
            var value = c.FuelTypes.ToList();
            return View(value);
        }

        [HttpGet]
        public ActionResult NewFuelType()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NewFuelType(FuelType f)
        {
            c.FuelTypes.Add(f);
            c.SaveChanges();
            return RedirectToAction("FuelTypeList");
        }

        public ActionResult DeleteFuelType(int id)
        {
            var dlt=c.FuelTypes.Find(id);
            c.FuelTypes.Remove(dlt);
            c.SaveChanges();
            return RedirectToAction("FuelTypeList");
        }

        public ActionResult GetFuelType(int id)
        {
            var fuel=c.FuelTypes.Find(id);
            return View("GetFuelType",fuel);
        }
        public ActionResult UpdateFuelType(FuelType ft)
        {
            var upt = c.FuelTypes.Find(ft.FuelId);
            upt.FuelName = ft.FuelName;
            c.SaveChanges();
            return RedirectToAction("FuelTypeList");
        }

        //SERVICE
        public ActionResult ServiceList()
        {
            var value = c.Services.ToList();
            return View(value);
        }

        [HttpGet]
        public ActionResult NewServiceList()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NewServiceList(Service s)
        {
            c.Services.Add(s);
            c.SaveChanges();
            return RedirectToAction("ServiceList");
        }
        public ActionResult DeleteService(int id)
        {
            var dlt = c.Services.Find(id);
            c.Services.Remove(dlt);
            c.SaveChanges();
            return RedirectToAction("ServiceList");
        }
        public ActionResult GetService(int id)
        {
            var service=c.Services.Find(id);
            return View("GetService", service);
        }
        public ActionResult UpdateService(Service sr)
        {
            var upt = c.Services.Find(sr.Id);
            upt.serviceName = sr.serviceName;
            upt.serviceDescription=sr.serviceDescription;
            upt.servicePhotoUrl= sr.servicePhotoUrl;
            c.SaveChanges();
            return RedirectToAction("ServiceList");
        }
        //CONTACT
        public ActionResult ContactList()
        {
            var value = c.Contacts.ToList();
            return View(value);
        }
        public ActionResult GetContact(int id)
        {
            var contact=c.Contacts.Find(id);
            return View("GetContact", contact);
        }
        public ActionResult UpdateContact(Contact ct)
        {
            var upt=c.Contacts.Find(ct.Id);
            upt.email= ct.email;
            upt.adress= ct.adress;
            upt.phoneNumber= ct.phoneNumber;
            c.SaveChanges();
            return RedirectToAction("ContactList");
        }
    }
}