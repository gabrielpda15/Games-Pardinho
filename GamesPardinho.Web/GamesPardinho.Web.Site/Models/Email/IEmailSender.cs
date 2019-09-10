using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace GamesPardinho.Web.Site.Models.Email
{
    public interface IEmailSender
    {
        Task SendEmailAsync(MailMessage mail);
    }
}
