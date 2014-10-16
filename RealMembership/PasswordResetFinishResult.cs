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

using RealMembership.Logins;
using System;

namespace RealMembership
{
    /// <summary>
    /// Defines a class that represents the result of finishing a password reset process.
    /// </summary>
    public sealed class PasswordResetFinishResult<TAccount, TDateTime> : IResult
        where TAccount : IUserAccount<TAccount, TDateTime>
        where TDateTime : struct
    {
        /// <summary>
        /// Gets or sets the message that describes the result.
        /// </summary>
        public string Message
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets whether the operation was successful.
        /// </summary>
        public bool Successful
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the login that the reset was for.
        /// Null if not sucessful.
        /// </summary>
        /// <returns></returns>
        public IPasswordLogin<TAccount, TDateTime> Login
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the <see cref="SetPasswordResult"/> that was the result of the password reset process.
        /// </summary>
        /// <returns></returns>
        public SetPasswordResult SetPasswordResult
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the general type of the result.
        /// </summary>
        /// <returns></returns>
        public PasswordResetFinishType GeneralResult
        {
            get;
            set;
        }
    }

    /// <summary>
    /// Defines a list of values that describe a possible outcome for the password reset finishing process.
    /// </summary>
    public enum PasswordResetFinishType
    {
        /// <summary>
        /// Defines that the password was successfully reset.
        /// </summary>
        PasswordReset,

        /// <summary>
        /// Defines that the code was invalid.
        /// </summary>
        InvalidCode,

        /// <summary>
        /// Defines that the new password was invalid.
        /// </summary>
        InvalidPassword
    }
}