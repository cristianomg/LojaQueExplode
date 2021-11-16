using Br.Com.LojaQueExplode.Util.Smtp.Abstract;
using Clinia.FluentMailer.Core;
using Clinia.FluentMailer.Smtp;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Br.Com.LojaQueExplode.Util.Smtp.Concrete
{
    public class SendMailService : ISendMailService
    {
        private readonly string _fromEmail;
        private readonly string _passwordEmail;
        public SendMailService(IConfiguration configuration)
        {
            _fromEmail = configuration.GetSection("SmtpConfig:Email").Value;
            _passwordEmail = configuration.GetSection("SmtpConfig:Password").Value;
        }
        public async Task SendEmail(string email
                                   , string template
                                   , string subjectEmail
                                   , List<Attachment> attchaments = null
                                   , bool isHtml = false)
        {
            var sender = new SmtpSender(() => new SmtpClient(host: "smtp.gmail.com", 587)
            {
                EnableSsl = true,
                Timeout = 10000,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(_fromEmail, _passwordEmail)
            });

            Email.DefaultSender = sender;

            var emailToSend = Email
                .From(emailAddress: _fromEmail)
                .To(emailAddress: email)
                .Subject(subject: subjectEmail)
                .Body(template, isHtml);

            if (attchaments != null && attchaments.Any())
            {
                attchaments.ForEach(attchament =>
                {
                    emailToSend.Attach(new Clinia.FluentMailer.Core.Models.Attachment()
                    {
                        ContentType = "application/pdf",
                        Data = new MemoryStream(attchament.Content),
                        Filename = attchament.Name ?? "Anexo",
                        IsInline = false
                    });
                });
            }

            await emailToSend.SendAsync();
        }
    }
    public class Attachment
    {
        public string Name { get; set; }
        public byte[] Content { get; set; }
    }
}
