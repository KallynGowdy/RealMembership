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
using System.Text;
using System.Threading.Tasks;

namespace RealMembership.Logins
{
    /// <summary>
    /// Defines a static class that provides several helpers used in cryptography.
    /// </summary>
    public static class CryptoHelpers
    {
        /// <summary>
        /// Gets the date for January 1st, 2014.
        /// </summary>
        public static readonly DateTime _2014 = new DateTime(2014, 1, 1, 0, 0, 1, DateTimeKind.Utc);

        /// <summary>
        /// Gets the number of OWASP recommended iterations for 2014.
        /// </summary>
        /// <value>128,000</value>
        public const int IterationsFor2014 = 128000;

        /// <summary>
        /// Gets the default number of iterations based on the current date.
        /// </summary>
        /// <returns></returns>
        public static int DefaultIterations
        {
            get
            {
                return GetIterationsForYear(
                    ((((((DateTime.UtcNow.Ticks
                        / 10000D) // 10,000 ticks in a milisecond
                        / 1000D) // 1000 miliseconds in a second
                        / 60D) // 60 seconds in a minute
                        / 60D) // 60 minute in an hour
                        / 24d) // 24 hours in a day
                        / 365.25d) // 365 days in a year
                        + 1); // +1 year because Ticks starts from year 0001 instead of year 0000
            }
        }


        public static int GetIterationsForYear(double year)
        {
            double _2014 = 2014;
            if (year > _2014)
            {
                double totalYearsSince2014 = year - _2014;
                double doublePerExtra2Years = Math.Pow(2, totalYearsSince2014 / 2);
                int currentIterations = (int)(IterationsFor2014 * doublePerExtra2Years); // the number of iterations should double every two years,
                                                                                         // so take the total number of years since 2014 til now
                                                                                         // and multiply to get the current number of iterations that should be used.
                                                                                         // If the value is less than the number of iterations for 2014
                                                                                         // then the conversion wrapped
                return currentIterations < IterationsFor2014 ? int.MaxValue : currentIterations;
            }
            return IterationsFor2014;
        }

        /// <summary>
        /// Gets the default number of bytes that should be used in the hash and salt.
        /// </summary>
        /// <returns></returns>
        public static int DefaultHashSize
        {
            get
            {
                return 20;
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

        /// <summary>
        /// Calculates the PBKDF2 hash of the given secret using the given salt with the given number of iterations.
        /// </summary>
        /// <param name="secret">The secret that should be hashed.</param>
        /// <param name="salt">The salt that should be used to protect the salt from cost sharing.</param>
        /// <param name="iterations">The number of iterations that should be used. (minimum of 1)</param>
        /// <param name="outputSize">The number of bytes that should be returned in the output.</param>
        /// <returns></returns>
        public static byte[] CalculateHash(byte[] secret, byte[] salt, int iterations, int? outputSize = null)
        {
            outputSize = outputSize ?? DefaultHashSize;
            using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(secret, salt, iterations))
            {
                return pbkdf2.GetBytes(outputSize.Value);
            }
        }
    }

    /// <summary>
    /// Defines an abstract class that represents a <see cref="IPasswordLogin"/>.
    /// </summary>
    public class PasswordLogin : EmailLogin, IPasswordLogin
    {
        /// <summary>
        /// Gets the HMAC message that should be used when computing the HMAC for the reset codes.
        /// Note that this value does not need to be unique or secret. This is because it serves the same
        /// purpose as a salt would for password storage. It allows us to use an HMAC algorithm which
        /// prevents from a length extension attack.
        /// (A length extension attack does not retrieve the original message/code, but allows an arbitrary code to be forged in place of the real one)
        /// The reason why salts should be required to be unique is that the passwords that they are protecting are usually not unique. Therefore
        /// a salt must be unique in order to eliminate cost sharing through an attacker's use of rainbow tables. Because the roles are essentially reversed
        /// and the generated reset code is unique and unpredictable, then the salt is just here to prevent the use of a length extension attack.
        /// (because no rainbow table could ever be created for codes with 20 bytes of entropy)
        /// </summary>
        /// <returns></returns>
        public static string ResetCodeSalt
        {
            get
            {
                return "RealMembershipResetCodeKey";
            }
        }

        /// <summary>
        /// Gets the hash of the given code that can be used to match to the stored code.
        /// </summary>
        /// <param name="code">The code that should be hashed.</param>
        /// <returns></returns>
        public static string GetCodeHash(string code)
        {
            return Convert.ToBase64String(CryptoHelpers.CalculateHash(Convert.FromBase64String(code), Encoding.UTF8.GetBytes(ResetCodeSalt), 1));
        }

        /// <summary>
        /// Gets a new randomly generated code.
        /// </summary>
        /// <returns></returns>
        public static string GetRandomCode()
        {
            return Convert.ToBase64String(CryptoHelpers.GetSecureRandomBytes(CryptoHelpers.DefaultHashSize));
        }

        public static PasswordValidator DefaultPasswordValidator
        {
            get
            {
                return new DefaultPasswordValidator();
            }
        }

