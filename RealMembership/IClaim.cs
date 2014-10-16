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
    /// Defines an interface for objects that define a claim. That is, a value that represents a property that another object might posses.
    /// For example, a user might have an 'Admin' claim, where the type of the claim is 'Role' and the value of the claim is 'Admin'.
    /// </summary>
    public interface IClaim : IEquatable<IClaim>
    {
        /// <summary>
        /// Gets or sets the type of the claim. What sort of value that the claim represents.
        /// </summary>
        /// <returns></returns>
        string Type
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the value that the claim posseses.
        /// </summary>
        /// <returns></returns>
        string Value
        {
            get;
            set;
        }
    }
}
