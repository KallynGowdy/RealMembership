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
        /// Gets or sets the Tenant that this account belongs to.
        /// </summary>
        /// <returns></returns>
        string Tenant
        {
            get;
            set;
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
