using AvisFormationsCore.WebUi.Models;
using Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace AvisFormationsCore.WebUi.Controllers
{
    public class ContactController : Controller
    {
        IContactRepository _context;
        public ContactController(IContactRepository context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var vm = new EnvoiEmailViewModel();
            return View(vm);
        }
        public IActionResult SaveMessage(EnvoiEmailViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", viewModel);
            }
            _context.SaveMessage(viewModel.Nom, viewModel.Email, viewModel.Message);
            return View();
        }

        //[HttpPost]
        public IActionResult EnvoyerEmail(string nom, string email, string message)
        {
            try
            {
                MailMessage mm = new MailMessage();
                mm.From = new MailAddress(email);
                mm.To.Add(new MailAddress("oury.familles@gmail.com"));
                mm.Subject = "Nouveau message Avis Formations";
                mm.Body = message;

                mm.IsBodyHtml = false;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential(email, "efKMNMIcgTamo2LfEbkp");
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);

                //MailMessage msg = new MailMessage();
                //msg.From = new System.Net.Mail.MailAddress(email);
                //msg.To.Add(new System.Net.Mail.MailAddress("damadououry18@gmail.com"));
                //msg.Subject = "Nouveau message Avis Formations";

                //// you need to use Index because that is the name declared above
                //msg.Body = message;
                //var client = new SmtpClient
                //{
                //    Host = "smtp.gmail.com",
                //    Port = 587,
                //    EnableSsl = true,
                //    DeliveryMethod = SmtpDeliveryMethod.Network,
                //    UseDefaultCredentials = false,
                //    Timeout = 30 * 1000,
                //    Credentials = new NetworkCredential("damadououry18@gmail.com", "Dalan224.cosa")

                //};
                //client.Send(msg);

                //var fromAddress = new MailAddress(email, nom);
                //var toAddress = new MailAddress("oury.familles@gmail.com", "Oury");
                //const string fromPassword = "Dalan224.cosa";
                //const string subject = "Nouveau message Avis Formations";

                //var smtp = new SmtpClient
                //{
                //    Host = "smtp.gmail.com",
                //    Port = 587,
                //    EnableSsl = true,
                //    DeliveryMethod = SmtpDeliveryMethod.Network,
                //    UseDefaultCredentials = true,
                //    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                //};
                //using (var msg = new MailMessage(fromAddress, toAddress)
                //{
                //    Subject = subject,
                //    Body = message
                //})

                //smtp.Send(msg);
                

            }
            catch
            {
                return View("ErreurEnvoiEmail");
                //return View("Erreur Envoi");
            }

            return View("Merci");

        }
    }
}
