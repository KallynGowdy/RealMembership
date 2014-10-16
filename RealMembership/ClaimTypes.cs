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
