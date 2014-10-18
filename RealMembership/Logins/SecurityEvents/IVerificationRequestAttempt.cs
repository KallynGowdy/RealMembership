using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealMembership.Logins.SecurityEvents
{
    /// <summary>
    /// Defines an interface for objects that represents an audit of the process of verifiying a login.
    /// </summary>
    public interface IVerificationRequestAttempt<TAccount, TDateTime> : ILoginSecurityEvent<TAccount, TDateTime>
        where TAccount : IUserAccount<TAccount, TDateTime>
        where TDateTime : struct
    {
        /// <summary>
        /// Gets or sets the time that the verification process was finished at.
        /// </summary>
        /// <returns></returns>
        TDateTime? FinishTime
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the result of the verification code request attempt.
        /// </summary>
        /// <returns></returns>
        VerificationRequestResult RequestAttemptResult
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the result of the verification attempt. The attempt of trying to verify the login with a code.
        /// </summary>
        /// <returns></returns>
        VerificationResult VerificationAttemptResult
        {
            get;
            set;
        }

        /// <summary>
        /// Gets whether the entire verification process has been finished.
        /// </summary>
        /// <returns></returns>
        bool Finished
        {
            get;
        }
    }
}
