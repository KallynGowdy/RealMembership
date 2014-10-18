﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealMembership.Logins.SecurityEvents
{
    /// <summary>
    /// Defines a class that provides an implementation of <see cref="ILoginAttempt{TAccount, TDateTime}"/>.
    /// </summary>
    public class LoginAttempt<TAccount, TDateTime> : LoginSecurityEvent<TAccount, TDateTime>, ILoginAttempt<TAccount, TDateTime>
        where TAccount : IUserAccount<TAccount, TDateTime>
        where TDateTime : struct
    {
        /// <summary>
        /// Gets the type of event that is being recorded.
        /// </summary>
        public override SecurityEventType EventType
        {
            get
            {
                return SecurityEventType.LoginAttempt;
            }
        }

        /// <summary>
        /// Gets whether the event was performed successfully.
        /// </summary>
        public override bool Successful
        {
            get
            {
                return new[]
                {
                    AuthenticationResultType.GoodButRequiresTwoFactor,
                    AuthenticationResultType.ValidCredentialsProvided
                }.Contains(Result.Result);
            }
        }

        /// <summary>
        /// Gets or sets the result that occured from the login attempt.
        /// </summary>
        public AuthenticationResult Result
        {
            get;
            set;
        }
    }
}
