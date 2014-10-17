using System;

namespace RealMembership.Logins
{
    /// <summary>
    /// Defines a class that represents the result of a 'Set Password' operation.
    /// </summary>
    public class SetPasswordResult : ResultBase
    {
        /// <summary>
        /// Gets or sets the result of the 'Set Password' process
        /// </summary>
        /// <returns></returns>
        public SetPasswordResultType Result
        {
            get;
            set;
        }
    }

    public enum SetPasswordResultType
    {
        /// <summary>
        /// Defines that the password was set to the given new password.
        /// </summary>
        PasswordSetToNew,

        /// <summary>
        /// Defines that the given password was invalid because it was null or empty.
        /// </summary>
        NullOrEmptyPassword,

        /// <summary>
        /// Defines that the given password was too short and therefore was not set as the new password.
        /// </summary>
        TooShort,

        /// <summary>
        /// Defines that the given password did not have enough lowercase letters to be a valid password.
        /// </summary>
        NotEnoughLowerCase,

        /// <summary>
        /// Defines that the given password did not have enough uppercase letters to be a valid password.
        /// </summary>
        NotEnoughUpperCase,

        /// <summary>
        /// Defines that the given password did not have enough digits to be a valid password.
        /// </summary>
        NotEnoughDigits,

        /// <summary>
        /// Defines that the given password did not have enough symbols/punctuation/(characters that are not letters or digits) to be a valid password.
        /// </summary>
        NotEnoughSymbols,

        /// <summary>
        /// Defines that the account is not active, meaning that is has either been closed or not been activated by an admin yet.
        /// </summary>
        LoginNotActive,

        /// <summary>
        /// Defines that the login has not been verified yet.
        /// </summary>
        LoginNotVerified,

        /// <summary>
        /// Defines that the account is locked due to too many incorrect login attempts in a row.
        /// </summary>
        AccountLockedOut,

        /// <summary>
        /// Defines that the set password process failed for a different reason. (not active, not validated, ect.)
        /// </summary>
        OtherReasonForFailure
    }
}