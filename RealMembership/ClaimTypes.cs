using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealMembership
{
    /// <summary>
    /// Defines a static partial class that contains a list of constants that represent possible claim types.
    /// </summary>
    public static partial class ClaimTypes
    {
        /// <summary>
        /// The constant claim type for roles.
        /// </summary>
        public const string Role = "Role";

        /// <summary>
        /// The constant claim type for the first name of an entity.
        /// </summary>
        public const string FirstName = "FirstName";

        /// <summary>
        /// The constant claim type for the last name of an entity.
        /// </summary>
        public const string LastName = "LastName";
    }
}
