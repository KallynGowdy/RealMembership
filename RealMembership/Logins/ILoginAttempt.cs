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

namespace RealMembership.Logins
{
    /// <summary>
    /// Defines an interface for objects that record information about attempted logins.
    /// </summary>
    /// <typeparam name="TAccount">The type of the accounts used.</typeparam>
    /// <typeparam name="TDate">The type of the structue used to contain date information.</typeparam>
    public interface ILoginAttempt<TAccount, TDate>
        where TAccount : IUserAccount<TAccount, TDate>
        where TDate : struct
    {
        /// <summary>
        /// Gets or sets the time that the attempt took place at.
        /// </summary>
        /// <returns></returns>
        TDate TimeOfAttempt
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets whether the attempt was successful.
        /// </summary>
        /// <returns></returns>
        bool Successful
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the result that occured from the login attempt.
        /// </summary>
        /// <returns></returns>
        AuthenticationResultType Result
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the login that the attempt was for.
        /// If null then the attempt was for a non-existant account.
        /// </summary>
        /// <returns></returns>
        ILogin<TAccount, TDate> Login
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the tenant that the login was for.
        /// </summary>
        /// <returns></returns>
        string Tenant
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the identification (the specific username, email, phone number, ect.) that was used for the attempt.
        /// </summary>
        /// <returns></returns>
        string LoginIdentification
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the type of identification used ('Username', 'Email', ect.) for the attempt.
        /// </summary>
        /// <returns></returns>
        string IdentificationType
        {
            get;
            set;
        }
    }
}