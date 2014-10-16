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
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealMembership
{
    /// <summary>
    /// Defines a class that represents a basic claim.
    /// </summary>
    public class Claim : IClaim
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Claim"/> class.
        /// </summary>
        public Claim() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Claim"/> class.
        /// </summary>
        /// <param name="type">The type possesed by the claim.</param>
        /// <param name="value">The value possesed by the claim.</param>
        public Claim(string type, string value)
        {
            this.Type = type;
            this.Value = value;
        }
        
        /// <summary>
        /// Gets or sets the type of the claim. What sort of value that the claim represents.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string Type
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the value that the claim posseses.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string Value
        {
            get;
            set;
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            return Equals(obj as IClaim);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return new { Value, Type }.GetHashCode() * 23;
        }

        /// <summary>
        /// Determines whether the specified <see cref="IClaim" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="IClaim" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="IClaim" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public bool Equals(IClaim other)
        {
            return other != null &&
                this.Type.Equals(other.Type, StringComparison.OrdinalIgnoreCase) &&
                this.Value.Equals(other.Value, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("Type: {0}, Value: {1}", Type, Value);
        }
    }
}
