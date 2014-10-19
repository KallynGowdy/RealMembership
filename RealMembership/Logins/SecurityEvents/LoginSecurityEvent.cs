using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealMembership.Logins.SecurityEvents
{
    /// <summary>
    /// Defines am abstract class that represents a <see cref="ILoginSecurityEvent"/>.
    /// </summary>
    public abstract class LoginSecurityEvent : ILoginSecurityEvent
    {
        /// <summary>
        /// Gets the type of event that is being recorded.
        /// </summary>
        public abstract SecurityEventType EventType
        {
            get;
        }

        /// <summary>
        /// Gets or sets the ID of this login event.
        /// </summary>
        public long Id
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the type of identification used ('Username', 'Email', ect.) for the attempt.
        /// </summary>
        public IdentificationType? IdentificationType
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the login that the event was against. Null if the event could not be matched to a login. (i.e. non-existant account)
        /// </summary>
        public Login Login
        {
            get;
            set;
        }

        ILogin ILoginSecurityEvent.Login
        {
            get
            {
                return Login;
            }
        }

        /// <summary>
        /// Gets or sets the identification (the specific username, email, phone number, ect.) that was used for the attempt.
        /// </summary>
        public string LoginIdentification
        {
            get;
            set;
        }

        /// <summary>
        /// Gets whether the event was performed successfully.
        /// </summary>
        public abstract bool Successful
        {
            get;
        }

        /// <summary>
        /// Gets or sets the tenant that the login was for.
        /// </summary>
        public string Tenant
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the time that the event occured at.
        /// </summary>
        public DateTimeOffset TimeOfEvent
        {
            get;
            set;
        }
    }
}
