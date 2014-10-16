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

namespace RealMembership.Logins
{
    /// <summary>
    /// Defines an interface that provides a login using two factor authentication.
    /// </summary>
    public interface ITwoFactorLogin<TAccount, TDateTime> : ILogin<TAccount, TDateTime>
        where TAccount : IUserAccount<TAccount, TDateTime>
        where TDateTime : struct
    {
        /// <summary>
        /// Gets or sets the primary login that must be used in the two factor login process.
        /// </summary>
        /// <returns></returns>
        ILogin<TAccount, TDateTime> PrimaryLogin
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the list of secondary logins that can be used for alternative authentication.
        /// The first login is the default. (hence the reason why this is a list instead of a collection, because order is important)
        /// </summary>
        /// <returns></returns>
        IList<ILogin<TAccount, TDateTime>> SecondaryLogins
        {
            get;
            set;
        }


    }
}
