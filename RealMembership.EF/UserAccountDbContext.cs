using RealMembership.Logins;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealMembership.EF
{
    /// <summary>
    /// Defines a class that represents a <see cref="DbContext"/> for user accounts and logins.
    /// </summary>
    public abstract class UserAccountDbContext<TAccount, TDateTime> : DbContext
        where TAccount : class, IUserAccount<TAccount, TDateTime>
        where TDateTime : struct
    {
        /// <summary>
        /// Gets or sets the set of accounts contained in the database.
        /// </summary>
        /// <returns></returns>
        public virtual DbSet<TAccount> Accounts
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the set of logins contained in the database.
        /// </summary>
        /// <returns></returns>
        public virtual DbSet<Login<TAccount,TDateTime>> Logins
        {
            get;
            set;
        }
    }
}
