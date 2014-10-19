using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealMembership.Logins;

namespace RealMembership
{
    /// <summary>
    /// Defines a class that provides a default implementation of <see cref="IMessageFormatter"/>.
    /// </summary>
    public class DefaultMessageFormatter : IMessageFormatter
    {
        public Task<string> FormatPasswordChangedMessageAsync(ILogin login)
        {
            return Task.FromResult("Your password was recently changed.");
        }

        public Task<string> FormatPasswordChangedSubjectAsync(ILogin login)
        {
            return Task.FromResult("Password Changed");
        }

        public Task<string> FormatPasswordResetMessageAsync(string resetCode, IPasswordLogin login)
        {
            return Task.FromResult(string.Format("Reset your password with this code: {0}", resetCode));
        }

        public Task<string> FormatPasswordResetSubjectAsync(IPasswordLogin login)
        {
            return Task.FromResult("Reset your password");
        }

        public Task<string> FormatVerifiedMessageAsync(ILogin login)
        {
            return Task.FromResult("Your account has just been verified!");
        }

        public Task<string> FormatVerifiedSubjectAsync(ILogin login)
        {
            return Task.FromResult("Account Verified");
        }

        public Task<string> FormatVerifyLoginMessageAsync(string verificationCode, ILogin login)
        {
            return Task.FromResult(string.Format("Verify your account with this code: {0}", verificationCode));
        }

        public Task<string> FormatVerifyLoginSubjectAsync(ILogin login)
        {
            return Task.FromResult("Verify your account");
        }
    }
}
