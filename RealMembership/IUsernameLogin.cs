using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealMembership
{
    /// <summary>
    /// Defines an interface for objects that represent a login for usernames.
    /// </summary>
    public interface IUsernameLogin<TAccount, TDateTime> : IPasswordLogin<TAccount, TDateTime>
        where TAccount : IUserAccount<TAccount, TDateTime>
        where TDateTime : struct
    {
        /// <summary>
        /// Gets or sets the username that should be used for this login.
        /// </summary>
        /// <returns></returns>
        string Username
        {
            get;
            set;
        }
    }
}
