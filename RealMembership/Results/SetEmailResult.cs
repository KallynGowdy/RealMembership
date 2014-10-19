using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealMembership.Logins;

namespace RealMembership
{
    /// <summary>
    /// Defines a class that contains information about the result of an attempt to set the email address of a <see cref="IEmailLogin{TAccount, TDate}"/>.
    /// </summary>
    public sealed class SetEmailResult : ResultBase
    {
        /// <summary>
        /// Gets or sets the type of result that was produced from the attempt.
        /// </summary>
        /// <returns></returns>
        public SetEmailResultType Result
        {
            get;
            set;
        }
    }

    /// <summary>
    /// Defines a list of values that represent possible results of an attempt to set the email address of a login.
    /// </summary>
    public enum SetEmailResultType
    {
        /// <summary>
        /// Defines that the email address given was valid.
        /// </summary>
        ValidEmail,

        /// <summary>
        /// Defines that the email address was not valid.
        /// </summary>
        NotValidEmail,

        /// <summary>
        /// Defines that the email address was not valid because it contained an invalid character.
        /// </summary>
        ContainsInvalidCharacter,

        /// <summary>
        /// Defines that the email could not be set because the login is not valid.
        /// </summary>
        LoginNotActive,

        /// <summary>
        /// Defines that the email could not be set because the login could not be found.
        /// </summary>
        LoginNotFound
    }
}
