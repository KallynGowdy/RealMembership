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
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace RealMembership.Logins
{
    /// <summary>
    /// Defines a static class that provides several helpers used in cryptography.
    /// </summary>
    public static class CryptoHelpers
    {
        /// <summary>
        /// Gets the default number of iterations based on the current date.
        /// </summary>
        /// <returns></returns>
        public static int DefaultIterations
        {
            get
            {
                return Convert.ToInt32(DateTime.Now.Ticks / 10000000);
            }
        }

        /// <summary>
        /// Gets the default number of bytes that should be used in the hash and salt.
        /// </summary>
        /// <returns></returns>
        public static int DefaultHashSize
        {
            get
            {
                return DateTime.Now.Year - 1994;
            }
        }

        /// <summary>
        /// Creates and returns an array of bytes that were securely generated using <see cref="RNGCryptoServiceProvider"/>.
        /// </summary>
        /// <param name="count">The number of bytes that should be returned.</param>
        /// <returns></returns>
        public static byte[] GetSecureRandomBytes(int count)
        {
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] b = new byte[count];
                rng.GetBytes(b);
                return b;
            }
        }
    }

    /// <summary>
    /// Defines an abstract class that represents a <see cref="IPasswordLogin"/>.
    /// </summary>
    public abstract class PasswordLogin<TAccount, TDateTime> : Login<TAccount, TDateTime>, IPasswordLogin<TAccount, TDateTime>
        where TAccount : IUserAccount<TAccount, TDateTime>
        where TDateTime : struct
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="PasswordLogin{TAccount, TDateTime}"/> class.
        /// </summary>
        /// <param name="password">The password that should be stored.</param>
        protected PasswordLogin(string password) : this()
        {
            PasswordHash = CalculateHash(password).Result;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PasswordLogin"/> class.
        /// </summary>
        protected PasswordLogin() : this(CryptoHelpers.DefaultIterations, CryptoHelpers.GetSecureRandomBytes(CryptoHelpers.DefaultHashSize))
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PasswordLogin"/> class.
        /// </summary>
        /// <param name="hashIterations">The hash iterations.</param>
        /// <param name="salt">The salt.</param>
        /// <exception cref="ArgumentOutOfRangeException">hashIterations</exception>
        /// <exception cref="ArgumentNullException">salt</exception>
        protected PasswordLogin(int hashIterations, byte[] salt)
        {
            if (hashIterations < 1) throw new ArgumentOutOfRangeException("hashIterations");
            if (salt == null) throw new ArgumentNullException("salt");
            this.Iterations = hashIterations;
            this.Salt = Convert.ToBase64String(salt);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PasswordLogin{TAccount, TDateTime}"/> class.
        /// </summary>
        /// <param name="hashIterations">The hash iterations.</param>
        /// <param name="salt">The salt.</param>
        /// <param name="password">The password.</param>
        protected PasswordLogin(int hashIterations, byte[] salt, string password) : this(hashIterations, salt)
        {
            PasswordHash = CalculateHash(password).Result;
        }

        /// <summary>
        /// Gets or sets the password hash stored in this object.
        /// </summary>
        /// <returns></returns>
        [Required]
        public string PasswordHash
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the number of iterations used to hash the password.
        /// </summary>
        /// <returns></returns>
        public int Iterations
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the salt used to hash the password.
        /// </summary>
        /// <returns></returns>
        [Required]
        public string Salt
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets whether the login is currently in the password reset process.
        /// </summary>
        /// <returns></returns>
        [NotMapped]
        public abstract bool IsInResetProcess
        {
            get;
        }

        /// <summary>
        /// Gets or sets the time that the password reset was requested.
        /// </summary>
        /// <returns></returns>
        public virtual TDateTime? ResetRequestTime
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the time that the password reset expires.
        /// </summary>
        /// <returns></returns>
        [NotMapped]
        public abstract TDateTime? ResetExpireTime
        {
            get;
        }

        /// <summary>
        /// Gets or sets the password reset code that is currently being used.
        /// </summary>
        /// <returns></returns>
        public virtual string ResetCode
        {
            get;
            set;
        }

        /// <summary>
        /// Calculates the hash of the given password using the stored salt and iterations and returns the base64 encoded result.
        /// </summary>
        /// <param name="password">The password that should be hashed.</param>
        /// <returns></returns>
        protected virtual Task<string> CalculateHash(string password)
        {
            byte[] salt = Convert.FromBase64String(Salt);
            using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations))
            {
                return Task.FromResult(Convert.ToBase64String(pbkdf2.GetBytes(salt.Length)));
            }
        }

        /// <summary>
        /// Determines if the given password matches the password stored in this login.
        /// </summary>
        /// <param name="password">The password to validate against the store.</param>
        /// <returns>
        ///   <c>true</c> if the password is valid for this login, otherwise false.
        /// </returns>
        public async Task<bool> MatchesPasswordAsync(string password)
        {
            return (await CalculateHash(password)).Equals(PasswordHash, StringComparison.Ordinal);
        }

        /// <summary>
        /// Determines if the given reset code matches the reset code stored in this login.
        /// Should return false if the reset has expired or was not requested.
        /// </summary>
        /// <param name="code">The code to validated against the stored code.</param>
        /// <returns><c>true</c> if the code is valid for this login, otherwise <c>false</c></returns>
        public virtual Task<bool> MatchesResetCodeAsync(string code)
        {
            return Task.FromResult(IsInResetProcess && ResetCode != null && ResetCode.Equals(code, StringComparison.Ordinal));
        }

        /// <summary>
        /// Requests a new password reset code for this login.
        /// </summary>
        /// <returns>Returns a new string representing the password reset code or null if a reset is not allowed.</returns>
        public abstract Task<PasswordResetRequestResult> RequestResetCodeAsync();

        /// <summary>
        /// Sets the password stored in this object to the given value and returns a result determining whether the operation was sucessful.
        /// </summary>
        /// <param name="newPassword">The new password that should be stored in the login.</param>
        /// <returns>
        /// Returns a new <see cref="SetPasswordResult" /> that represents whether the operation was successful.
        /// </returns>
        public abstract Task<SetPasswordResult> SetPasswordAsync(string newPassword);

        /// <summary>
        /// Sets the password stored in this object to the given value and returns a result determining whether the operation was sucessful.
        /// Should be used internally by <see cref="SetPassword(string)"/> to catch the common problems with setting the password.
        /// </summary>
        /// <param name="newPassword">The new password that should be stored in the login.</param>
        /// <returns>
        /// Returns a new <see cref="SetPasswordResult" /> that represents whether any problems were found with the operation.
        /// Returns null if no problems were found.
        /// </returns>
        protected virtual Task<SetPasswordResult> SetPasswordCoreAsync(string newPassword)
        {
            SetPasswordResult result;
            if (string.IsNullOrEmpty(newPassword))
            {
                result = new SetPasswordResult
                {
                    Successful = false,
                    Result = SetPasswordResultType.NullOrEmptyPassword
                };
            }
            else if (!this.IsCurrentlyActive)
            {
                result = new SetPasswordResult
                {
                    Successful = false,
                    Result = SetPasswordResultType.LoginNotActive,
                    Message = "The login is not active."
                };
            }
            else if (!this.IsVerified)
            {
                result = new SetPasswordResult
                {
                    Successful = false,
                    Result = SetPasswordResultType.LoginNotVerified
                };
            }
            else if (this.IsLockedOut)
            {
                result = new SetPasswordResult
                {
                    Successful = false,
                    Result = SetPasswordResultType.AccountLockedOut
                };
            }
            else
            {
                result = null;
            }
            return Task.FromResult(result);
        }
    }
}