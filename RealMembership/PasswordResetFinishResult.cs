using System;

namespace RealMembership
{
    /// <summary>
    /// Defines a class that represents the result of finishing a password reset process.
    /// </summary>
    public sealed class PasswordResetFinishResult<TAccount, TDateTime> : IResult
        where TAccount : IUserAccount<TAccount, TDateTime>
        where TDateTime : struct
    {
        /// <summary>
        /// Gets or sets the message that describes the result.
        /// </summary>
        public string Message
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets whether the operation was successful.
        /// </summary>
        public bool Successful
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the login that the reset was for.
        /// Null if not sucessful.
        /// </summary>
        /// <returns></returns>
        public IPasswordLogin<TAccount, TDateTime> Login
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the type of the result.
        /// </summary>
        /// <returns></returns>
        public PasswordResetFinishType Result
        {
            get;
            set;
        }
    }

    /// <summary>
    /// Defines a list of values that describe a possible outcome for the password reset finishing process.
    /// </summary>
    public enum PasswordResetFinishType
    {
        /// <summary>
        /// Defines that the password was successfully reset.
        /// </summary>
        PasswordReset,

        /// <summary>
        /// Defines that the code was invalid.
        /// </summary>
        InvalidCode,

        /// <summary>
        /// Defines that the new password was invalid.
        /// </summary>
        InvalidPassword
    }
}