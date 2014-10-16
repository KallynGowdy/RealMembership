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
    /// Defines an abstract base class that implements <see cref="IUserAccount{TDateTime}" />.
    /// </summary>
    /// <typeparam name="TDate">The type of the date.</typeparam>
    public abstract class UserAccount<TAccount, TDate> : IUserAccount<TAccount, TDate> 
        where TAccount : UserAccount<TAccount, TDate>
        where TDate : struct
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserAccount{TDate}"/> class.
        /// </summary>
        protected UserAccount()
        {
            this.Logins = new List<ILogin<TAccount, TDate>>();
            this.Claims = new List<IClaim>();
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
                return Logins.Any(l => l is ITwoFactorLogin<TAccount, TDate>);
            }
        }

        /// <summary>
        /// Gets or sets the time that this account was created.
        /// </summary>
        /// <returns></returns>
        public virtual TDate CreationTime { get; set; }

        /// <summary>
        /// Gets or sets the time that this account was last updated.
        /// </summary>
        /// <returns></returns>
        public virtual TDate? TimeLastUpdated { get; set; }

        /// <summary>
        /// Gets or sets the time that this account was deleted on, if it was deleted.
        /// </summary>
        /// <returns></returns>
        public virtual TDate? DeletionTime { get; set; }

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
        public virtual ICollection<ILogin<TAccount, TDate>> Logins { get; set; }

        /// <summary>
        /// Gets or sets the collection of claims that this account has.
        /// </summary>
        /// <returns></returns>
        public virtual ICollection<IClaim> Claims { get; set; }

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
        public abstract bool IsLockedOut { get; }

        /// <summary>
        /// Gets or sets the time that the lockout ends at.
        /// </summary>
        /// <returns></returns>
        public abstract TDate? LockoutEndTime { get; set; }
    }
}
