using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog001.DataAccessLayer;
using Blog001.Models;
using Microsoft.AspNetCore.Mvc;

namespace Blog001.Controllers
{
    public class AnasayfaController : Controller
    {

        InformationOperations informationOperations = new InformationOperations();
        PortfolioOperations portfolioOperations = new PortfolioOperations();
        MessagesOperations messagesOperations = new MessagesOperations();

        public IActionResult Index()
        {
            var information = informationOperations.getInformationById(1);
            var portfolio = portfolioOperations.getPortfolio();

            ViewModels viewModels = new ViewModels();
            viewModels.information = information;
            viewModels.portfolios = portfolio;

            return View(viewModels);
        }

        public IActionResult Detay(int id)
        {
            var portfolyo = portfolioOperations.getPortfolioById(id);
            if (portfolyo == null)
            {
                return RedirectToAction("ErrorPage");
            }
            return View(portfolyo);
        }

        public IActionResult ErrorPage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Iletisim(Messages messages)
        {
            if (messages.Title == null) { messages.Title = "Null"; }
            if (messages.NameSurname == null) { messages.NameSurname = "Null"; }
            if (messages.MailAdress == null) { messages.MailAdress = "Null"; }
            if (messages.Message == null) { messages.Message = "Null"; }
            messages.CreatedDate = DateTime.Now.ToShortDateString();
            messagesOperations.addMessage(messages);
            return RedirectToAction("Index");
        }

    }
}