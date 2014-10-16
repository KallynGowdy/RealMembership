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

namespace RealMembership
{
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
        /// Initializes a new instance of the <see cref="UserService{TAccount, TDateTime}"/> class.
        /// </summary>
        /// <param name="loginRepository">The login repository.</param>
        protected UserService(ILoginRepository<TAccount, TDateTime> loginRepository)
        {
            if (loginRepository == null) throw new ArgumentNullException("loginRepository");
            Repository = loginRepository;
        }

        protected AuthenticationResult ProcessBasicLogin<TTargetLogin>(ILogin<TAccount, TDateTime> login, out TTargetLogin target)
            where TTargetLogin : class, ILogin<TAccount, TDateTime>
        {
            target = login as TTargetLogin;
            var result = ProcessBasicLogin(login);
            if(result == null)
            {
                result = target == null ?
                    AuthenticationResult.IncorrectAuthenticationType :
                    null;
            }
            return result;
        }

        protected AuthenticationResult ProcessBasicLogin(ILogin<TAccount, TDateTime> login)
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

        protected AuthenticationResult ProcessTwoFactor(bool success, ILogin<TAccount, TDateTime> login)
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

        public Task<AuthenticationResult> AuthenticateWithEmailAndCodeAsync(string tenant, string email, string code)
        {
            throw new NotImplementedException();
        }

        public async Task<AuthenticationResult> AuthenticateWithEmailAndPasswordAsync(string tenant, string email, string password)
        {
            AuthenticationResult result;
            var login = await Repository.GetLoginByEmailAsync(tenant, email);
            IEmailPasswordLogin<TAccount, TDateTime> passwordLogin;
            if ((result = ProcessBasicLogin(login, out passwordLogin)) == null)
            {
                result = ProcessTwoFactor(passwordLogin.MatchesPassword(password), passwordLogin);
            }
            await Repository.RecordAttemptForLoginAsync(tenant, email, "email", result, login);
            return result;
        }

        public Task<AuthenticationResult> AuthenticateWithLoginAsync(string tenant, IPhoneLogin<TAccount, TDateTime> login, string code)
        {
            throw new NotImplementedException();
        }

        public async Task<AuthenticationResult> AuthenticateWithLoginAsync(string tenant, IPasswordLogin<TAccount, TDateTime> login, string password)
        {
            AuthenticationResult result;
            if((result = ProcessBasicLogin(login)) == null)
            {
                result = ProcessTwoFactor(login.MatchesPassword(password), login);
            }
            await Repository.RecordAttemptForLoginAsync(tenant, null, null, result, login);
            return result;
        }

        public async Task<AuthenticationResult> AuthenticateWithUsernameAsync(string tenant, string username, string password)
        {
            AuthenticationResult result;
            var login = await Repository.GetLoginByUsernameAsync(tenant, username);
            if ((result = ProcessBasicLogin(login)) == null)
            {
                result = ProcessTwoFactor(login.MatchesPassword(password), login);
            }
            await Repository.RecordAttemptForLoginAsync(tenant, username, "username", result, login);
            return result;
        }

        public AccountCreationResult CreateAccount(TAccount account)
        {
            throw new NotImplementedException();
        }

        public PasswordResetFinishResult<TAccount, TDateTime> FinishPasswordReset(string code, string newPassword)
        {
            throw new NotImplementedException();
        }

        public IEmailLogin<TAccount, TDateTime> GetLoginByEmail(string tenant, string email)
        {
            throw new NotImplementedException();
        }

        public IUsernameLogin<TAccount, TDateTime> GetLoginByUsername(string tenant, string username)
        {
            throw new NotImplementedException();
        }

        public TAccount GetUserById(string tenant, long id)
        {
            throw new NotImplementedException();
        }

        public PasswordResetRequestResult RequestPasswordReset(IPasswordLogin<TAccount, TDateTime> login)
        {
            throw new NotImplementedException();
        }
    }
}
