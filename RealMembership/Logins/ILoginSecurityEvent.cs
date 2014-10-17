using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealMembership.Logins
{
    /// <summary>
    /// Defines an interface for objects that represent a security event that occured for an account.
    /// </summary>
    /// <typeparam name="TAccount">The type of the account.</typeparam>
    /// <typeparam name="TDateTime">The type of the date.</typeparam>
    public interface ILoginSecurityEvent<TAccount, TDateTime>
        where TAccount : IUserAccount<TAccount, TDateTime>
        where TDateTime : struct
    {
        /// <summary>
        /// Gets or sets the ID of this login event.
        /// </summary>
        /// <returns></returns>
        long Id
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the time that the event occured at.
        /// </summary>
        /// <returns></returns>
        TDateTime TimeOfEvent
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the login that the event was against.
        /// Null if the event could not be matched to a login. (i.e. non-existant account)
        /// </summary>
        /// <returns></returns>
        ILogin<TAccount, TDateTime> Login
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the tenant that the login was for.
        /// </summary>
        /// <returns></returns>
        string Tenant
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the type of event that is being recorded.
        /// </summary>
        /// <returns></returns>
        SecurityEventType EventType
        {
            get;
        }

        /// <summary>
        /// Gets whether the event was performed successfully.
        /// </summary>
        /// <returns></returns>
        bool Successful
        {
            get;
        }

        /// <summary>
        /// Gets or sets the identification (the specific username, email, phone number, ect.) that was used for the attempt.
        /// </summary>
        /// <returns></returns>
        string LoginIdentification
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the type of identification used ('Username', 'Email', ect.) for the attempt.
        /// </summary>
        /// <returns></returns>
        IdentificationType? IdentificationType
        {
            get;
            set;
        }
    }

    /// <summary>
    /// Defines a list of values that represent the type of security event that occurred on a login.
    /// </summary>
    public enum SecurityEventType
    {
        /// <summary>
        /// Defines that the event was a login attempt against the login.
        /// </summary>
        LoginAttempt,

        /// <summary>
        /// Defines that the event was an attempt to validate/confirm the login.
        /// </summary>
        LoginValidationAttempt,

        /// <summary>
        /// Defines that the event was an attempt to reset the password of the login.
        /// </summary>
        PasswordResetAttempt,

        /// <summary>
        /// Defines that the event was an attempt to deactivate the login.
        /// </summary>
        DeactivationAttempt
    }

    public enum IdentificationType
    {
        /// <summary>
        /// Defines that the type of identification provided was a username.
        /// </summary>
        Username,

        /// <summary>
        /// Defines that the type of identification provided was an email address.
        /// </summary>
        Email,

        /// <summary>
        /// Defines that the type of identification provided was a password reset code.
        /// </summary>
        ResetCode,

        /// <summary>
        /// Defines that the type of identification provided was a login verification code.
        /// </summary>
        VerificationCode
    }
}
