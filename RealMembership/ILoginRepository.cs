using RealMembership.Logins;
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
        Task<IUsernameLogin<TAccount, TDateTime>> GetLoginByUsernameAsync(string tenant, string username);

        /// <summary>
        /// Gets the login that belongs to the given tenant that has the given email address. Returns null if it doesn't exist.
        /// </summary>
        /// <param name="tenant">The tenant that the login belongs to.</param>
        /// <param name="email">The email address of the login to retrieve.</param>
        /// <returns></returns>
        Task<IEmailLogin<TAccount, TDateTime>> GetLoginByEmailAsync(string tenant, string email);

        /// <summary>
        /// Gets the login that belongs to the given tenant that has the given phone number. Returns null if it doesn't exist. 
        /// </summary>
        /// <param name="tenant">The tenant that the login belongs to.</param>
        /// <param name="phoneNumber">The phone number of the login to retrieve.</param>
        /// <returns></returns>
        Task<IPhoneLogin<TAccount, TDateTime>> GetLoginByPhoneAsync(string tenant, string phoneNumber);

        /// <summary>
        /// Records a new login attempt that was against the given tenant using the given identification for a login with the given identification type that had the given result
        /// and was for the given login.
        /// </summary>
        /// <param name="tenant">The tenant that the authentication attempt was for.</param>
        /// <param name="identification">The identification that was used in the login attempt.</param>
        /// <param name="identificationType">The type of identification used. (email, username, etc.)</param>
        /// <param name="result">The result of the authentication attempt.</param>
        /// <param name="login">The specific login that the attempt was against. If null then no login was found.</param>
        /// <returns>Returns the newly created login attempt.</returns>
        Task<ILoginAttempt<TAccount, TDateTime>> RecordAttemptForLoginAsync(string tenant, string identification, string identificationType, AuthenticationResult result, ILogin<TAccount, TDateTime> login);
    }
}
