using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealMembership.Logins;

namespace RealMembership.Implementation.EF
{
    /// <summary>
    /// Defines a an abstract base class for <see cref="IUserAccount{TAccount, TDateTime}"/>.
    /// </summary>
    public class UserAccount<TAccount> : RealMembership.UserAccount<TAccount, DateTimeOffset>
        where TAccount : UserAccount<TAccount, DateTimeOffset>
    {
        /// <summary>
        /// Gets a value indicating whether this instance is locked out.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is locked out; otherwise, <c>false</c>.
        /// </value>
        public override bool IsLockedOut
        {
            get
            {
                return DateTimeOffset.Now < LockoutEndTime;
            }
        }

        public override DateTimeOffset? LockoutEndTime
        {
            get;
            set;
        }
    }
}
