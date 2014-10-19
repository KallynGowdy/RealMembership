using System;
using RealMembership.Logins;

namespace RealMembership.Logins
{
    /// <summary>
    /// Defines an abstract class that provides a basic implementation of <see cref="IPhoneLogin"/>.
    /// </summary>
    public class PhoneLogin : Login, IPhoneLogin
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PhoneLogin"/> class.
        /// </summary>
        /// <param name="phoneNumber">The phone number.</param>
        /// <exception cref="System.ArgumentException">The given phone number must not be null or whitespace.</exception>
        protected PhoneLogin(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber)) throw new ArgumentException("The given phone number must not be null or whitespace.", phoneNumber);
            this.PhoneNumber = phoneNumber;
        }

        /// <summary>
        /// Gets or sets the phone number used for this login.
        /// </summary>
        public virtual string PhoneNumber
        {
            get;
            set;
        }
    }
}