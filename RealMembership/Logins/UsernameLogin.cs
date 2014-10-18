using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealMembership.Logins
{
    /// <summary>
    /// Defines an abstract class that provides an implementation of <see cref="IUsernameLogin{TAccount, TDateTime}"/>.
    /// </summary>
    public abstract class UsernameLogin<TAccount, TDateTime> : PasswordLogin<TAccount, TDateTime>, IUsernameLogin<TAccount, TDateTime>
        where TAccount : IUserAccount<TAccount, TDateTime>
        where TDateTime : struct
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UsernameLogin{TAccount, TDateTime}"/> class.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="username">The username.</param>
        /// <exception cref="System.ArgumentException">Must not be null or white space;username</exception>
        protected UsernameLogin(string email, string username) : base(email)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UsernameLogin{TAccount, TDateTime}"/> class.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <exception cref="System.ArgumentException">Must not be null or white space;username</exception>
        protected UsernameLogin(string email, string username, string password) : base(email, password)
        {
            if (string.IsNullOrWhiteSpace(username)) throw new ArgumentException("Must not be null or white space", "username");
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
