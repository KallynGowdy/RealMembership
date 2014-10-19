using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealMembership.Logins;
using System.Linq.Expressions;
using RealMembership.Logins.SecurityEvents;

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
        /// Gets the queryable list of logins that this repository has access to.
        /// </summary>
        /// <returns></returns>
        protected abstract IQueryable<Login<TAccount, TDateTime>> Logins
        {
            get;
        }

        /// <summary>
        /// Gets the queryable list of accounts that this repository has access to.
        /// </summary>
        /// <returns></returns>
        protected abstract IQueryable<TAccount> Accounts
        {
            get;
        }

        #region Expressions
        /// <summary>
        /// Gets an expression that represents a comparision between a login security event's tenant and the given tenant.
        /// </summary>
        /// <param name="tenant"></param>
        /// <returns></returns>
        protected static Expression<Func<TEvent, bool>> WhereTenantEqualsForSecurityEventExpression<TEvent>(string tenant)
            where TEvent : LoginSecurityEvent<TAccount, TDateTime>
        {
            return (e) => string.Equals(e.Tenant, tenant, StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// Gets an expression that represents a comparision between an account's tenant and the given tenant.
        /// </summary>
        /// <param name="tenant"></param>
        /// <returns></returns>
        protected virtual Expression<Func<TAccount, bool>> WhereTenantEqualsForAccountExpression(string tenant)
        {
            return (a) => a.Tenant.Equals(tenant, StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// Gets an expression that represents a comparision between an account's tenant and the given tenant.
        /// </summary>
        /// <param name="tenant"></param>
        /// <returns></returns>
        protected virtual Expression<Func<TLogin, bool>> WhereTenantEqualsForLoginExpression<TLogin>(string tenant)
            where TLogin : Login<TAccount, TDateTime>
        {
            return (l) => l.Account.Tenant.Equals(tenant, StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// Gets an expression that represents a comparision between an login's email address and the given address.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        protected virtual Expression<Func<EmailLogin<TAccount, TDateTime>, bool>> WhereEmailEqualsExpression(string email)
        {
            return (e) => e.EmailAddress.Equals(email, StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// Gets an expression that represents a comparision between a login's phone number and the given number.
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        protected virtual Expression<Func<PhoneLogin<TAccount, TDateTime>, bool>> WherePhoneEqualsExpression(string phoneNumber)
        {
            return (p) => p.PhoneNumber.Equals(phoneNumber, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Gets an expression that represents a comparision between a login's username and the given username.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        protected virtual Expression<Func<UsernameLogin<TAccount, TDateTime>, bool>> WhereUsernameEqualsExpression(string username)
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
        /// Returns an awaitable task that results in the <typeparamref name="TAccount" /> that has the given ID.
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
        public virtual Task<EmailLogin<TAccount, TDateTime>> GetLoginByEmailAsync(string tenant, string email)
        {
            return Task.FromResult(
                Logins
                .OfType<EmailLogin<TAccount, TDateTime>>()
                .Where(WhereEmailEqualsExpression(email))
                .SingleOrDefault(WhereTenantEqualsForLoginExpression<EmailLogin<TAccount, TDateTime>>(tenant)));
        }

        /// <summary>
        /// Gets the login that belongs to the given tenant that has the given phone number. Returns null if it doesn't exist.
        /// </summary>
        /// <param name="tenant">The tenant that the login belongs to.</param>
        /// <param name="phoneNumber">The phone number of the login to retrieve.</param>
        /// <returns></returns>
        public virtual Task<PhoneLogin<TAccount, TDateTime>> GetLoginByPhoneAsync(string tenant, string phoneNumber)
        {
            return Task.FromResult(
                Logins
                .OfType<PhoneLogin<TAccount, TDateTime>>()
                .Where(WherePhoneEqualsExpression(phoneNumber))
                .SingleOrDefault(WhereTenantEqualsForLoginExpression<PhoneLogin<TAccount, TDateTime>>(tenant)));
        }



        /// <summary>
        /// Gets the login that belongs to the given tenant that has the given username. Returns null if it doesn't exist.
        /// </summary>
        /// <param name="tenant">The tenant that the login belongs to.</param>
        /// <param name="username">The username of the login to retrieve.</param>
        /// <returns></returns>
        public virtual Task<UsernameLogin<TAccount, TDateTime>> GetLoginByUsernameAsync(string tenant, string username)
        {
            return Task.FromResult(
                Logins
                .OfType<UsernameLogin<TAccount, TDateTime>>()
                .Where(WhereUsernameEqualsExpression(username))
                .SingleOrDefault(WhereTenantEqualsForLoginExpression<UsernameLogin<TAccount, TDateTime>>(tenant)));
        }

        /// <summary>
        /// Gets the login that currently posseses the given verification code.
        /// </summary>
        /// <param name="code">The code that is contained in the login that should be retrieved.</param>
        /// <returns></returns>
        public virtual Task<Login<TAccount, TDateTime>> GetLoginByVerificationCodeAsync(string code)
        {
            return Task.FromResult(Logins.SingleOrDefault(l => l.VerificationCode.Equals(code, StringComparison.Ordinal)));
        }

        /// <summary>
        /// Gets the login that contains the given password reset code. Returns null if it doesn't exist.
        /// </summary>
        /// <param name="code">The code that the login should be retrieved with.</param>
        /// <returns>
        /// Returns an awaitable task that results in the <see cref="IPasswordLogin{TAccount, TDate}" /> that has the given code.
        /// </returns>
        public virtual Task<PasswordLogin<TAccount, TDateTime>> GetLoginByResetCodeAsync(string code)
        {
            string codeHash = PasswordLogin<TAccount, TDateTime>.GetCodeHash(code);
            return Task.FromResult(
                Logins.OfType<PasswordLogin<TAccount, TDateTime>>()
                .SingleOrDefault(l => l.ResetCodeHash.Equals(codeHash, StringComparison.Ordinal)));
        }
        #endregion

        #region Not Implemented
        public abstract Task<LoginAttempt<TAccount, TDateTime>> RecordAttemptForLoginAsync(string tenant, string identification, IdentificationType? identificationType, AuthenticationResult result, Login<TAccount, TDateTime> login);

        public abstract Task<VerificationRequestAttempt<TAccount, TDateTime>> RecordAttemptForLoginVerificationAsync(string tenant, string identification, IdentificationType? identificationType, VerificationResult result, Login<TAccount, TDateTime> login);

        public abstract Task<VerificationRequestAttempt<TAccount, TDateTime>> RecordAttemptForLoginVerificationAsync(string tenant, string identification, IdentificationType? identificationType, VerificationRequestResult result, Login<TAccount, TDateTime> login);

        public abstract Task<PasswordResetAttempt<TAccount, TDateTime>> RecordAttemptForPasswordResetAsync(string tenant, string identification, IdentificationType? identificationType, PasswordResetFinishResult<TAccount, TDateTime> result, PasswordLogin<TAccount, TDateTime> login);

        public abstract Task<PasswordResetAttempt<TAccount, TDateTime>> RecordAttemptForPasswordResetAsync(string tenant, string identification, IdentificationType? identificationType, PasswordResetRequestResult result, PasswordLogin<TAccount, TDateTime> login);

        public abstract Task<TAccount> AddAccountAsync(TAccount account);
        public abstract Task<TAccount> CreateAccountAsync();
        public abstract Task<TLogin> CreateLoginAsync<TLogin>() where TLogin : Login<TAccount, TDateTime>;
        #endregion
    }
}
