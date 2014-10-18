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
using RealMembership.Logins.SecurityEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealMembership
{
    /// <summary>
    /// Defines an interface for a repository of user logins (because that's how we retrieve users anyway, by their login information).
    /// </summary>
    public interface ILoginRepository<TAccount, TDateTime>
        where TAccount : IUserAccount<TAccount, TDateTime>
        where TDateTime : struct
    {
        /// <summary>
        /// Gets the login that belongs to the given tenant that has the given username. Returns null if it doesn't exist.
        /// </summary>
        /// <param name="tenant">The tenant that the login belongs to.</param>
        /// <param name="username">The username of the login to retrieve.</param>
        /// <returns></returns>
        Task<UsernameLogin<TAccount, TDateTime>> GetLoginByUsernameAsync(string tenant, string username);

        /// <summary>
        /// Gets the login that belongs to the given tenant that has the given email address. Returns null if it doesn't exist.
        /// </summary>
        /// <param name="tenant">The tenant that the login belongs to.</param>
        /// <param name="email">The email address of the login to retrieve.</param>
        /// <returns></returns>
        Task<EmailLogin<TAccount, TDateTime>> GetLoginByEmailAsync(string tenant, string email);

        /// <summary>
        /// Gets the login that belongs to the given tenant that has the given phone number. Returns null if it doesn't exist. 
        /// </summary>
        /// <param name="tenant">The tenant that the login belongs to.</param>
        /// <param name="phoneNumber">The phone number of the login to retrieve.</param>
        /// <returns></returns>
        Task<PhoneLogin<TAccount, TDateTime>> GetLoginByPhoneAsync(string tenant, string phoneNumber);

        /// <summary>
        /// Gets the login that contains the given password reset code. Returns null if it doesn't exist.
        /// </summary>
        /// <param name="code">The code that the login should be retrieved with.</param>
        /// <returns>Returns an awaitable task that results in the <see cref="PasswordLogin{TAccount, TDate}"/> that has the given code.</returns>
        Task<PasswordLogin<TAccount, TDateTime>> GetLoginByResetCodeAsync(string code);

        /// <summary>
        /// Gets the account that has the given ID.
        /// </summary>
        /// <param name="id">The ID of the account that should be retrieved.</param>
        /// <returns>Returns an awaitable task that results in the <typeparamref name="TAccount"/> that has the given ID.</returns>
        Task<TAccount> GetAccountById(long id);

        /// <summary>
        /// Persists the given account in the backing data store.
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        Task CreateAccountAsync(TAccount account);

        /// <summary>
        /// Records a new login attempt that was against the given tenant using the given identification for a login with the given identification type that had the given result
        /// and was for the given login.
        /// </summary>
        /// <param name="tenant">The tenant that the authentication attempt was for.</param>
        /// <param name="identification">The identification that was used in the login attempt.</param>
        /// <param name="identificationType">The type of identification used. (email, username, etc.)</param>
        /// <param name="result">The result of the authentication attempt.</param>
        /// <param name="login">The specific login that the attempt was against. If null then no login was found.</param>
        /// <returns>Returns the login attempt that the information was recorded in.</returns>
        Task<LoginAttempt<TAccount, TDateTime>> RecordAttemptForLoginAsync(string tenant, string identification, IdentificationType? identificationType, AuthenticationResult result, Login<TAccount, TDateTime> login);

        /// <summary>
        /// Records a new password reset attempt that was against the given tenant using the given identification for a login with the given identification type that had the given
        /// result and was for the given login.
        /// </summary>
        /// <param name="tenant">The tenant that the password reset attempt was for.</param>
        /// <param name="identification">The identification that was used in the login attempt.</param>
        /// <param name="identificationType">The type of identification that was used.</param>
        /// <param name="result">The result of the password reset request attempt.</param>
        /// <param name="login">The specific login that the attempt was against. If null then no login was found.</param>
        /// <returns>Returns the password reset attempt that the information was recorded in.</returns>
        Task<PasswordResetAttempt<TAccount, TDateTime>> RecordAttemptForPasswordResetAsync(string tenant, string identification, IdentificationType? identificationType, PasswordResetRequestResult result, PasswordLogin<TAccount, TDateTime> login);

        /// <summary>
        /// Records the attempt to finish the password reset process for the given login for the given tenant using the given identification and resulted with the given
        /// result.
        /// </summary>
        /// <param name="tenant">The tenant that the password reset attempt was for.</param>
        /// <param name="identification">The identification that was used in the attempt.</param>
        /// <param name="identificationType">The type of identification that was used.</param>
        /// <param name="result">The result of the attempt to finish the password reset process.</param>
        /// <param name="login">The specific login that the attempt was against. If null then no login was found.</param>
        /// <returns>Returns the password reset attempt that the information was recorded in.</returns>
        Task<PasswordResetAttempt<TAccount, TDateTime>> RecordAttemptForPasswordResetAsync(string tenant, string identification, IdentificationType? identificationType, PasswordResetFinishResult<TAccount, TDateTime> result, PasswordLogin<TAccount, TDateTime> login);

        /// <summary>
        /// Records an attempt to retrieve a new verification code for the given login belonging to the given tenant using the given identification that resulted with the given result.
        /// </summary>
        /// <param name="tenant">The tenant that the verification request attempt was for.</param>
        /// <param name="identification">The identification that was used in the attempt.</param>
        /// <param name="identificationType">The type of identification that was used.</param>
        /// <param name="result">The result of the attempt to request a new verification code.</param>
        /// <param name="login">The specific login that the attempt was against. If null then no login was found.</param>
        /// <returns>Returns the verification request attempt that the information was recorded in.</returns>
        Task<VerificationRequestAttempt<TAccount, TDateTime>> RecordAttemptForLoginVerificationAsync(string tenant, string identification, IdentificationType? identificationType, VerificationRequestResult result, Login<TAccount, TDateTime> login);

        /// <summary>
        /// Records an attempt to verify the given login belonging to the given tenant using the given identification that resulted with the given result.
        /// </summary>
        /// <param name="tenant">The tenant that the verification attempt was for.</param>
        /// <param name="identification">The identification that was used in the attempt.</param>
        /// <param name="identificationType">The type of identification that was used.</param>
        /// <param name="result">The result of the attempt to verify the login.</param>
        /// <param name="login">The specific login that the attempt was against. If null then no login was found.</param>
        /// <returns>Returns the verification request attempt that the information was recorded in.</returns>
        Task<VerificationRequestAttempt<TAccount, TDateTime>> RecordAttemptForLoginVerificationAsync(string tenant, string identification, IdentificationType? identificationType, VerificationResult result, Login<TAccount, TDateTime> login);

        /// <summary>
        /// Gets the login that currently posseses the given verification code.
        /// </summary>
        /// <param name="code">The code that is contained in the login that should be retrieved.</param>
        /// <returns></returns>
        Task<Login<TAccount, TDateTime>> GetLoginByVerificationCodeAsync(string code);
    }
}
