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

namespace RealMembership
{
    /// <summary>
    /// Defines an abstract class that represents the result of the account creation process.
    /// </summary>
    public class AccountCreationResult<TAccount> : ResultBase
        where TAccount : UserAccount
    {
        /// <summary>
        /// Gets or sets the type that defines why the result was successful or not.
        /// </summary>
        /// <returns></returns>
        public AccountCreationResultType Result
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the account that was created as a part of the result.
        /// </summary>
        /// <returns></returns>
        public TAccount CreatedAccount
        {
            get;
            set;
        }
    }

    /// <summary>
    /// Defines a list of values that represent the type of result that came of the account creation process.
    /// </summary>
    public enum AccountCreationResultType
    {
        /// <summary>
        /// Defines that the account was created successfully and the verification code was sent.
        /// </summary>
        CreatedAndSentCode,

        /// <summary>
        /// Defines that the account was created, but the verification code was not sent.
        /// </summary>
        CreatedButCodeNotSent,
        
        /// <summary>
        /// Defines that the given password was invalid.
        /// </summary>
        InvalidPassword,

        /// <summary>
        /// Defines that the given username was invalid.
        /// </summary>
        InvalidUsername,

        /// <summary>
        /// Defines that the given email was invalid.
        /// </summary>
        InvalidEmail,

        /// <summary>
        /// Defines that the given account creation request was invalid in some way or was unsupported.
        /// </summary>
        InvalidRequest
    }
}