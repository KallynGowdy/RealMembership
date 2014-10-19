using RealMembership.Logins;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealMembership.Logins.SecurityEvents;

namespace RealMembership.Implementation.EF
{
    /// <summary>
    /// Defines a class that represents a <see cref="DbContext"/> for user accounts and logins.
    /// </summary>
    public abstract class UserAccountDbContext<TAccount> : DbContext
        where TAccount : UserAccount<TAccount>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserAccountDbContext{TAccount}"/> class.
        /// </summary>
        /// <param name="name">The name of the connection string that should be used to connect to the database.</param>
        protected UserAccountDbContext(string name) : base(name) { }

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
        public virtual DbSet<Login<TAccount, DateTimeOffset>> Logins
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the set of login security events contained in the database.
        /// </summary>
        /// <returns></returns>
        public virtual DbSet<LoginSecurityEvent<TAccount, DateTimeOffset>> SecurityEvents
        {
            get;
            set;
        }
    }
}