        /// <summary>
        /// Gets or sets the span of time that the password reset is valid for.
        /// </summary>
        /// <returns></returns>
        public TimeSpan ResetLifetime
        {
            get;
            set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PasswordLogin"/> class.
        /// </summary>
        /// <param name="passwordValidator">The password validator.</param>
        /// <exception cref="ArgumentNullException">passwordValidator</exception>
        protected PasswordLogin(PasswordValidator passwordValidator) : this(passwordValidator, CryptoHelpers.DefaultIterations, CryptoHelpers.GetSecureRandomBytes(CryptoHelpers.DefaultHashSize))
        {
            if (passwordValidator == null) throw new ArgumentNullException("passwordValidator");
            this.passwordValidator = passwordValidator;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PasswordLogin"/> class.
        /// </summary>
        protected PasswordLogin() : this(DefaultPasswordValidator) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PasswordLogin" /> class.
        /// </summary>
        /// <param name="hashIterations">The hash iterations.</param>
        /// <param name="salt">The salt.</param>
        protected PasswordLogin(PasswordValidator passwordValidator, int hashIterations, byte[] salt)
        {
            if (hashIterations < 1) throw new ArgumentOutOfRangeException("hashIterations");
            if (salt == null) throw new ArgumentNullException("salt");
            if (passwordValidator == null) throw new ArgumentNullException("passwordValidator");
            this.Iterations = hashIterations;
            this.Salt = Convert.ToBase64String(salt);
            this.passwordValidator = passwordValidator;
        }

        private PasswordValidator passwordValidator;

        /// <summary>
        /// Gets or sets the <see cref="PasswordValidator"/> used to validate passwords.
        /// </summary>
        /// <returns></returns>
        [NotMapped]
        public virtual PasswordValidator PasswordValidator
        {
            get
            {
                return passwordValidator;
            }
            set
            {
                if (value == null) throw new ArgumentNullException("value");
                passwordValidator = value;
            }
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
        /// Gets or sets the time that the password reset was requested.
        /// </summary>
        /// <returns></returns>
        public virtual DateTimeOffset? ResetRequestTime
        {
            get;
            set;
        }

        /// <summary>
        /// Gets whether the password reset process is currently active for this login.
        /// </summary>
        public bool IsInResetProcess
        {
            get
            {
                return DateTimeOffset.Now < ResetExpireTime;
            }
        }

        /// <summary>
        /// Gets or sets the time that the password reset code
        /// </summary>
        public DateTimeOffset? ResetExpireTime
        {
            get
            {
                if (ResetRequestTime != null)
                {
                    return ResetRequestTime.Value + ResetLifetime;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Gets or sets the password reset code that is currently being used.
        /// </summary>
        /// <returns></returns>
        public virtual string ResetCodeHash
        {
            get;
            set;
        }

        /// <summary>
        /// Calculates the hash of the given password using the stored salt and iterations and returns the base64 encoded result.
        /// </summary>
        /// <param name="password">The password that should be hashed.</param>
        /// <returns></returns>
        protected virtual Task<string> CalculatePasswordHashAsync(string password)
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
            return (await CalculatePasswordHashAsync(password)).Equals(PasswordHash, StringComparison.Ordinal);
        }

        /// <summary>
        /// Requests a new password reset code for this login.
        /// </summary>
        /// <returns>Returns a new string representing the password reset code or null if a reset is not allowed.</returns>
        public virtual Task<PasswordResetRequestResult> RequestResetCodeAsync()
        {
            PasswordResetRequestResult result;
            if (this.IsLockedOut)
            {
                result = new PasswordResetRequestResult
                {
                    Code = null,
                    Successful = false,
                    Result = PasswordResetRequestResultType.AccountLockedOut
                };
            }
            else if (!this.IsCurrentlyActive)
            {
                result = new PasswordResetRequestResult
                {
                    Code = null,
                    Successful = false,
                    Result = PasswordResetRequestResultType.LoginNotActive
                };
            }
            else if (!this.IsVerified)
            {
                result = new PasswordResetRequestResult
                {
                    Code = null,
                    Successful = false,
                    Result = PasswordResetRequestResultType.LoginNotVerified
                };
            }
            else
            {
                result = new PasswordResetRequestResult
                {
                    Code = GetRandomCode(),
                    Result = PasswordResetRequestResultType.ResetCodeIssued,
                    Successful = true
                };
            }
            if (result.Successful)
            {
                ResetRequestTime = DateTimeOffset.Now;
                ResetCodeHash = GetCodeHash(result.Code);
            }
            else
            {
                ResetRequestTime = null;
                ResetCodeHash = null;
            }
            return Task.FromResult(result);
        }

        /// <summary>
        /// Sets the password stored in this object to the given value and returns a result determining whether the operation was sucessful.
        /// </summary>
        /// <param name="newPassword">The new password that should be stored in the login.</param>
        /// <returns>
        /// Returns a new <see cref="SetPasswordResult" /> that represents whether the operation was successful.
        /// </returns>
        public virtual async Task<SetPasswordResult> SetPasswordAsync(string newPassword)
        {
            SetPasswordResult result = await SetPasswordCoreAsync(newPassword);

            if (result.Successful)
            {
                PasswordHash = await CalculatePasswordHashAsync(newPassword);
            }

            return result;
        }

        /// <summary>
        /// Sets the password stored in this object to the given value and returns a result determining whether the operation was sucessful.
        /// Should be used internally by <see cref="SetPasswordAsync(string)"/> to catch the common problems with setting the password.
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
            else if (!this.IsVerified && RequiresVerification && PasswordHash != null)
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
                result = PasswordValidator.ValidatePassword(newPassword);
            }
            return Task.FromResult(result);
        }
    }
}