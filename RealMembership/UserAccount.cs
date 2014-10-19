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

using RealMembership.CollectionHelpers;
using RealMembership.Logins;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealMembership
{
    /// <summary>
    /// Defines an abstract base class that implements <see cref="IUserAccount" />.
    /// </summary>
    public class UserAccount
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserAccount"/> class.
        /// </summary>
        protected UserAccount()
        {
            this.Logins = new List<Login>();
            this.Claims = new List<Claim>();
        }

        /// <summary>
        /// Gets or sets the ID of the object.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets whether this account requires two factor authentication or not.
        /// Determined by the presence of a two factor login in this account's list of logins.
        /// </summary>
        /// <returns></returns>
        public virtual bool RequiresTwoFactorAuth
        {
            get
            {
                return Logins.Any(l => l is ITwoFactorLogin);
            }
        }

        /// <summary>
        /// Gets or sets the time that this account was created.
        /// </summary>
        /// <returns></returns>
        public virtual DateTimeOffset CreationTime { get; set; }

        /// <summary>
        /// Gets or sets the time that this account was last updated.
        /// </summary>
        /// <returns></returns>
        public virtual DateTimeOffset? TimeLastUpdated { get; set; }

        /// <summary>
        /// Gets or sets the time that this account was deleted on, if it was deleted.
        /// </summary>
        /// <returns></returns>
        public virtual DateTimeOffset? DeletionTime { get; set; }

        /// <summary>
        /// Gets or sets the display name of this account.
        /// </summary>
        /// <returns></returns>
        public virtual string DisplayName { get; set; }

        /// <summary>
        /// Gets whether this account has been deleted.
        /// </summary>
        /// <returns></returns>
        public bool IsDeleted
        {
            get
            {
                return DeletionTime.HasValue;
            }
        }

        /// <summary>
        /// Gets or sets the collection of logins that this account has.
        /// </summary>
        /// <returns></returns>
        public virtual ICollection<Login> Logins { get; set; }

        /// <summary>
        /// Gets or sets the collection of claims that this account has.
        /// </summary>
        /// <returns></returns>
        public virtual ICollection<Claim> Claims { get; set; }

        /// <summary>
        /// Gets or sets the tenant that this account belongs to.
        /// </summary>
        /// <returns></returns>
        [Required]
        public virtual string Tenant
        {
            get;
            set;
        }

        /// <summary>
        /// Gets whether the user has been locked out of the account due to too many incorrect login attempts.
        /// </summary>
        /// <returns></returns>
        public virtual bool IsLockedOut
        {
            get
            {
                return DateTimeOffset.Now < LockoutEndTime;
            }
        }

        /// <summary>
        /// Gets or sets the time that the lockout ends at.
        /// </summary>
        /// <returns></returns>
        public DateTimeOffset? LockoutEndTime { get; set; }
    }
}
