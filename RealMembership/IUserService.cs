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
        TAccount GetUserById(string tenant, long id);

        /// <summary>
        /// Gets the user that belongs to the given tenant with the given email address.
        /// </summary>
        /// <param name="tenant">The name of the tenant that the account should be retrieved from.</param>
        /// <param name="email">The email address of the user that should be retrieved.</param>
        /// <returns></returns>
        IEmailLogin<TAccount, TDateTime> GetLoginByEmail(string tenant, string email);

        /// <summary>
        /// Gets the user that belongs to the given tenant with the given username.
        /// </summary>
        /// <param name="tenant">The name of the tenant that the account should be retrieved from.</param>
        /// <param name="username">The username of the user that should be retrieved.</param>
        /// <returns></returns>
        IUsernameLogin<TAccount, TDateTime> GetLoginByUsername(string tenant, string username);

        /// <summary>
        /// Creates a new account using the given account model and returns the result.
        /// </summary>
        /// <param name="account">The account that should be created.</param>
        /// <returns>Returns a new <see cref="AccountCreationResult"/> object that represents the result of the operation.</returns>
        AccountCreationResult CreateAccount(TAccount account);

        /// <summary>
        /// Requests a password reset for the given login.
        /// </summary>
        /// <param name="login">The login that the password reset is for.</param>
        /// <returns></returns>
        PasswordResetRequestResult RequestPasswordReset(IPasswordLogin<TAccount, TDateTime> login);

        /// <summary>
        /// Finishes the password reset process for the given code by applying the new password.
        /// </summary>
        /// <param name="code">The code that validates the password reset.</param>
        /// <param name="newPassword">The new password that should be used for the login.</param>
        /// <returns></returns>
        PasswordResetFinishResult<TAccount, TDateTime> FinishPasswordReset(string code, string newPassword);


        Task<AuthenticationResult> AuthenticateWithUsernameAsync(string tenant, string username, string password);
        Task<AuthenticationResult> AuthenticateWithEmailAndPasswordAsync(string tenant, string email, string password);
        Task<AuthenticationResult> AuthenticateWithLoginAsync(string tenant, IPasswordLogin<TAccount, TDateTime> login, string password);
        Task<AuthenticationResult> AuthenticateWithLoginAsync(string tenant, IPhoneLogin<TAccount, TDateTime> login, string code);
        Task<AuthenticationResult> AuthenticateWithEmailAndCodeAsync(string tenant, string email, string code);

    }
}
