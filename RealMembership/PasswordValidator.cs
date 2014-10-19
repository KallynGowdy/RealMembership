using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealMembership
{
    /// <summary>
    /// Defines an abstract base class for an object that validates passwords.
    /// </summary>
    public abstract class PasswordValidator
    {
        /// <summary>
        /// Validates the given password against the internally stored rules.
        /// </summary>
        /// <param name="password">The password that should be validated.</param>
        /// <returns>Returns a new result defining the result of the password validation.</returns>
        public abstract SetPasswordResult ValidatePassword(string password);
    }
}
