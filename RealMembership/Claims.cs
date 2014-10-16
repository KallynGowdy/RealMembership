using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealMembership
{
    /// <summary>
    /// Defines a static partial class that contains a list of well known claims.
    /// </summary>
    public static partial class Claims
    {
        /// <summary>
        /// Gets a new <see cref="Claim"/> that represents a claim of "Admin" as a "Role".
        /// </summary>
        /// <returns></returns>
        public static Claim AdminClaim
        {
            get
            {
                return new Claim(ClaimTypes.Role, "Admin");
            }
        }

        /// <summary>
        /// Gets a new <see cref="Claim"/> that represents a claim of "User" as a "Role".
        /// </summary>
        /// <returns></returns>
        public static Claim NormalUserClaim
        {
            get
            {
                return new Claim(ClaimTypes.Role, "User");
            }
        }

        /// <summary>
        /// Gets a new <see cref="Claim"/> that represents a claim of the given first name as the "FirstName".
        /// </summary>
        /// <param name="firstName">The name that is being claimed as the first name.</param>
        /// <returns></returns>
        public static Claim FirstNameClaim(string firstName)
        {
            return new Claim(ClaimTypes.FirstName, firstName);
        }

        /// <summary>
        /// Gets a new <see cref="Claim"/> that represents a claim of the given first name as the "FirstName".
        /// </summary>
        /// <param name="lastName">The name that is being claimed as the last name.</param>
        /// <returns></returns>
        public static Claim LastNameClaim(string lastName)
        {
            return new Claim(ClaimTypes.LastName, lastName);
        }
    }
}
