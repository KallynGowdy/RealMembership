using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealMembership.Logins;
using RealMembership.Logins.SecurityEvents;

namespace RealMembership.Implementation.Default.Logins
{
    /// <summary>
    /// Defines an abstract class that provides a basic implementation of <see cref="IVerificationRequestAttempt{TAccount, DateTimeOffset}"/>.
    /// </summary>
    public abstract class VerificationRequestAttempt<TAccount> : RealMembership.Logins.SecurityEvents.VerificationRequestAttempt<TAccount, DateTimeOffset>
        where TAccount : IUserAccount<TAccount, DateTimeOffset>
    {
    }
}
