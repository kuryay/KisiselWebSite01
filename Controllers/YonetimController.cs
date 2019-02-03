using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Blog001.DataAccessLayer;
using Blog001.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace Blog001.Controllers
{
    public class YonetimController : Controller
    {

        UsersOperations usersOperations = new UsersOperations();
        InformationOperations informationOperations = new InformationOperations();
        MessagesOperations messagesOperations = new MessagesOperations();
        PortfolioOperations portfolioOperations = new PortfolioOperations();

        private string applicationRoot;
        private readonly IHostingEnvironment hostingEnvironment;

        public YonetimController(IHostingEnvironment environment)
        {
            hostingEnvironment = environment;
        }

        /* Program İlk Çalıştığında Admin Oluşturur */
        public void createDefaultUser()
        {
            if (usersOperations.getUsers().Count == 0)
            {
                Users user = new Users("admin", "1111");
                usersOperations.addUser(user);
            }
        }

        /* Program İlk Çalıştığında Bilgiler Oluşturur */
        public void createDefaultInformation()
        {
            if (informationOperations.getInformations().Count == 0)
            {
                Information information = new Information("Default", "Site Başlık", "Burası Sitenin Alt Başlığı", "Burası Sitenin Açıklaması","default");
                informationOperations.addInformation(information);
            }
        }

        /* Program İlk Çalıştığında Mesaj Oluşturur */
        public void createDefaultMessage()
        {
            if (messagesOperations.getMessages().Count == 0)
            {
                Messages message = new Messages("Berke Kurnaz","bkoyunberkekurnaz@hotmail.com","Hoşgeldin...","Merhaba...Kendi Siteni Oluşturmaya Başladığın İçin Ve Bizi Tercih Ettiğin İçin Teşekkür Ediyorum :)", DateTime.Now.ToShortDateString());
                messagesOperations.addMessage(message);
            }
        }

        /* Yonetim Paneli Giris Sayfasi */
        public IActionResult Giris()
        {
            createDefaultUser();
            createDefaultInformation();
            createDefaultMessage();
            return View();
        }

        /* Yonetim Paneli Giris Islemi */
        [HttpPost]
        public IActionResult Giris(Users user)
        {
            var loginUser = usersOperations.loginUser(user);
            if (loginUser != null)
            {
                HttpContext.Session.SetString("SessionUserName", user.Username);
                return RedirectToAction("Anasayfa", "Yonetim");
            }
            return View(user);
        }

        public IActionResult Cikis()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Anasayfa");
        }
        
        /* Yonetim Paneli Anasayfa */
        public IActionResult Anasayfa()
        {
            var contentName = HttpContext.Session.GetString("SessionUserName");
            ViewBag.UserNameSession = contentName;
            if (ViewBag.UserNameSession == null)
            {
                return RedirectToAction("Index", "Anasayfa");
            }

            ViewBag.CountPortfolio = portfolioOperations.getPortfolio().Count;
            ViewBag.CountMessages = messagesOperations.getMessages().Count;
            return View();
        }

        /* Yonetim Paneli Hata Sayfası */
        public IActionResult ErrorAdminPage()
        {
            var contentName = HttpContext.Session.GetString("SessionUserName");
            ViewBag.UserNameSession = contentName;
            if (ViewBag.UserNameSession == null)
            {
                return RedirectToAction("Index", "Anasayfa");
            }

            return View();
        }

        /* Yonetim Paneli Nasil Kullanilir Sayfası */
        public IActionResult NasilKullanilir()
        {
            var contentName = HttpContext.Session.GetString("SessionUserName");
            ViewBag.UserNameSession = contentName;
            if (ViewBag.UserNameSession == null)
            {
                return RedirectToAction("Index", "Anasayfa");
            }

            return View();
        }

        /* Yonetim Paneli Kullanıcıları Listeleme */
        public IActionResult Kullanicilar()
        {
            var contentName = HttpContext.Session.GetString("SessionUserName");
            ViewBag.UserNameSession = contentName;
            if (ViewBag.UserNameSession == null)
            {
                return RedirectToAction("Index", "Anasayfa");
            }

            return View(usersOperations.getUsers());
        }

        /* Yonetim Paneli Kullanici Duzenleme */
        public IActionResult KullaniciDuzenle(int id)
        {
            var contentName = HttpContext.Session.GetString("SessionUserName");
            ViewBag.UserNameSession = contentName;
            if (ViewBag.UserNameSession == null)
            {
                return RedirectToAction("Index", "Anasayfa");
            }

            Users user = usersOperations.getUserById(id);
            if (user == null)
            {
                return RedirectToAction("ErrorAdminPage");
            }
            return View(user);
        }

        /* Yonetim Paneli Kullanici Duzenleme Islemi */
        [HttpPost]
        public IActionResult KullaniciDuzenle(int id, Users newUser)
        {
            Users user = usersOperations.getUserById(id);
            if (user == null)
            {
                return RedirectToAction("ErrorAdminPage");
            }
            usersOperations.updateUser(newUser);
            return RedirectToAction("Kullanicilar");
        }

        /* Yonetim Paneli Site Ayarlar Sayfasi*/
        public IActionResult Ayarlar()
        {
            var contentName = HttpContext.Session.GetString("SessionUserName");
            ViewBag.UserNameSession = contentName;
            if (ViewBag.UserNameSession == null)
            {
                return RedirectToAction("Index", "Anasayfa");
            }

            return View(informationOperations.getInformationById(1));
        }

        /* Yonetim Paneli Site Ayarlari Duzenleme Sayfasi */
        public IActionResult AyarlariDuzenle()
        {
            var contentName = HttpContext.Session.GetString("SessionUserName");
            ViewBag.UserNameSession = contentName;
            if (ViewBag.UserNameSession == null)
            {
                return RedirectToAction("Index", "Anasayfa");
            }

            return View(informationOperations.getInformationById(1));
        }

        /* Yonetim Paneli Site Ayarlarini Duzenleme Islemi */
        [HttpPost]
        public IActionResult AyarlariDuzenle(int id, Information newInformation)
        {
            Information information = informationOperations.getInformationById(id);
            if (information == null)
            {
                return RedirectToAction("ErrorAdminPage");
            }
            informationOperations.updateInformation(newInformation);
            return RedirectToAction("Ayarlar");
        }

        /* Yonetim Paneli Mesajlari Listeleme */
        public IActionResult Mesajlar()
        {
            var contentName = HttpContext.Session.GetString("SessionUserName");
            ViewBag.UserNameSession = contentName;
            if (ViewBag.UserNameSession == null)
            {
                return RedirectToAction("Index", "Anasayfa");
            }

            return View(messagesOperations.getMessages());
        }

        /* Yonetim Paneli Mesaj Okuma Sayfasi */
        public IActionResult MesajOku(int id)
        {
            var contentName = HttpContext.Session.GetString("SessionUserName");
            ViewBag.UserNameSession = contentName;
            if (ViewBag.UserNameSession == null)
            {
                return RedirectToAction("Index", "Anasayfa");
            }

            Messages messages = messagesOperations.getMessageById(id);
            if (messages == null)
            {
                return RedirectToAction("ErrorAdminPage");
            }
            return View(messages);
        }

        /* Yonetim Paneli Mesaj Silme Sayfasi */
        public IActionResult MesajSil(int id)
        {
            var contentName = HttpContext.Session.GetString("SessionUserName");
            ViewBag.UserNameSession = contentName;
            if (ViewBag.UserNameSession == null)
            {
                return RedirectToAction("Index", "Anasayfa");
            }

            Messages messages = messagesOperations.getMessageById(id);
            if (messages == null)
            {
                return RedirectToAction("ErrorAdminPage");
            }
            return View(messages);
        }

        /* Yonetim Paneli Mesaj Silme Islemi */
        [HttpPost]
        public IActionResult MesajSil(int id, IFormCollection collection)
        {
            messagesOperations.deleteMessage(id);
            return RedirectToAction("Mesajlar");
        }

        /* Yonetim Paneli Portfolyo */
        public IActionResult Portfolyo()
        {
            var contentName = HttpContext.Session.GetString("SessionUserName");
            ViewBag.UserNameSession = contentName;
            if (ViewBag.UserNameSession == null)
            {
                return RedirectToAction("Index", "Anasayfa");
            }

            return View(portfolioOperations.getPortfolio());
        }

        /* Yonetim Paneli Portfolyo Ekleme Sayfasi */
        public IActionResult YeniEkle()
        {
            var contentName = HttpContext.Session.GetString("SessionUserName");
            ViewBag.UserNameSession = contentName;
            if (ViewBag.UserNameSession == null)
            {
                return RedirectToAction("Index", "Anasayfa");
            }

            return View();
        }

        /* Yonetim Paneli Portfolyo Ekleme Islemi */
        [HttpPost]
        public async Task<IActionResult> YeniEkle(Portfolio portfolio, IFormFile Image)
        {
            var myPortfolio = new Portfolio();

            if (Image == null || Image.Length == 0)
            {
                return RedirectToAction("ErrorAdminPage");
            }

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", Image.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await Image.CopyToAsync(stream);
            }

            myPortfolio.Image = Image.FileName;

            myPortfolio.Title = portfolio.Title;
            myPortfolio.Description = portfolio.Description;
            myPortfolio.CreatedDate = DateTime.Now.ToShortDateString();
            portfolioOperations.addPortfolio(myPortfolio);

            return RedirectToAction("Portfolyo");
        }


        /* Yonetim Paneli Portfolyo Goruntuleme Sayfasi */
        public IActionResult PortfolioIncele(int id)
        {
            var contentName = HttpContext.Session.GetString("SessionUserName");
            ViewBag.UserNameSession = contentName;
            if (ViewBag.UserNameSession == null)
            {
                return RedirectToAction("Index", "Anasayfa");
            }

            Portfolio portfolio = portfolioOperations.getPortfolioById(id);
            if (portfolio == null)
            {
                return RedirectToAction("ErrorAdminPage");
            }
            return View(portfolio);
        }

        /* Yonetim Paneli Portfolyo Silme Sayfasi */
        public IActionResult PortfolioSil(int id)
        {
            var contentName = HttpContext.Session.GetString("SessionUserName");
            ViewBag.UserNameSession = contentName;
            if (ViewBag.UserNameSession == null)
            {
                return RedirectToAction("Index", "Anasayfa");
            }

            Portfolio portfolio = portfolioOperations.getPortfolioById(id);
            if (portfolio == null)
            {
                return RedirectToAction("ErrorAdminPage");
            }
            return View(portfolio);
        }

        /* Yonetim Paneli Portfolyo Silme Islemi */
        [HttpPost]
        public IActionResult PortfolioSil(int id, IFormCollection collection)
        {
            Portfolio portfolio = portfolioOperations.getPortfolioById(id);
            if (portfolio == null)
            {
                return RedirectToAction("ErrorAdminPage");
            }
            if (System.IO.File.Exists(Directory.GetCurrentDirectory() + "\\wwwroot\\img\\" + portfolio.Image))
            {
                System.IO.File.Delete(Directory.GetCurrentDirectory() + "\\wwwroot\\img\\" + portfolio.Image);
            }
            portfolioOperations.deletePortfolio(id);
            return RedirectToAction("Portfolyo");
        }

        /* Yonetim Paneli Portfolyo Guncelleme Sayfasi */
        public IActionResult PortfolioGuncelle(int id)
        {
            var contentName = HttpContext.Session.GetString("SessionUserName");
            ViewBag.UserNameSession = contentName;
            if (ViewBag.UserNameSession == null)
            {
                return RedirectToAction("Index", "Anasayfa");
            }

            Portfolio portfolio = portfolioOperations.getPortfolioById(id);
            if (portfolio == null)
            {
                return RedirectToAction("ErrorAdminPage");
            }
            return View(portfolio);
        }

        /* Yonetim Paneli Portfolyo Guncelleme Islemi */
        [HttpPost]
        public async Task<IActionResult> PortfolioGuncelle(int id, Portfolio myPortfolio, IFormFile Image)
        {
            Portfolio portfolio = portfolioOperations.getPortfolioById(id);
            if (portfolio == null)
            {
                return RedirectToAction("ErrorAdminPage");
            }
      
            if (Image != null)
            {
                if (System.IO.File.Exists(Directory.GetCurrentDirectory() + "\\wwwroot\\img\\" + portfolio.Image))
                {
                    System.IO.File.Delete(Directory.GetCurrentDirectory() + "\\wwwroot\\img\\" + portfolio.Image);
                }

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", Image.FileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await Image.CopyToAsync(stream);
                }

                portfolio.Image = Image.FileName;
            }

            portfolio.Title = myPortfolio.Title;
            portfolio.Description = myPortfolio.Description;
            portfolioOperations.updatePortfolio(portfolio);
            return RedirectToAction("Portfolyo");
        }



    }
}