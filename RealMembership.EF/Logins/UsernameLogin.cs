using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealMembership.Logins;

namespace RealMembership.Implementation.EF.Logins
{
    /// <summary>
    /// Defines an abstract class that provides a basic implementation of <see cref="IUsernameLogin{TAccount, DateTimeOffset}"/>
    /// </summary>
    public abstract class UsernameLogin<TAccount> : PasswordLogin<TAccount>, IUsernameLogin<TAccount, DateTimeOffset>
        where TAccount : IUserAccount<TAccount, DateTimeOffset>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UsernameLogin{TAccount}"/> class.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="email">The email.</param>
        protected UsernameLogin(string username, string email) : base(email)
        {
            if (string.IsNullOrWhiteSpace(username)) throw new ArgumentException("The username must not be null or whitespace","username");
            this.Username = username;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UsernameLogin{TAccount}"/> class.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        protected UsernameLogin(string username) : base()
        {
            if (string.IsNullOrWhiteSpace(username)) throw new ArgumentException("The username must not be null or whitespace", "username");
            this.Username = username;
        }

        /// <summary>
        /// Gets or sets the username that should be used for this login.
        /// </summary>
        public string Username
        {
            get;
            set;
        }
    }
}
