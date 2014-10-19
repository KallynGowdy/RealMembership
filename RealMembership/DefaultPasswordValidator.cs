using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealMembership
{
    /// <summary>
    /// Defines a class that provides a default password validator.
    /// </summary>
    public class DefaultPasswordValidator : PasswordValidator
    {
        public const int DefaultMinLength = 8;

        public const int DefaultMinDigits = 1;

        public const int DefaultMinLowercase = 1;

        public const int DefaultMinUppercase = 1;

        public const int DefaultMinSymbols = 1;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultPasswordValidator"/> class.
        /// </summary>
        public DefaultPasswordValidator() : this(DefaultMinLength, DefaultMinLowercase, DefaultMinUppercase, DefaultMinDigits, DefaultMinSymbols) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultPasswordValidator"/> class.
        /// </summary>
        /// <param name="minLength">The minimum length.</param>
        /// <param name="minLowercase">The minimum lowercase.</param>
        /// <param name="minUppercase">The minimum uppercase.</param>
        /// <param name="minDigits">The minimum digits.</param>
        /// <param name="minSymbols">The minimum symbols.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// Must be greater than or equal to 0.
        /// or
        /// Must be greater than or equal to 0.
        /// or
        /// Must be greater than or equal to 0.
        /// or
        /// Must be greater than or equal to 0.
        /// </exception>
        public DefaultPasswordValidator(int minLength, int minLowercase, int minUppercase, int minDigits, int minSymbols)
        {
            if (minLength < 0) throw new ArgumentOutOfRangeException("Must be greater than or equal to 0.");
            if (minLowercase < 0) throw new ArgumentOutOfRangeException("Must be greater than or equal to 0.");
            if (minUppercase < 0) throw new ArgumentOutOfRangeException("Must be greater than or equal to 0.");
            if (minSymbols < 0) throw new ArgumentOutOfRangeException("Must be greater than or equal to 0.");
            this.MinimumLength = minLength;
            this.MinimumRequiredLowercase = minLowercase;
            this.MinimumRequiredUppercase = minUppercase;
            this.MinimumRequiredDigits = minDigits;
            this.MinimumRequiredSymbols = minSymbols;
        }

        /// <summary>
        /// Gets or sets the minimum number of required digits.
        /// </summary>
        /// <returns></returns>
        public int MinimumRequiredDigits
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the minimum number of required lowercase characters.
        /// </summary>
        /// <returns></returns>
        public int MinimumRequiredLowercase
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the minimum number of required uppercase characters
        /// </summary>
        /// <returns></returns>
        public int MinimumRequiredUppercase
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the minimum length of passwords that are allowed.
        /// </summary>
        /// <returns></returns>
        public int MinimumLength
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the minimum number of symbols that are required.
        /// </summary>
        /// <returns></returns>
        public int MinimumRequiredSymbols
        {
            get;
            set;
        }

        /// <summary>
        /// Validates the given password against the internally stored rules.
        /// </summary>
        /// <param name="password">The password that should be validated.</param>
        /// <returns>
        /// Returns a new result defining the result of the password validation.
        /// </returns>
        public override SetPasswordResult ValidatePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                return new SetPasswordResult
                {
                    Successful = false,
                    Result = SetPasswordResultType.NullOrEmptyPassword
                };
            }
            else if (password.Length < MinimumLength)
            {
                return new SetPasswordResult
                {
                    Successful = false,
                    Result = SetPasswordResultType.TooShort
                };
            }
            else if (password.Count(c => char.IsUpper(c)) < MinimumRequiredUppercase)
            {
                return new SetPasswordResult
                {
                    Successful = false,
                    Result = SetPasswordResultType.NotEnoughUpperCase
                };
            }
            else if (password.Count(c => char.IsLower(c)) < MinimumRequiredLowercase)
            {
                return new SetPasswordResult
                {
                    Successful = false,
                    Result = SetPasswordResultType.NotEnoughLowerCase
                };
            }
            else if (password.Count(c => char.IsDigit(c)) < MinimumRequiredDigits)
            {
                return new SetPasswordResult
                {
                    Successful = false,
                    Result = SetPasswordResultType.NotEnoughDigits
                };
            }
            else if (password.Count(c => !(char.IsDigit(c) || char.IsUpper(c) || char.IsLower(c))) < MinimumRequiredSymbols)
            {
                return new SetPasswordResult
                {
                    Successful = false,
                    Result = SetPasswordResultType.NotEnoughSymbols
                };
            }
            else
            {
                return new SetPasswordResult
                {
                    Successful = true,
                    Result = SetPasswordResultType.PasswordSetToNew
                };
            }
        }
    }
}
