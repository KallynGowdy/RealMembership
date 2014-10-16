using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealMembership.Logins
{
    /// <summary>
    /// Defines am abstract class that represents an <see cref="IEmailLogin{TAccount, TDate}"/>.
    /// </summary>
    public abstract class EmailLogin<TAccount, TDateTime> : PasswordLogin<TAccount, TDateTime>, IEmailLogin<TAccount, TDateTime>
        where TAccount : IUserAccount<TAccount, TDateTime>
        where TDateTime : struct
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmailLogin"/> class.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <exception cref="ArgumentException">Must not be null or just whitespace;email</exception>
        public EmailLogin(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Must not be null or just whitespace", "email");
            this.EmailAddress = email;
        }

        /// <summary>
        /// Gets or sets the email address of the user account.
        /// </summary>
        [Required]
        [EmailAddress]
        public string EmailAddress
        {
            get;
            set;
        }
    }
}
