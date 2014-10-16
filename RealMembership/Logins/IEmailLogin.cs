using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealMembership.Logins
{
    /// <summary>
    /// Defines a class that represents a user's login with an email address.
    /// </summary>
    public interface IEmailLogin<TAccount, TDate> : ILogin<TAccount, TDate>
        where TAccount : IUserAccount<TAccount, TDate>
        where TDate : struct
    {
        /// <summary>
        /// Gets or sets the email address of the user account.
        /// </summary>
        /// <returns></returns>
        string EmailAddress
        {
            get;
            set;
        }
    }
}
