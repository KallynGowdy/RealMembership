using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealMembership.Logins
{
    /// <summary>
    /// Defines an interface for an object that defines a login where the user uses their email address for identification and a password for authentication.
    /// </summary>
    public interface IEmailPasswordLogin<TAccount, TDateTime> : IEmailLogin<TAccount, TDateTime>, IPasswordLogin<TAccount, TDateTime>
        where TAccount : IUserAccount<TAccount, TDateTime>
        where TDateTime : struct
    {
    }
}
