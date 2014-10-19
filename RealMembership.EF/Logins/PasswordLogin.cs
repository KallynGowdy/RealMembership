using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealMembership.Logins;

namespace RealMembership.Implementation.EF.Logins
{
    /// <summary>
    /// Defines an abstract class that provides a basic implementation of <see cref="IPasswordLogin{TAccount, DateTimeOffset}"/>
    /// </summary>
    public class PasswordLogin<TAccount> : PasswordLogin<TAccount, DateTimeOffset>
        where TAccount : IUserAccount<TAccount, DateTimeOffset>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PasswordLogin{TAccount, TDateTime}"/> class.
        /// </summary>
        /// <param name="email">The email used by the login.</param>
        public PasswordLogin(string email) : base(email)
        {
        }

        public PasswordLogin() : base() { }

        /// <summary>
        /// Gets whether the account is currently locked because of incorrect login attempts.
        /// </summary>
        public override bool IsLockedOut
        {
            get
            {
                return DateTimeOffset.Now < LockoutEndTime;
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
        /// Gets whether the password reset process is currently active for this login.
        /// </summary>
        public override bool IsInResetProcess
        {
            get
            {
                return DateTimeOffset.Now < ResetExpireTime;
            }
        }

        /// <summary>
        /// Gets or sets the time that the password reset code
        /// </summary>
        public override DateTimeOffset? ResetExpireTime
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

        public async override Task<SetPasswordResult> SetPasswordAsync(string newPassword)
        {
            return await SetPasswordCoreAsync(newPassword);
        }

        protected override DateTimeOffset GetCurrentInstant()
        {
            return DateTimeOffset.Now;
        }
    }
}
