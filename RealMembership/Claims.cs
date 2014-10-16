//   Copyright 2014 Kallyn Gowdy
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.

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
