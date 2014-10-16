using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealMembership
{
    /// <summary>
    /// Defines an interface for an object that can send SMS texts to a phone number.
    /// </summary>
    public interface ISmsService
    {
        /// <summary>
        /// Sends the given string of text to the given phone number over SMS.
        /// </summary>
        /// <param name="phoneNumber">The phone number that the SMS should be sent to.</param>
        /// <param name="text">The text that should be sent to the phone number.</param>
        void SendSms(string phoneNumber, string text);
    }
}
