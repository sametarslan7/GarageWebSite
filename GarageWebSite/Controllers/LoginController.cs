using GarageWebSite.Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.IO;
using System.Web.Security;

namespace GarageWebSite.Controllers
{
    public class LoginController : Controller
    {
        Context c = new Context();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string nickname,string password)
        {
            // Kullanıcıyı veritabanında arıyoruz
            var user = c.Admins.FirstOrDefault(a => a.nickname == nickname && a.password == password);

            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(user.nickname, false);
                Session["nickname"] = user.nickname.ToString();
                return RedirectToAction("Index", "Admin");

            }
            else
            {
                // Giriş başarısız ise hata mesajı gönder
                ViewBag.ErrorMessage = "Geçersiz kullanıcı adı veya şifre.";
                return View();
            }
        }

        [HttpGet]
        public ActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Signup(Admin a)
        {
            c.Admins.Add(a);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}