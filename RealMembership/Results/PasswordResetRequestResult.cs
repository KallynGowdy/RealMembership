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
using System.ComponentModel;

namespace RealMembership
{
    /// <summary>
    /// Defines a class that represents the result of the password reset request process.
    /// </summary>
    public sealed class PasswordResetRequestResult : ResultBase
    {

        /// <summary>
        /// Gets or sets the literal value that describes the result.
        /// </summary>
        /// <returns></returns>
        public PasswordResetRequestResultType Result
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the code that can be used to reset the user's password.
        /// </summary>
        /// <returns></returns>
        public string Code
        {
            get;
            set;
        }
    }

    /// <summary>
    /// Defines a list of values that represent the possible results of a password reset request.
    /// </summary>
    public enum PasswordResetRequestResultType
    {
        /// <summary>
        /// Defines that the reset process was started successfully.
        /// </summary>
        [Description("The verification code has been sent.")]
        Success,

        /// <summary>
        /// Defines that the login that the password reset was requested for is non-existant.
        /// </summary>
        [Description("The login does not exist, the password reset process could not be started.")]
        NonExistantLogin,

        /// <summary>
        /// Defines that the account has not yet been verified, so a password reset cannot be started.
        /// </summary>
        [Description("The account is not verified yet, the password reset process could not be started.")]
        LoginNotVerified,

        /// <summary>
        /// Defines that the account is not active, so a password reset cannot be started.
        /// </summary>
        [Description("The account is not active, the password reset process could not be started.")]
        LoginNotActive,

        /// <summary>
        /// Defines that the account is locked due to too many incorrect login attempts in a row.
        /// </summary>
        AccountLockedOut,

        /// <summary>
        /// Defines that the set password process failed for a different reason. (not active, not validated, ect.)
        /// </summary>
        OtherReasonForFailure
    }
}