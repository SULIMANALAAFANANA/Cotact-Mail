using Cotact_Mail.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Cotact_Mail.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult ContactUs()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ContactUs(SendMail sendMail)
        {
            if (!ModelState.IsValid) return View();

            try
            {
                MailMessage mail = new MailMessage();
                // you need to enter your mail address
                mail.From = new MailAddress("Suliman9315@gmail.com");

                //To Email Address - your need to enter your to email address
                mail.To.Add("Suliman9315@gmail.com");

                mail.Subject = sendMail.Subject;
                //you can  specify also  CC and BCC - i will skip this
                //mail.CC.Add("");
                //mail.Bcc.Add("");

                mail.IsBodyHtml = true;

                string content = "Name : " + sendMail.Name;
                content += "<br/> Message : " + sendMail.Message;

                mail.Body = content;


                //create SMTP instant

                //you need to pass mail server address and you can also specify the port number if you required
                SmtpClient smtpClient = new SmtpClient("Suliman9315@gmail.com");

                //Create nerwork credential and you need to give from email address and password
                NetworkCredential networkCredential = new NetworkCredential("Suliman9315@gmail.com", "PureIt_123");
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = networkCredential;
                smtpClient.Port = 25; //this is default port number - you can also change this
                smtpClient.EnableSsl = false; // if ssl required you need to enable it
                smtpClient.Send(mail);


                ViewBag.Message = "Mail Send";


                // now i need to create the form
                ModelState.Clear();



            }
            catch (Exception ex)
            {
                //If any error occured it will show
                ViewBag.Message = ex.Message.ToString();
            }


            return View();
        }
    }
}
