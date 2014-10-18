using System;
using RealMembership.Logins;

namespace RealMembership.Logins
{
    /// <summary>
    /// Defines an abstract class that provides a basic implementation of <see cref="IPhoneLogin{TAccount, TDateTime}"/>.
    /// </summary>
    /// <typeparam name="TAccount">The type of the accounts being used.</typeparam>
    /// <typeparam name="TDateTime">The type of dates being used.</typeparam>
    public abstract class PhoneLogin<TAccount, TDateTime> : Login<TAccount, TDateTime>, IPhoneLogin<TAccount, TDateTime>
        where TAccount : IUserAccount<TAccount, TDateTime>
        where TDateTime : struct
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PhoneLogin{TAccount, TDateTime}"/> class.
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