using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealMembership.Logins
{
    /// <summary>
    /// Defines an interface that represents an audit of an attempt to reset the password of a login.
    /// </summary>
    public interface IPasswordResetAttempt<TAccount, TDateTime> : ILoginSecurityEvent<TAccount, TDateTime>
        where TAccount : IUserAccount<TAccount, TDateTime>
        where TDateTime : struct
    {
        /// <summary>
        /// Gets or sets the result for the first phase of the password reset process. 
        /// The result that came of the request for the password reset code.
        /// </summary>
        /// <returns></returns>
        PasswordResetRequestResultType? RequestCodeResult
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the result for the second phase of the password reset process.
        /// The result of attempting to finish the password reset process by providing the issued code and a new password.
        /// Null if this phase has not been reached yet.
        /// </summary>
        /// <returns></returns>
        PasswordResetFinishType? FinishResetResult
        {
            get;
            set;
        }

        /// <summary>
        /// Gets whether the reset attempt has been finished or closed. (No longer valid)
        /// </summary>
        /// <returns></returns>
        bool Finished
        {
            get;
        }
    }
}
