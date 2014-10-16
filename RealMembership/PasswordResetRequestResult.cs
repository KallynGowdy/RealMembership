using System;
using System.ComponentModel;

namespace RealMembership
{
    /// <summary>
    /// Defines a class that represents the result of the password reset request process.
    /// </summary>
    public sealed class PasswordResetRequestResult : IResult
    {
        /// <summary>
        /// Gets the message that describes the result.
        /// </summary>
        public string Message
        {
            get;
            set;
        }

        /// <summary>
        /// Gets whether the operation was successful.
        /// </summary>
        public bool Successful
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the literal value that describes the result.
        /// </summary>
        /// <returns></returns>
        public PasswordResetRequestResultType Result
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the code that can be used to reset the user's password.
        /// </summary>
        /// <returns></returns>
        public string Code
        {
            get;
            set;
        }
    }

    /// <summary>
    /// Defines a list of values that represent the possible results of a password reset request.
    /// </summary>
    public enum PasswordResetRequestResultType
    {
        /// <summary>
        /// Defines that the reset process was started successfully.
        /// </summary>
        [Description("The verification code has been sent.")]
        Success,

        /// <summary>
        /// Defines that the account that the password reset was requested for is non-existant.
        /// </summary>
        [Description("The account does not exist, the password reset process could not be started.")]
        NonExistantAccount,

        /// <summary>
        /// Defines that the account has not yet been verified, so a password reset cannot be started.
        /// </summary>
        [Description("The account is not verified yet, the password reset process could not be started.")]
        NotVerified,

        /// <summary>
        /// Defines that the account is not active, so a password reset cannot be started.
        /// </summary>
        [Description("The account is not active, the password reset process could not be started.")]
        NotActive,
    }
}