using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealMembership.Logins
{
    /// <summary>
    /// Defines an abstract class that provides an implementation of <see cref="IUsernameLogin"/>.
    /// </summary>
    public class UsernameLogin : PasswordLogin, IUsernameLogin
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="UsernameLogin"/> class.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <exception cref="System.ArgumentException">Must not be null or white space;username</exception>
        protected UsernameLogin(string username)
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
