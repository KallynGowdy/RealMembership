using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealMembership.Logins
{
    /// <summary>
    /// Defines an interface that provides a login using two factor authentication.
    /// </summary>
    public interface ITwoFactorLogin<TAccount, TDateTime> : ILogin<TAccount, TDateTime>
        where TAccount : IUserAccount<TAccount, TDateTime>
        where TDateTime : struct
    {
        /// <summary>
        /// Gets or sets the primary login that must be used in the two factor login process.
        /// </summary>
        /// <returns></returns>
        ILogin<TAccount, TDateTime> PrimaryLogin
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the list of secondary logins that can be used for alternative authentication.
        /// The first login is the default. (hence the reason why this is a list instead of a collection, because order is important)
        /// </summary>
        /// <returns></returns>
        IList<ILogin<TAccount, TDateTime>> SecondaryLogins
        {
            get;
            set;
        }


    }
}
