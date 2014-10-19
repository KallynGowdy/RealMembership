using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealMembership.Logins.SecurityEvents
{
    /// <summary>
    /// Defines a class that provides a basic implementation of <see cref="IVerificationRequestAttempt"/>.
    /// </summary>
    public class VerificationRequestAttempt : LoginSecurityEvent, IVerificationRequestAttempt
    {
        /// <summary>
        /// Gets the type of event that is being recorded.
        /// </summary>
        public override SecurityEventType EventType
        {
            get
            {
                return SecurityEventType.LoginValidationAttempt;
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
                        VerificationResultType.AlreadyVerified,
                        VerificationResultType.LoginVerified
                    }.Contains(VerificationAttemptResult.Result);
            }
        }

        /// <summary>
        /// Gets whether the entire verification process has been finished.
        /// </summary>
        public bool Finished
        {
            get
            {
                return RequestAttemptResult != null &&
                    VerificationAttemptResult != null && FinishTime.HasValue;
            }
        }

        /// <summary>
        /// Gets or sets the time that the verification process was finished at.
        /// </summary>
        /// <returns></returns>
        public DateTimeOffset? FinishTime
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the result of the verification code request attempt.
        /// </summary>
        public VerificationRequestResult RequestAttemptResult
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the result of the verification attempt. The attempt of trying to verify the login with a code.
        /// </summary>
        public VerificationResult VerificationAttemptResult
        {
            get;
            set;
        }
    }
}
