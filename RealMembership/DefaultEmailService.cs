using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace RealMembership
{
    /// <summary>
    /// Defines a class that provides a default implementation of <see cref="IEmailService"/>.
    /// </summary>
    public class DefaultEmailService : IEmailService
    {
        System.Net.Mail.SmtpClient client;

        string sender;

        public DefaultEmailService(SmtpClient client, string sender)
        {
            this.client = client;
            this.sender = sender;
        }

        public async Task SendEmailAsync(EmailMessage message)
        {
            MailMessage m = new MailMessage(sender, message.Recipent);
            m.Body = message.Html;
            m.Subject = message.Subject;
            m.IsBodyHtml = true;

            await client.SendMailAsync(m);
        }
    }
}
