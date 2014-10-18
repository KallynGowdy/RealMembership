﻿//   Copyright 2014 Kallyn Gowdy
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.

using RealMembership.Logins.SecurityEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealMembership.Logins
{
    /// <summary>
    /// Defines an abstract class that provides a base for <see cref="ILogin{TAccount, TDateTime}"/>.
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
        /// Gets whether the account is currently locked because of incorrect login attempts.
        /// </summary>
        /// <returns></returns>
        public abstract bool IsLockedOut { get; }

        /// <summary>
        /// Gets or sets the time that the lockout ends on.
        /// </summary>
        /// <returns></returns>
        public virtual TDateTime? LockoutEndTime { get; set; }

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
        /// Gets the collection of security events that have occured for this login. If null then security should not be recorded.
        /// </summary>
        public virtual ICollection<LoginSecurityEvent<TAccount, TDateTime>> SecurityEvents
        {
            get;
            protected set;
        }

        /// <summary>
        /// Retrieves a new verification code for this login. (and therefore invalidates the current verification code)
        /// </summary>
        /// <returns>Returns a new string representing the new verification code or null if verification cannot be performed on this login. (already verified, etc.)</returns>
        public virtual Task<VerificationRequestResult> RequestVerificationCodeAsync()
        {
            VerificationRequestResultType type;
            if (!IsCurrentlyActive)
            {
                type = VerificationRequestResultType.LoginNotActive;
                VerificationCode = null;
            }
            else if (IsVerified)
            {
                type = VerificationRequestResultType.AlreadyVerified;
                VerificationCode = null;
            }
            else
            {
                VerificationCode = Convert.ToBase64String(CryptoHelpers.GetSecureRandomBytes(CryptoHelpers.DefaultHashSize));
                type = VerificationRequestResultType.NewCodeCreated;
            }
            return Task.FromResult(new VerificationRequestResult
            {
                Code = VerificationCode,
                Successful = type == VerificationRequestResultType.NewCodeCreated,
                Result = type
            });
        }

        /// <summary>
        /// Attempts to verify the login using the given verification code.
        /// </summary>
        /// <param name="code">The code that should be used the verify the login.</param>
        /// <returns></returns>
        public Task<VerificationResult> VerifyAsync(string code)
        {
            VerificationResultType type;
            if (!IsCurrentlyActive)
            {
                type = VerificationResultType.LoginNotActive;
            }
            else if (!IsVerified)
            {
                IsVerified = VerificationCode.Equals(code, StringComparison.Ordinal);
                type = IsVerified ? VerificationResultType.LoginVerified : VerificationResultType.InvalidCode;
            }
            else
            {
                type = VerificationResultType.AlreadyVerified;
            }
            if (IsVerified)
            {
                VerificationCode = null;
            }
            return Task.FromResult(new VerificationResult
            {
                Successful = IsVerified,
                Result = type
            });
        }
    }
}
