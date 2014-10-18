//   Copyright 2014 Kallyn Gowdy
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
using System.Threading.Tasks;

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
        /// Gets the collection of security events that have occured for this login.
        /// If null then security should not be recorded.
        /// </summary>
        /// <returns></returns>
        ICollection<LoginSecurityEvent<TAccount, TDate>> SecurityEvents
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
        Task<VerificationResult> VerifyAsync(string code);

        /// <summary>
        /// Retrieves a new verification code for this login. (and therefore invalidates the current verification code)
        /// </summary>
        /// <returns>Returns a new string representing the new verification code or null if verification cannot be performed on this login. (already verified, etc.)</returns>
        Task<VerificationRequestResult> RequestVerificationCodeAsync();

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