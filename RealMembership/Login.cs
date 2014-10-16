using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealMembership
{
    /// <summary>
    /// Defines an abstract class that provides a base for <see cref="ILogin"/>.
    /// </summary>
    public abstract class Login<TAccount, TDateTime> : ILogin<TAccount, TDateTime> 
        where TAccount : IUserAccount<TAccount, TDateTime> 
        where TDateTime : struct
    {
        /// <summary>
        /// Gets or sets the account that this login belongs to.
        /// </summary>
        /// <returns></returns>
        public virtual TAccount Account
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the ID of the object.
        /// </summary>
        public long Id
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets whether this login can currently be used. (i.e. whether it is active or not)
        /// </summary>
        public virtual bool IsCurrentlyActive
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets whether this login is verified or not.
        /// </summary>
        public virtual bool IsVerified
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets whether this login requires verification before it can be used.
        /// </summary>
        public bool RequiresVerification
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the verifcation code for this email.
        /// </summary>
        /// <returns></returns>
        public string VerificationCode
        {
            get;
            set;
        }

        /// <summary>
        /// Attempts to verify the login using the given verification code.
        /// </summary>
        /// <param name="code">The code that should be used the verify the login.</param>
        /// <returns></returns>
        public bool Verify(string code)
        {
            if (!IsVerified)
            {
                IsVerified = VerificationCode.Equals(code, StringComparison.Ordinal);
            }
            return IsVerified;
        }
    }
}
