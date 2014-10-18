using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealMembership.Logins;

namespace RealMembership.EF
{
    /// <summary>
    /// Defines a an abstract base class for <see cref="IUserAccount{TAccount, TDateTime}"/>.
    /// </summary>
    public abstract class UserAccount<TAccount, TDateTime> : RealMembership.UserAccount<TAccount, TDateTime>
        where TAccount : UserAccount<TAccount, TDateTime>
        where TDateTime : struct
    {
        
    }
}
