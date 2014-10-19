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
using RealMembership.Logins;
using RealMembership.Logins.SecurityEvents;

namespace RealMembership
{
    /// <summary>
    /// Defines an abstract base class that provides an implementation of <see cref="IUserService{TAccount, TDateTime}"/>.
    /// </summary>
    /// <typeparam name="TAccount">The type of the user account.</typeparam>
    /// <typeparam name="TDateTime">The type of the values that should be used for datetime recording.</typeparam>
    public abstract class UserService<TAccount, TDateTime> : IUserService<TAccount, TDateTime>
        where TAccount : IUserAccount<TAccount, TDateTime>
        where TDateTime : struct
    {
        /// <summary>
        /// Gets or sets the repository of logins.
        /// </summary>
        /// <returns></returns>
        protected ILoginRepository<TAccount, TDateTime> Repository
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the email service.
        /// </summary>
        /// <returns></returns>
        protected IEmailService EmailService
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the SMS service.
        /// </summary>
        /// <returns></returns>
        protected ISmsService SmsService
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the <see cref="IMessageFormatter{TAccount, TDateTime}"/> that should be used to format the outgoing messages.
        /// </summary>
        /// <returns></returns>
        protected IMessageFormatter<TAccount, TDateTime> MessageFormatter
        {
            get;
            set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService{TAccount, TDateTime}"/> class.
        /// </summary>
        /// <param name="loginRepository">The login repository.</param>
        protected UserService(ILoginRepository<TAccount, TDateTime> loginRepository)
        {
            if (loginRepository == null) throw new ArgumentNullException("loginRepository");
            Repository = loginRepository;
        }

        /// <summary>
        /// Processes the given login as a login of the given target type and returns the correct authorization result for any
        /// problems with the given login. Returns null if there are no current problems.
        /// </summary>
        /// <typeparam name="TTargetLogin">The type of the login that is expected to be retrieved.</typeparam>
        /// <param name="login">The login that has currently been retrieved.</param>
        /// <param name="target">The target login that the given login will be cast from. Returned as null if the login could not be converted.</param>
        /// <returns>Returns a new authentication result that represents the result of the processing if any issues were found with the given login, otherwise null.</returns>
        protected AuthenticationResult ProcessBasicLogin<TTargetLogin>(Login<TAccount, TDateTime> login, out TTargetLogin target)
            where TTargetLogin : class, ILogin<TAccount, TDateTime>
        {
            target = login as TTargetLogin;
            var result = ProcessBasicLogin(login);
            if (result == null)
            {
                result = target == null ?
                    AuthenticationResult.IncorrectAuthenticationType :
                    null;
            }
            return result;
        }

        /// <summary>
        /// Processes the given login for any minor problems (inactive, not verified, locked out, etc.) and returns a new <see cref="AuthenticationResult"/> to
        /// represent the problems. Returns null if no problems were found.
        /// </summary>
        /// <param name="login">The login that should be processed.</param>
        /// <returns>Returns a new <see cref="AuthenticationResult"/> that represents the problem that was found with the login, if not problems were found then null.</returns>
        protected AuthenticationResult ProcessBasicLogin(Login<TAccount, TDateTime> login)
        {
            if (login == null)
                return AuthenticationResult.NotFound;
            else if (!login.IsCurrentlyActive)
                return AuthenticationResult.AccountNotActive;
            else if (!login.IsVerified)
                return AuthenticationResult.LoginNotVerified;
            else if (login.Account.IsLockedOut)
                return AuthenticationResult.AccountLockedOut;
            else
                return null;
        }

        protected AuthenticationResult ProcessTwoFactor(bool success, Login<TAccount, TDateTime> login)
        {
            if (success)
                if (login.Account.RequiresTwoFactorAuth)
                    return AuthenticationResult.GoodButRequiresTwoFactor;
                else
                    return AuthenticationResult.ValidCredentialProvided;
            else
            {
                if (login.Account.RequiresTwoFactorAuth)
                    return AuthenticationResult.InvalidAndRequiresTwoFactor;
                else
                    return AuthenticationResult.InvalidCredentialsProvided;
            }
        }

        /// <summary>
        /// Authenticates the given email and code against the given tenant and returns the result of the authentication attempt.
        /// </summary>
        /// <param name="tenant">The tenant that the user is trying to authenticate against.</param>
        /// <param name="email">The email address of the login to authenticate against.</param>
        /// <param name="code">The code that should be used for the credentials of the authentication.</param>
        /// <returns></returns>
        public virtual Task<AuthenticationResult> AuthenticateWithEmailAndCodeAsync(string tenant, string email, string code)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Authenticates the given email address and password against the given tenant and returns the result of the authentication attempt.
        /// </summary>
        /// <param name="tenant">The tenant that the user is trying to authenticate against.</param>
        /// <param name="email">The email of the login to authenticate against.</param>
        /// <param name="password">The password that should be used for the credentials of the authentication.</param>
        /// <returns></returns>
        public async Task<AuthenticationResult> AuthenticateWithEmailAndPasswordAsync(string tenant, string email, string password)
        {
            AuthenticationResult result;
            var login = await Repository.GetLoginByEmailAsync(tenant, email);
            PasswordLogin<TAccount, TDateTime> passwordLogin;
            if ((result = ProcessBasicLogin(login, out passwordLogin)) == null)
            {
                result = ProcessTwoFactor(await passwordLogin.MatchesPasswordAsync(password), passwordLogin);
            }
            await Repository.RecordAttemptForLoginAsync(tenant, email, IdentificationType.Email, result, login);
            return result;
        }

        /// <summary>
        /// Authenticates the given code against the given login and returns the result of the authentication attempt.
        /// </summary>
        /// <param name="login">The login to authenticate against.</param>
        /// <param name="code">The code that should be used for the credentials of the authentication.</param>
        /// <returns></returns>
        public virtual Task<AuthenticationResult> AuthenticateWithLoginAsync(PhoneLogin<TAccount, TDateTime> login, string code)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Authenticates the given password against the given login and returns the result of the authentication attempt.
        /// </summary>
        /// <param name="login">The login to authenticate against.</param>
        /// <param name="password">The password that should be used for the credentials of the authentication.</param>
        /// <returns></returns>
        public async Task<AuthenticationResult> AuthenticateWithLoginAsync(PasswordLogin<TAccount, TDateTime> login, string password)
        {
            AuthenticationResult result;
            if ((result = ProcessBasicLogin(login)) == null)
            {
                result = ProcessTwoFactor(await login.MatchesPasswordAsync(password), login);
            }
            await Repository.RecordAttemptForLoginAsync(login != null ? login.Account.Tenant : null, null, null, result, login);
            return result;
        }

        /// <summary>
        /// Authenticates the given username and password against the given tenant and returns the result of the authentication attempt.
        /// </summary>
        /// <param name="tenant">The tenant that the user is trying to authenticate against.</param>
        /// <param name="username">The username of the login to authenticate against.</param>
        /// <param name="password">The password that should be used for the credentials of the authentication.</param>
        /// <returns></returns>
        public async Task<AuthenticationResult> AuthenticateWithUsernameAsync(string tenant, string username, string password)
        {
            AuthenticationResult result;
            var login = await Repository.GetLoginByUsernameAsync(tenant, username);
            if ((result = ProcessBasicLogin(login)) == null)
            {
                result = ProcessTwoFactor(await login.MatchesPasswordAsync(password), login);
            }
            await Repository.RecordAttemptForLoginAsync(tenant, username, IdentificationType.Username, result, login);
            return result;
        }

        /// <summary>
        /// Gets the user that belongs to the given tenant with the given email address.
        /// </summary>
        /// <param name="tenant">The name of the tenant that the account should be retrieved from.</param>
        /// <param name="email">The email address of the user that should be retrieved.</param>
        /// <returns></returns>
        public Task<EmailLogin<TAccount, TDateTime>> GetLoginByEmailAsync(string tenant, string email)
        {
            return Repository.GetLoginByEmailAsync(tenant, email);
        }

        /// <summary>
        /// Gets the user that belongs to the given tenant with the given username.
        /// </summary>
        /// <param name="tenant">The name of the tenant that the account should be retrieved from.</param>
        /// <param name="username">The username of the user that should be retrieved.</param>
        /// <returns></returns>

        public Task<UsernameLogin<TAccount, TDateTime>> GetLoginByUsernameAsync(string tenant, string username)
        {
            return Repository.GetLoginByUsernameAsync(tenant, username);
        }

        /// <summary>
        /// Gets the user with the given ID.
        /// </summary>
        /// <param name="id">The ID of the user that should be retrieved.</param>
        /// <param name="tenant">The name of the tenant that the account should be retrieved from.</param>
        /// <returns></returns>
        public Task<TAccount> GetUserByIdAsync(string tenant, long id)
        {
            return Repository.GetAccountById(id);
        }

        /// <summary>
        /// Finishes the password reset process for the given code by applying the new password.
        /// </summary>
        /// <param name="code">The code that validates the password reset.</param>
        /// <param name="newPassword">The new password that should be used for the login.</param>
        /// <returns></returns>
        public virtual async Task<PasswordResetFinishResult<TAccount, TDateTime>> FinishPasswordResetAsync(string code, string newPassword)
        {
            PasswordResetFinishResult<TAccount, TDateTime> result;
            var login = await Repository.GetLoginByResetCodeAsync(code);
            if (login == null)
            {
                result = new PasswordResetFinishResult<TAccount, TDateTime>
                {
                    Successful = false,
                    SetPasswordResult = null,
                    GeneralResult = PasswordResetFinishType.InvalidCode
                };
            }
            else
            {
                var passwordSetResult = await login.SetPasswordAsync(newPassword);
                result = new PasswordResetFinishResult<TAccount, TDateTime>
                {
                    Successful = passwordSetResult.Successful,
                    GeneralResult = passwordSetResult.Successful ? PasswordResetFinishType.PasswordReset : PasswordResetFinishType.InvalidPassword,
                    SetPasswordResult = passwordSetResult
                };
            }
            if (result.Successful)
            {
                await SendPasswordChangedEmailAsync(login);
            }
            await Repository.RecordAttemptForPasswordResetAsync(login != null ? login.Account.Tenant : null, code, IdentificationType.ResetCode, result, login);
            return result;
        }

        /// <summary>
        /// Requests a password reset for the login contained by the given tenant with the given email.
        /// </summary>
        /// <param name="email">The email address of the login that the password request is for.</param>
        /// <param name="tenant">The tenant that the login should be retrieved from.</param>
        /// <returns></returns>
        public virtual async Task<PasswordResetRequestResult> RequestEmailPasswordResetAsync(string tenant, string email)
        {
            PasswordLogin<TAccount, TDateTime> login = await Repository.GetLoginByEmailAsync(tenant, email) as PasswordLogin<TAccount, TDateTime>;
            PasswordResetRequestResult result = await RequestPasswordResetAsync(login);
            await Repository.RecordAttemptForPasswordResetAsync(tenant, email, IdentificationType.Email, result, login);
            return result;
        }

        /// <summary>
        /// Requests a password reset for the login contained by the given tenant with the given username.
        /// </summary>
        /// <param name="username">The username of the login that the password reset is for.</param>
        /// <param name="tenant">The tenant that the login should be retrieved from.</param>
        /// <returns></returns>
        public virtual async Task<PasswordResetRequestResult> RequestUsernamePasswordResetAsync(string tenant, string username)
        {
            PasswordLogin<TAccount, TDateTime> login = await Repository.GetLoginByUsernameAsync(tenant, username);
            PasswordResetRequestResult result = await RequestPasswordResetAsync(login);
            await Repository.RecordAttemptForPasswordResetAsync(tenant, username, IdentificationType.Username, result, login);
            return result;
        }

        /// <summary>
        /// Requests a password reset for the given login.
        /// </summary>
        /// <param name="login">The login that the password reset is for.</param>
        /// <returns></returns>
        protected async Task<PasswordResetRequestResult> RequestPasswordResetAsync(PasswordLogin<TAccount, TDateTime> login)
        {
            PasswordResetRequestResult result;
            if (login == null)
            {
                result = new PasswordResetRequestResult
                {
                    Successful = false,
                    Code = null,
                    Result = PasswordResetRequestResultType.NonExistantLogin
                };
            }
            else
            {
                result = await login.RequestResetCodeAsync();
                if (result.Successful)
                {
                    await SendRequestPasswordResetEmailAsync(result.Code, login);
                }
            }
            return result;
        }

        /// <summary>
        /// Attempts to verify the login that contains the given verification code and returns the result of the verification.
        /// </summary>
        /// <param name="code">The verifcation code.</param>
        /// <returns></returns>
        public async Task<VerificationResult> VerifyLoginWithCodeAsync(string code)
        {
            VerificationResult result;
            Login<TAccount, TDateTime> login = await Repository.GetLoginByVerificationCodeAsync(code);
            if (login == null)
            {
                result = new VerificationResult
                {
                    Successful = false,
                    Result = VerificationResultType.CodeNotFound
                };
            }
            else
            {
                result = await login.VerifyAsync(code);
            }
            if (result.Successful)
            {
                EmailLogin<TAccount, TDateTime> email = login as EmailLogin<TAccount, TDateTime>;
                if (email != null)
                {
                    await SendVerifiedEmailAsync(email);
                }
            }
            await Repository.RecordAttemptForLoginVerificationAsync(null, code, IdentificationType.VerificationCode, result, login);
            return result;
        }

        /// <summary>
        /// Requests a new verification code to be sent for the given email belonging to the given tentant.
        /// </summary>
        /// <param name="tenant">The tenant that the email address belongs to.</param>
        /// <param name="email">The email address that the new verification code is being requested for.</param>
        /// <returns>Returns a new awaitable task that results in a new <see cref="VerificationRequestResult"/> that represents the result of the request.</returns>
        public async Task<VerificationRequestResult> RequestNewEmailVerificationCodeAsync(string tenant, string email)
        {
            EmailLogin<TAccount, TDateTime> login = await GetLoginByEmailAsync(tenant, email);
            return await RequestNewEmailVerificationCodeAsync(login);
        }

        protected async Task<VerificationRequestResult> RequestNewEmailVerificationCodeAsync(EmailLogin<TAccount,TDateTime> login)
        {
            VerificationRequestResult result;
            if (login == null)
            {
                result = new VerificationRequestResult
                {
                    Successful = false,
                    Code = null,
                    Result = VerificationRequestResultType.NotFound
                };
            }
            else
            {
                result = await login.RequestVerificationCodeAsync();
                if (result.Successful)
                {
                    await SendVerificationEmailAsync(result.Code, login);
                }
            }
            return result;
        }

        /// <summary>
        /// Requests a new verification code to be sent to the given phone number belonging to the given tenant.
        /// </summary>
        /// <param name="tenant">The tenant that the phone number is contained by.</param>
        /// <param name="phoneNumber">The phone number that the new code should be sent to.</param>
        /// <returns>
        /// Returns a new awaitable task that results in the result of the verification request.
        /// </returns>
        public async virtual Task<VerificationRequestResult> RequestNewSmsVerificationCodeAsync(string tenant, string phoneNumber)
        {
            PhoneLogin<TAccount, TDateTime> login = await Repository.GetLoginByPhoneAsync(tenant, phoneNumber);
            VerificationRequestResult result;
            if (login == null)
            {
                result = new VerificationRequestResult
                {
                    Successful = false,
                    Code = null,
                    Result = VerificationRequestResultType.NotFound
                };
            }
            else
            {
                result = await login.RequestVerificationCodeAsync();
                if (result.Successful)
                {
                    await SendSmsAsync(new SmsMessage
                    {
                        PhoneNumber = login.PhoneNumber,
                        Body = await MessageFormatter.FormatVerifyLoginMessageAsync(result.Code, login)
                    });
                }
            }
            await Repository.RecordAttemptForLoginVerificationAsync(tenant, phoneNumber, IdentificationType.PhoneNumber, result, login);
            return result;
        }

        protected async virtual Task SendRequestPasswordResetEmailAsync(string code, PasswordLogin<TAccount, TDateTime> login)
        {
            await SendEmailAsync(new EmailMessage
            {
                Recipent = login.EmailAddress,
                Html = await MessageFormatter.FormatPasswordResetMessageAsync(code, login),
                Subject = await MessageFormatter.FormatPasswordResetSubjectAsync(login)
            });
        }

        protected async virtual Task SendPasswordChangedEmailAsync(PasswordLogin<TAccount, TDateTime> login)
        {
            await SendEmailAsync(new EmailMessage
            {
                Recipent = login.EmailAddress,
                Html = await MessageFormatter.FormatPasswordChangedMessageAsync(login),
                Subject = await MessageFormatter.FormatVerifyLoginSubjectAsync(login)
            });
        }

        protected async virtual Task SendVerificationEmailAsync(string code, EmailLogin<TAccount, TDateTime> login)
        {
            await SendEmailAsync(new EmailMessage
            {
                Recipent = login.EmailAddress,
                Html = await MessageFormatter.FormatVerifyLoginMessageAsync(code, login),
                Subject = await MessageFormatter.FormatVerifyLoginSubjectAsync(login)
            });
        }

        protected async virtual Task SendVerifiedEmailAsync(EmailLogin<TAccount, TDateTime> login)
        {
            await SendEmailAsync(new EmailMessage
            {
                Recipent = login.EmailAddress,
                Html = await MessageFormatter.FormatVerifiedMessageAsync(login),
                Subject = await MessageFormatter.FormatVerifiedSubjectAsync(login)
            });
        }

        /// <summary>
        /// Sends the given <see cref="SmsMessage"/> using the configured <see cref="SmsService"/>.
        /// Throws a new <see cref="InvalidOperationException"/> if no <see cref="SmsService"/> was configured.
        /// </summary>
        /// <param name="message">The message that should be sent.</param>
        protected virtual Task SendSmsAsync(SmsMessage message)
        {
            if (SmsService == null)
            {
                throw new InvalidOperationException("No SMS Service was provided so SMS cannot be sent.");
            }
            else
            {
                return SmsService.SendSmsAsync(message);
            }
        }

        /// <summary>
        /// Sends the given <see cref="EmailMessage"/> using the configured <see cref="EmailService"/>.
        /// Throws a new <see cref="InvalidOperationException"/> if no <see cref="EmailService"/> was configured.
        /// </summary>
        /// <param name="message">The message that should be sent.</param>
        protected virtual Task SendEmailAsync(EmailMessage message)
        {
            if (EmailService == null)
            {
                throw new InvalidOperationException("No Email Service was provided so email cannot be sent.");
            }
            else
            {
                return EmailService.SendEmailAsync(message);
            }
        }

        /// <summary>
        /// Creates a new account using the given account model and returns the result.
        /// </summary>
        /// <param name="request">The request that contains the information on how the account should be created.</param>
        /// <returns>Returns a new <see cref="AccountCreationResult{TAccount, TDateTime}"/> object that represents the result of the operation.</returns>
        public async virtual Task<AccountCreationResult<TAccount, TDateTime>> CreateAccountAsync(AccountCreationRequest request)
        {
            if (request is EmailAccountCreationRequest)
            {
                return await CreateAccountAsync((EmailAccountCreationRequest)request);
            }
            else
            {
                return new AccountCreationResult<TAccount, TDateTime>
                {
                    Successful = false,
                    Result = AccountCreationResultType.InvalidRequest,
                    CreatedAccount = default(TAccount)
                };
            }
        }

        /// <summary>
        /// Creates a new account using the given account model and returns the result
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public abstract Task<EmailAccountCreationResult<TAccount, TDateTime>> CreateAccountAsync(EmailAccountCreationRequest request);
    }
}
