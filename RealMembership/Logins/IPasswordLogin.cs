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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealMembership.Logins
{
    /// <summary>
    /// Defines an interface for logins that require a password.
    /// </summary>
    public interface IPasswordLogin<TAccount, TDate> : ILogin<TAccount, TDate>
        where TAccount : IUserAccount<TAccount, TDate>
        where TDate : struct
    {
        /// <summary>
        /// Gets whether the password reset process is currently active for this login.
        /// </summary>
        /// <returns></returns>
        bool IsInResetProcess
        {
            get;
        }

        /// <summary>
        /// Gets or sets the time that the password reset was requested.
        /// </summary>
        /// <returns></returns>
        TDate? ResetRequestTime
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the time that the password reset code expires.
        /// </summary>
        /// <returns></returns>
        TDate? ResetExpireTime
        {
            get;            
        }

        /// <summary>
        /// Determines if the given reset code matches the reset code stored in this login.
        /// Should return false if the reset has expired or was not requested.
        /// </summary>
        /// <param name="code">The code to validated against the stored code.</param>
        /// <returns><c>true</c> if the code is valid for this login, otherwise <c>false</c></returns>
        bool MatchesResetCode(string code);

        /// <summary>
        /// Requests a new password reset code for this login.
        /// </summary>
        /// <returns>Returns a new string representing the password reset code or null if a reset is not allowed.</returns>
        string RequestResetCode();

        /// <summary>
        /// Determines if the given password matches the password stored in this login.
        /// </summary>
        /// <param name="password">The password to validate against the store.</param>
        /// <returns><c>true</c> if the password is valid for this login, otherwise false.</returns>
        bool MatchesPassword(string password);
    }
}
