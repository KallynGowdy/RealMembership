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

using RealMembership.Logins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealMembership
{
    /// <summary>
    /// Defines an interface for objects that provide services that manipulate users.
    /// </summary>
    public interface IUserService<TAccount, TDateTime>
        where TAccount : IUserAccount<TAccount, TDateTime>
        where TDateTime : struct
    {
        /// <summary>
        /// Gets the user with the given ID.
        /// </summary>
        /// <param name="id">The ID of the user that should be retrieved./param>
        /// <param name="tenant">The name of the tenant that the account should be retrieved from.</param>
        /// <returns></returns>
        Task<TAccount> GetUserByIdAsync(string tenant, long id);

        /// <summary>
        /// Gets the user that belongs to the given tenant with the given email address.
        /// </summary>
        /// <param name="tenant">The name of the tenant that the account should be retrieved from.</param>
        /// <param name="email">The email address of the user that should be retrieved.</param>
        /// <returns></returns>
        Task<IEmailLogin<TAccount, TDateTime>> GetLoginByEmailAsync(string tenant, string email);

        /// <summary>
        /// Gets the user that belongs to the given tenant with the given username.
        /// </summary>
        /// <param name="tenant">The name of the tenant that the account should be retrieved from.</param>
        /// <param name="username">The username of the user that should be retrieved.</param>
        /// <returns></returns>
        Task<IUsernameLogin<TAccount, TDateTime>> GetLoginByUsernameAsync(string tenant, string username);

        /// <summary>
        /// Creates a new account using the given account model and returns the result.
        /// </summary>
        /// <param name="account">The account that should be created.</param>
        /// <returns>Returns a new <see cref="AccountCreationResult"/> object that represents the result of the operation.</returns>
        Task<AccountCreationResult> CreateAccountAsync(TAccount account);

        /// <summary>
        /// Requests a password reset for the login contained by the given tenant with the given email.
        /// </summary>
        /// <param param name="email">The email address of the login that the password request is for.</param>
        /// <param name="tenant">The tenant that the login should be retrieved from.</param>
        /// <returns></returns>
        Task<PasswordResetRequestResult> RequestEmailPasswordResetAsync(string tenant, string email);

        /// <summary>
        /// Requests a password reset for the login contained by the given tenant with the given username.
        /// </summary>
        /// <param name="login">The login that the password reset is for.</param>
        /// <returns></returns>
        Task<PasswordResetRequestResult> RequestUsernamePasswordResetAsync(string tenant, string username);

        /// <summary>
        /// Finishes the password reset process for the given code by applying the new password.
        /// </summary>
        /// <param name="code">The code that validates the password reset.</param>
        /// <param name="newPassword">The new password that should be used for the login.</param>
        /// <returns></returns>
        Task<PasswordResetFinishResult<TAccount, TDateTime>> FinishPasswordResetAsync(string code, string newPassword);

        /// <summary>
        /// Authenticates the given username and password against the given tenant and returns the result of the authentication attempt.
        /// </summary>
        /// <param name="tenant">The tenant that the user is trying to authenticate against.</param>
        /// <param name="username">The username of the login to authenticate against.</param>
        /// <param name="password">The password that should be used for the credentials of the authentication.</param>
        /// <returns></returns>
        Task<AuthenticationResult> AuthenticateWithUsernameAsync(string tenant, string username, string password);

        /// <summary>
        /// Authenticates the given email address and password against the given tenant and returns the result of the authentication attempt.
        /// </summary>
        /// <param name="tenant">The tenant that the user is trying to authenticate against.</param>
        /// <param name="email">The email of the login to authenticate against.</param>
        /// <param name="password">The password that should be used for the credentials of the authentication.</param>
        /// <returns></returns>
        Task<AuthenticationResult> AuthenticateWithEmailAndPasswordAsync(string tenant, string email, string password);

        /// <summary>
        /// Authenticates the given password against the given login and returns the result of the authentication attempt.
        /// </summary>
        /// <param name="tenant">The tenant that the user is trying to authenticate against.</param>
        /// <param name="login">The login to authenticate against.</param>
        /// <param name="password">The password that should be used for the credentials of the authentication.</param>
        /// <returns></returns>
        Task<AuthenticationResult> AuthenticateWithLoginAsync(IPasswordLogin<TAccount, TDateTime> login, string password);

        /// <summary>
        /// Authenticates the given code against the given login and returns the result of the authentication attempt.
        /// </summary>
        /// <param name="tenant">The tenant that the user is trying to authenticate against.</param>
        /// <param name="login">The login to authenticate against.</param>
        /// <param name="code">The code that should be used for the credentials of the authentication.</param>
        /// <returns></returns>
        Task<AuthenticationResult> AuthenticateWithLoginAsync(IPhoneLogin<TAccount, TDateTime> login, string code);

        /// <summary>
        /// Authenticates the given email and code against the given tenant and returns the result of the authentication attempt.
        /// </summary>
        /// <param name="tenant">The tenant that the user is trying to authenticate against.</param>
        /// <param name="email">The email address of the login to authenticate against.</param>
        /// <param name="code">The code that should be used for the credentials of the authentication.</param>
        /// <returns></returns>
        Task<AuthenticationResult> AuthenticateWithEmailAndCodeAsync(string tenant, string email, string code);

        /// <summary>
        /// Attempts to verify the login that contains the given verification code and returns the result of the verification.
        /// </summary>
        /// <param name="code">The verifcation code.</param>
        /// <returns></returns>
        Task<VerificationResult> VerifyLoginWithCodeAsync(string code);

        /// <summary>
        /// Requests a new verification code to be sent for the given email belonging to the given tenant.
        /// </summary>
        /// <param name="tenant">The tenant that the email address belongs to.</param>
        /// <param name="email">The email address that the new verification code is being requested for.</param>
        /// <returns>Returns a new awaitable task that results in a new <see cref="VerificationRequestResult"/> that represents the result of the request.</returns>
        Task<VerificationRequestResult> RequestNewEmailVerificationCodeAsync(string tenant, string email);

        /// <summary>
        /// Requests a new verification code to be sent to the given phone number belonging to the given tenant.
        /// </summary>
        /// <param name="tenant">The tenant that the phone number is contained by.</param>
        /// <param name="phoneNumber">The phone number that the new code should be sent to.</param>
        /// <returns>Returns a new awaitable task that results in the result of the verification request.</returns>
        Task<VerificationRequestResult> RequestNewSmsVerificationCodeAsync(string tenant, string phoneNumber);
    }
}
