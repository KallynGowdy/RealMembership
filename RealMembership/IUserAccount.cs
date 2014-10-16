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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealMembership
{
    /// <summary>
    /// Defines an interface that represents a basic user account.
    /// </summary>
    public interface IUserAccount<TAccount, TDateTime> : IHasId, IAuditable<TDateTime> where TDateTime : struct where TAccount : IUserAccount<TAccount, TDateTime>
    {
        /// <summary>
        /// Gets whether the account is currently locked because of incorrect login attempts.
        /// </summary>
        /// <returns></returns>
        bool IsLockedOut
        {
            get;
        }

        /// <summary>
        /// Gets or sets the time that the lockout ends on.
        /// </summary>
        /// <returns></returns>
        TDateTime? LockoutEndTime
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Tenant that this account belongs to.
        /// </summary>
        /// <returns></returns>
        string Tenant
        {
            get;
            set;
        }

        /// <summary>
        /// Gets whether the account requires two factor authentication or not.
        /// </summary>
        /// <returns></returns>
        bool RequiresTwoFactorAuth
        {
            get;
        }

        /// <summary>
        /// Gets or sets the list of logins that this account has.
        /// </summary>
        /// <returns></returns>
        ICollection<ILogin<TAccount, TDateTime>> Logins
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the list of claims that this account has.
        /// </summary>
        /// <returns></returns>
        ICollection<IClaim> Claims
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the display name (non login name, but the name that the user should be refered to by) of this account.
        /// </summary>
        /// <returns></returns>
        string DisplayName
        {
            get;
            set;
        }
    }
}
