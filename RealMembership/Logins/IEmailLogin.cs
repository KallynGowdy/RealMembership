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
    /// Defines a class that represents a user's login with an email address.
    /// </summary>
    public interface IEmailLogin<TAccount, TDate> : ILogin<TAccount, TDate>
        where TAccount : IUserAccount<TAccount, TDate>
        where TDate : struct
    {
        /// <summary>
        /// Gets the email address of the user account.
        /// </summary>
        /// <returns></returns>
        string EmailAddress
        {
            get;
        }

        /// <summary>
        /// Sets the email address to the given value and returns a result specifying whether the attempt was successful.
        /// </summary>
        /// <param name="newEmail">The new email address.</param>
        /// <returns></returns>
        Task<SetEmailResult> SetEmailAddressAsync(string newEmail);
    }
}
