using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealMembership.Logins.SecurityEvents
{
    /// <summary>
    /// Defines a class that provides an implementation of <see cref="IPasswordResetAttempt"/>.
    /// </summary>
    public class PasswordResetAttempt : LoginSecurityEvent, IPasswordResetAttempt
    {
        /// <summary>
        /// Gets the type of event that is being recorded.
        /// </summary>
        public override SecurityEventType EventType
        {
            get
            {
                return SecurityEventType.PasswordResetAttempt;
            }
        }

        /// <summary>
        /// Gets whether the event was performed successfully.
        /// </summary>
        public override bool Successful
        {
            get
            {
                return Finished &&
                    new[]
                    {
                        PasswordResetFinishType.PasswordReset
                    }.Contains(FinishResetResult.GeneralResult);
            }
        }

        /// <summary>
        /// Gets whether the reset attempt has been finished or closed. (No longer valid)
        /// </summary>
        /// <returns></returns>
        public bool Finished
        {
            get
            {
                return RequestCodeResult != null &&
                    FinishResetResult != null && FinishTime.HasValue;
            }
        }

        /// <summary>
        /// Gets or sets the result for the second phase of the password reset process.
        /// The result of attempting to finish the password reset process by providing the issued code and a new password.
        /// Null if this phase has not been reached yet.
        /// </summary>
        /// <returns></returns>
        public PasswordResetFinishResult FinishResetResult
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the result for the first phase of the password reset process. 
        /// The result that came of the request for the password reset code.
        /// </summary>
        /// <returns></returns>
        public PasswordResetRequestResult RequestCodeResult
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the time that the password reset process was finished at.
        /// </summary>
        /// <returns></returns>
        public DateTimeOffset? FinishTime
        {
            get;
            set;
        }
    }
}
