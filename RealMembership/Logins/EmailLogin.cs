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

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealMembership.Logins
{
    /// <summary>
    /// Defines an abstract class that represents an <see cref="IEmailLogin"/>.
    /// </summary>
    public abstract class EmailLogin : Login, IEmailLogin
    {

        /// <summary>
        /// Gets or sets the email address of the user account.
        /// </summary>
        [Required]
        [EmailAddress]
        public string EmailAddress
        {
            get;
            set;
        }

        public Task<SetEmailResult> SetEmailAddressAsync(string newEmail)
        {
            SetEmailResult result;
            if (string.IsNullOrWhiteSpace(newEmail) || !new EmailAddressAttribute().IsValid(newEmail))
            {
                result = new SetEmailResult
                {
                    Successful = false,
                    Result = SetEmailResultType.NotValidEmail
                };
            }
            else if (!this.IsCurrentlyActive)
            {
                result = new SetEmailResult
                {
                    Successful = false,
                    Result = SetEmailResultType.LoginNotActive
                };
            }
            else
            {
                this.EmailAddress = newEmail;
                result = new SetEmailResult
                {
                    Successful = true,
                    Result = SetEmailResultType.ValidEmail
                };
            }

            return Task.FromResult(result);
        }
    }
}
