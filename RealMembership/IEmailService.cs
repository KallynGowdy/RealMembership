using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealMembership
{
    /// <summary>
    /// Defines an interface for an object that can send emails.
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Sends an email to the given recipiant with the given subject and html.
        /// </summary>
        /// <param name="recipiant">The address that the email should be sent to.</param>
        /// <param name="subject">The subject of the email.</param>
        /// <param name="html">The HTML body of the email.</param>
        void SendEmail(string recipiant, string subject, string html);
    }
}
