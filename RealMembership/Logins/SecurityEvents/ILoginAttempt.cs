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

namespace RealMembership.Logins.SecurityEvents
{
    /// <summary>
    /// Defines an interface for objects that record information about attempted logins.
    /// </summary>
    /// <typeparam name="TDateTime">The type of the structue used to contain date information.</typeparam>
    public interface ILoginAttempt : ILoginSecurityEvent
    {
        /// <summary>
        /// Gets or sets the ID of the login attempt.
        /// </summary>
        /// <returns></returns>
        long Id
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the result that occured from the login attempt.
        /// </summary>
        /// <returns></returns>
        AuthenticationResult Result
        {
            get;
            set;
        }
    }
}