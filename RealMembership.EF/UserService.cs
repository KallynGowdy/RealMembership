using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using RealMembership.Implementation.EF.Logins;

namespace RealMembership.Implementation.EF
{
    /// <summary>
    /// Defines a class that provides an implementation of <see cref="IUserService{TAccount, DateTimeOffset}"/> for Entity Framework.
    /// </summary>
    public class UserService<TAccount> : UserService<TAccount, DateTimeOffset>
        where TAccount : UserAccount<TAccount>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserService{TAccount}"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public UserService(ILoginRepository<TAccount, DateTimeOffset> repository) : base(repository)
        {
        }

        public async override Task<EmailAccountCreationResult<TAccount, DateTimeOffset>> CreateAccountAsync(EmailAccountCreationRequest request)
        {
            TAccount account = await Repository.CreateAccountAsync();

            PasswordLogin<TAccount> login = await Repository.CreateLoginAsync<PasswordLogin<TAccount>>();

            var emailResult = await login.SetEmailAddressAsync(request.Email);

            var passwordResult = await login.SetPasswordAsync(request.Password);

            AccountCreationResultType creationResult;

            if(emailResult.Successful && passwordResult.Successful)
            {
                var verification = await RequestNewEmailVerificationCodeAsync(login);
                if (!verification.Successful)
                {
                    creationResult = AccountCreationResultType.CreatedButCodeNotSent;
                }
                else
                {
                    creationResult = AccountCreationResultType.CreatedAndSentCode;
                }
            }
            else if (!emailResult.Successful)
            {
                creationResult = AccountCreationResultType.InvalidEmail;
            }
            else if (!passwordResult.Successful)
            {
                creationResult = AccountCreationResultType.InvalidPassword;
            }
            else
            {
                creationResult = AccountCreationResultType.InvalidRequest;
            }

            return new EmailAccountCreationResult<TAccount, DateTimeOffset>
            {
                Successful = emailResult.Successful && passwordResult.Successful,
                SetEmailResult = emailResult,
                SetPasswordResult = passwordResult,
                Result = creationResult
            };
        }
    }
}
