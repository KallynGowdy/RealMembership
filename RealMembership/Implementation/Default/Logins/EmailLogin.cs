using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealMembership.Logins;

namespace RealMembership.Implementation.Default.Logins
{
    /// <summary>
    /// Defines an abstract class that provides a basic implementation of <see cref="IEmailLogin{TAccount, DateTimeOffset}"/>.
    /// </summary>
    public abstract class EmailLogin<TAccount> : EmailLogin<TAccount, DateTimeOffset>
        where TAccount : IUserAccount<TAccount, DateTimeOffset>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmailLogin{TAccount}"/> class.
        /// </summary>
        /// <param name="email">The email.</param>
        protected EmailLogin(string email) : base(email) { }

        /// <summary>
        /// Gets whether the account is currently locked because of incorrect login attempts.
        /// </summary>
        public override bool IsLockedOut
        {
            get
            {
                return DateTimeOffset.Now < LockoutEndTime;
            }
        }
    }
}
