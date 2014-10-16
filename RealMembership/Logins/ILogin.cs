using System;
using System.Collections.Generic;

namespace RealMembership.Logins
{
    /// <summary>
    /// Defines an interface that represents an authentication method for a user.
    /// </summary>
    public interface ILogin<TAccount, TDate> : IHasId 
        where TAccount : IUserAccount<TAccount, TDate> 
        where TDate : struct
    {

        /// <summary>
        /// Gets the collection of login attempts that have been made against this login.
        /// If null then login attempts should not be recorded.
        /// </summary>
        /// <returns></returns>
        ICollection<ILoginAttempt<TAccount, TDate>> LoginAttempts
        {
            get;
        }

        /// <summary>
        /// Gets or sets whether this login is verified or not.
        /// </summary>
        /// <returns></returns>
        bool IsVerified
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets whether this login requires verification before it can be used.
        /// </summary>
        /// <returns></returns>
        bool RequiresVerification
        {
            get;
            set;
        }

        /// <summary>
        /// Attempts to verify the login using the given verification code.
        /// </summary>
        /// <param name="code">The code that should be used the verify the login.</param>
        /// <returns></returns>
        bool Verify(string code);

        /// <summary>
        /// Retrieves a new verification code for this login. (and therefore invalidates the current verification code)
        /// </summary>
        /// <returns>Returns a new string representing the new verification code or null if verification cannot be performed on this login. (already verified, etc.)</returns>
        string RequestVerificationCode();

        /// <summary>
        /// Gets or sets whether this login can currently be used. (i.e. whether it is active or not)
        /// </summary>
        /// <returns></returns>
        bool IsCurrentlyActive
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the account that this login belongs to.
        /// </summary>
        /// <returns></returns>
        TAccount Account
        {
            get;
            set;
        }
    }
}