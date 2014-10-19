using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealMembership.Logins.SecurityEvents;

namespace RealMembership.Implementation.EF.Logins
{
    /// <summary>
    /// Defines an abstract class that provides an implementation of <see cref="IPasswordResetAttempt{TAccount, DateTimeOffset}"/>.
    /// </summary>
    public abstract class PasswordResetAttempt<TAccount> : RealMembership.Logins.SecurityEvents.PasswordResetAttempt<TAccount, DateTimeOffset>
        where TAccount : IUserAccount<TAccount, DateTimeOffset>
    {
    }
}
