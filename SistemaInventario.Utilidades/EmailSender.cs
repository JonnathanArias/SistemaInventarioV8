using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.Utilidades
{
    public class EmailSender : IEmailSender
    {
        //Configuracion del EmailSender 
        public string SendGridSecret { get; set; }

        public EmailSender(IConfiguration _config)
        {
            //vamos acceder a nuestro apps seting .json donde configuramos el key 

            SendGridSecret = _config.GetValue<string>("Sendgrid:Secretkey");


        }
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var client = new SendGridClient(SendGridSecret);
            var from = new SendGrid.Helpers.Mail.EmailAddress("jopaka13@hotmail.com");
            var to = new SendGrid.Helpers.Mail.EmailAddress(email);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, "", htmlMessage);

            return client.SendEmailAsync(msg);
        }
    }
}
