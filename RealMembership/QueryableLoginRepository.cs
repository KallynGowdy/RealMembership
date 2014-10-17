using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealMembership.Logins;
using System.Linq.Expressions;

namespace RealMembership
{
    /// <summary>
    /// Defines an abstract base class for a <see cref="ILoginRepository{TAccount, TDateTime}"/> that uses a <see cref="IQueryable{T}"/> behind the scenes.
    /// </summary>
    public abstract class QueryableLoginRepository<TAccount, TDateTime> : ILoginRepository<TAccount, TDateTime>
        where TAccount : IUserAccount<TAccount, TDateTime>
        where TDateTime : struct
    {
        /// <summary>
        /// Gets or sets the queryable list of accounts that this repository has access to.
        /// </summary>
        /// <returns></returns>
        protected IQueryable<TAccount> Accounts
        {
            get;
            set;
        }

        #region Expressions
        /// <summary>
        /// Gets an expression that represents a comparision between an account's tenant and the given tenant.
        /// </summary>
        /// <param name="tenant"></param>
        /// <returns></returns>
        protected static Expression<Func<TAccount, bool>> WhereTenantEqualsExpression(string tenant)
        {
            return (a) => a.Tenant.Equals(tenant, StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// Gets an expression that represents a comparision between an login's email address and the given address.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        protected static Expression<Func<IEmailLogin<TAccount, TDateTime>, bool>> WhereEmailEqualsExpression(string email)
        {
            return (e) => e.EmailAddress.Equals(email, StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// Gets an expression that represents a comparision between a login's phone number and the given number.
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        protected static Expression<Func<IPhoneLogin<TAccount, TDateTime>, bool>> WherePhoneEqualsExpression(string phoneNumber)
        {
            return (p) => p.PhoneNumber.Equals(phoneNumber, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Gets an expression that represents a comparision between a login's username and the given username.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        protected static Expression<Func<IUsernameLogin<TAccount, TDateTime>, bool>> WhereUsernameEqualsExpression(string username)
        {
            return (u) => u.Username.Equals(username, StringComparison.InvariantCultureIgnoreCase);
        }
        #endregion

        #region Implemented
        /// <summary>
        /// Gets the account that has the given ID.
        /// </summary>
        /// <param name="id">The ID of the account that should be retrieved.</param>
        /// <returns>
        /// Returns an awaitable task that results in the <see cref="TAccount" /> that has the given ID.
        /// </returns>
        public virtual Task<TAccount> GetAccountById(long id)
        {
            return Task.FromResult(Accounts.SingleOrDefault(a => a.Id == id));
        }

        /// <summary>
        /// Gets the login that belongs to the given tenant that has the given email address. Returns null if it doesn't exist.
        /// </summary>
        /// <param name="tenant">The tenant that the login belongs to.</param>
        /// <param name="email">The email address of the login to retrieve.</param>
        /// <returns></returns>
        public virtual Task<IEmailLogin<TAccount, TDateTime>> GetLoginByEmailAsync(string tenant, string email)
        {
            return Task.FromResult(
                Accounts
                .Where(WhereTenantEqualsExpression(tenant))
                .SelectMany(a => a.Logins.OfType<IEmailLogin<TAccount, TDateTime>>())
                .SingleOrDefault(WhereEmailEqualsExpression(email)));
        }

        /// <summary>
        /// Gets the login that belongs to the given tenant that has the given phone number. Returns null if it doesn't exist.
        /// </summary>
        /// <param name="tenant">The tenant that the login belongs to.</param>
        /// <param name="phoneNumber">The phone number of the login to retrieve.</param>
        /// <returns></returns>
        public virtual Task<IPhoneLogin<TAccount, TDateTime>> GetLoginByPhoneAsync(string tenant, string phoneNumber)
        {
            return Task.FromResult(
                Accounts.Where(WhereTenantEqualsExpression(tenant))
                .SelectMany(a => a.Logins.OfType<IPhoneLogin<TAccount, TDateTime>>())
                .SingleOrDefault(WherePhoneEqualsExpression(phoneNumber)));
        }



        /// <summary>
        /// Gets the login that belongs to the given tenant that has the given username. Returns null if it doesn't exist.
        /// </summary>
        /// <param name="tenant">The tenant that the login belongs to.</param>
        /// <param name="username">The username of the login to retrieve.</param>
        /// <returns></returns>
        public Task<IUsernameLogin<TAccount, TDateTime>> GetLoginByUsernameAsync(string tenant, string username)
        {
            return Task.FromResult(
                Accounts.Where(WhereTenantEqualsExpression(tenant))
                .SelectMany(a => a.Logins.OfType<IUsernameLogin<TAccount, TDateTime>>())
                .SingleOrDefault(WhereUsernameEqualsExpression(username)));
        }
        #endregion

        #region Not Implemented

        /// <summary>
        /// Gets the login that contains the given password reset code. Returns null if it doesn't exist.
        /// </summary>
        /// <param name="code">The code that the login should be retrieved with.</param>
        /// <returns>
        /// Returns an awaitable task that results in the <see cref="IPasswordLogin{TAccount, TDate}" /> that has the given code.
        /// </returns>
        public abstract Task<IPasswordLogin<TAccount, TDateTime>> GetLoginByResetCodeAsync(string code);

        public abstract Task<ILogin<TAccount, TDateTime>> GetLoginByVerificationCodeAsync(string code);

        public abstract Task<ILoginAttempt<TAccount, TDateTime>> RecordAttemptForLoginAsync(string tenant, string identification, IdentificationType? identificationType, AuthenticationResult result, ILogin<TAccount, TDateTime> login);

        public abstract Task<IVerificationRequestAttempt<TAccount, TDateTime>> RecordAttemptForLoginVerificationAsync(string tenant, string identification, IdentificationType? identificationType, VerificationResult result, ILogin<TAccount, TDateTime> login);

        public abstract Task<IVerificationRequestAttempt<TAccount, TDateTime>> RecordAttemptForLoginVerificationAsync(string tenant, string identification, IdentificationType? identificationType, VerificationRequestResult result, ILogin<TAccount, TDateTime> login);

        public abstract Task<IPasswordResetAttempt<TAccount, TDateTime>> RecordAttemptForPasswordResetAsync(string tenant, string identification, IdentificationType? identificationType, PasswordResetFinishResult<TAccount, TDateTime> result, IPasswordLogin<TAccount, TDateTime> login);

        public abstract Task<IPasswordResetAttempt<TAccount, TDateTime>> RecordAttemptForPasswordResetAsync(string tenant, string identification, IdentificationType? identificationType, PasswordResetRequestResult result, IPasswordLogin<TAccount, TDateTime> login);

        public abstract Task CreateAccountAsync(TAccount account); 
        #endregion
    }
}
