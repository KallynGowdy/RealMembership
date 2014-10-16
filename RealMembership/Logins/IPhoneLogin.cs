using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealMembership.Logins
{
    /// <summary>
    /// Defines an interface for a login where user recieves/retrieves a code from their phone.
    /// </summary>
    public interface IPhoneLogin<TAccount, TDateTime> : ILogin<TAccount, TDateTime>
        where TAccount : IUserAccount<TAccount, TDateTime>
        where TDateTime : struct
    {
        /// <summary>
        /// Gets or sets the phone number used for this login.
        /// </summary>
        /// <returns></returns>
        string PhoneNumber
        {
            get;
            set;
        }


    }
}
