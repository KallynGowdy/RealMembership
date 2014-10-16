using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;

namespace RealMembership
{
    /// <summary>
    /// Defines an abstract class that represents a <see cref="IPasswordLogin"/>.
    /// </summary>
    public abstract class PasswordLogin<TAccount, TDateTime> : Login<TAccount, TDateTime>, IPasswordLogin<TAccount, TDateTime>
        where TAccount : IUserAccount<TAccount, TDateTime>
        where TDateTime : struct
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
            using(RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] b = new byte[count];
                rng.GetBytes(b);
                return b;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PasswordLogin"/> class.
        /// </summary>
        protected PasswordLogin() : this(DefaultIterations, GetSecureRandomBytes(DefaultHashSize))
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
        /// Determines if the given password matches the password stored in this login.
        /// </summary>
        /// <param name="password">The password to validate against the store.</param>
        /// <returns>
        ///   <c>true</c> if the password is valid for this login, otherwise false.
        /// </returns>
        public bool MatchesPassword(string password)
        {
            byte[] salt = Convert.FromBase64String(Salt);
            using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations))
            {
                return pbkdf2.GetBytes(salt.Length).SequenceEqual(Convert.FromBase64String(PasswordHash));
            }
        }

        /// <summary>
        /// Determines if the given reset code matches the reset code stored in this login.
        /// Should return false if the reset has expired or was not requested.
        /// </summary>
        /// <param name="code">The code to validated against the stored code.</param>
        /// <returns><c>true</c> if the code is valid for this login, otherwise <c>false</c></returns>
        public virtual bool MatchesResetCode(string code)
        {
            return IsInResetProcess && ResetCode != null && ResetCode.Equals(code, StringComparison.Ordinal);
        }

        /// <summary>
        /// Requests a new password reset code for this login.
        /// </summary>
        /// <returns>Returns a new string representing the password reset code or null if a reset is not allowed.</returns>
        public abstract string RequestResetCode();
    }
}