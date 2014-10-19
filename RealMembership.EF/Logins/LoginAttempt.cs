using RealMembership.Logins.SecurityEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealMembership.Implementation.EF.Logins
{
    /// <summary>
    /// Defines an abstract class that provides an implementation of <see cref="ILoginAttempt{TAccount, DateTimeOffset}"/>.
    /// </summary>
    public abstract class LoginAttempt<TAccount> : LoginAttempt<TAccount, DateTimeOffset>
        where TAccount : IUserAccount<TAccount, DateTimeOffset>
    {
    }
}
