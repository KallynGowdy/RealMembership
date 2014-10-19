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
    /// Defines an abstract base class for a <see cref="ILoginRepository"/> that uses a <see cref="IQueryable{T}"/> behind the scenes.
    /// </summary>
    public abstract class QueryableLoginRepository<TAccount> : ILoginRepository<TAccount>
        where TAccount : UserAccount
    {
        /// <summary>
        /// Gets the queryable list of logins that this repository has access to.
        /// </summary>
        /// <returns></returns>
        protected abstract IQueryable<Login> Logins
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
            where TEvent : LoginSecurityEvent
        {
            return (e) => string.Equals(e.Tenant, tenant, StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// Gets an expression that represents a comparision between a login's tenant and the given tenant.
        /// </summary>
        /// <typeparam name="TLogin"></typeparam>
        /// <param name="tenant"></param>
        /// <returns></returns>
        protected virtual Expression<Func<TLogin, bool>> WhereTenantEqualsForLoginExpression<TLogin>(string tenant)
            where TLogin : ILogin
        {
            return (l) => l.Account.Tenant.Equals(tenant, StringComparison.InvariantCultureIgnoreCase);
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
        /// Gets an expression that represents a comparision between an login's email address and the given address.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        protected virtual Expression<Func<EmailLogin, bool>> WhereEmailEqualsExpression(string email)
        {
            return (e) => e.EmailAddress.Equals(email, StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// Gets an expression that represents a comparision between a login's phone number and the given number.
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        protected virtual Expression<Func<PhoneLogin, bool>> WherePhoneEqualsExpression(string phoneNumber)
        {
            return (p) => p.PhoneNumber.Equals(phoneNumber, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Gets an expression that represents a comparision between a login's username and the given username.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        protected virtual Expression<Func<UsernameLogin, bool>> WhereUsernameEqualsExpression(string username)
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
        public virtual Task<EmailLogin> GetLoginByEmailAsync(string tenant, string email)
        {
            return Task.FromResult(
                Logins
                .OfType<EmailLogin>()
                .Where(WhereEmailEqualsExpression(email))
                .SingleOrDefault(WhereTenantEqualsForLoginExpression<EmailLogin>(tenant)));
        }

        /// <summary>
        /// Gets the login that belongs to the given tenant that has the given phone number. Returns null if it doesn't exist.
        /// </summary>
        /// <param name="tenant">The tenant that the login belongs to.</param>
        /// <param name="phoneNumber">The phone number of the login to retrieve.</param>
        /// <returns></returns>
        public virtual Task<PhoneLogin> GetLoginByPhoneAsync(string tenant, string phoneNumber)
        {
            return Task.FromResult(
                Logins
                .OfType<PhoneLogin>()
                .Where(WherePhoneEqualsExpression(phoneNumber))
                .SingleOrDefault(WhereTenantEqualsForLoginExpression<PhoneLogin>(tenant)));
        }

        /// <summary>
        /// Gets the login that belongs to the given tenant that has the given username. Returns null if it doesn't exist.
        /// </summary>
        /// <param name="tenant">The tenant that the login belongs to.</param>
        /// <param name="username">The username of the login to retrieve.</param>
        /// <returns></returns>
        public virtual Task<UsernameLogin> GetLoginByUsernameAsync(string tenant, string username)
        {
            return Task.FromResult(
                Logins
                .OfType<UsernameLogin>()
                .Where(WhereUsernameEqualsExpression(username))
                .SingleOrDefault(WhereTenantEqualsForLoginExpression<UsernameLogin>(tenant)));
        }

        /// <summary>
        /// Gets the login that currently posseses the given verification code.
        /// </summary>
        /// <param name="code">The code that is contained in the login that should be retrieved.</param>
        /// <returns></returns>
        public virtual Task<Login> GetLoginByVerificationCodeAsync(string code)
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
        public virtual Task<PasswordLogin> GetLoginByResetCodeAsync(string code)
        {
            string codeHash = PasswordLogin.GetCodeHash(code);
            return Task.FromResult(
                Logins.OfType<PasswordLogin>()
                .SingleOrDefault(l => l.ResetCodeHash.Equals(codeHash, StringComparison.Ordinal)));
        }

        /// <summary>
        /// Gets the queryable list of login security events that have been stored in the backing data store.
        /// </summary>
        /// <returns></returns>
        protected abstract IQueryable<LoginSecurityEvent> LoginSecurityEvents
        {
            get;
        }


        public virtual async Task<LoginAttempt> RecordAttemptForLoginAsync(string tenant, string identification, IdentificationType? identificationType, AuthenticationResult result, Login login)
        {
            return await RecordSecurityEventAsync(new LoginAttempt()
            {
                Tenant = tenant,
                LoginIdentification = identification,
                IdentificationType = identificationType,
                Result = result,
                Login = login,
                TimeOfEvent = DateTime.Now
            });
        }

        public virtual async Task<VerificationRequestAttempt> RecordAttemptForLoginVerificationAsync(string tenant, string identification, IdentificationType? identificationType, VerificationRequestResult result, Login login)
        {
            return await RecordSecurityEventAsync(new VerificationRequestAttempt()
            {
                Tenant = tenant,
                LoginIdentification = identification,
                IdentificationType = identificationType,
                RequestAttemptResult = result,
                Login = login,
                TimeOfEvent = DateTime.Now
            });
        }

        public virtual async Task<VerificationRequestAttempt> RecordAttemptForLoginVerificationAsync(string tenant, string identification, IdentificationType? identificationType, VerificationResult result, Login login)
        {
            // Try to retrieve an incomplete verification attempt first
            var attempt = LoginSecurityEvents
                .OfType<VerificationRequestAttempt>()
                .Where(WhereTenantEqualsForSecurityEventExpression<VerificationRequestAttempt>(tenant))
                .OrderByDescending(e => e.TimeOfEvent) // try retrieving the most recent events first
                .FirstOrDefault(e =>
                e.FinishTime == null &&
                e.RequestAttemptResult != null &&
                e.RequestAttemptResult.Result == VerificationRequestResultType.NewCodeCreated &&
                e.IdentificationType == identificationType &&
                ((login == null && e.LoginIdentification == identification) || (login != null && e.Login == login)));

            if (attempt != null) // if the verification attempt is not complete, then complete it
            {
                attempt.VerificationAttemptResult = result;
                attempt.FinishTime = DateTimeOffset.Now;
            }
            else // otherwise, record a new event.
            {
                attempt = new VerificationRequestAttempt
                {
                    Tenant = tenant,
                    IdentificationType = identificationType,
                    LoginIdentification = identification,
                    TimeOfEvent = DateTime.Now,
                    Login = login,
                    VerificationAttemptResult = result
                };
            }

            return await RecordSecurityEventAsync(attempt);
        }

        public virtual async Task<PasswordResetAttempt> RecordAttemptForPasswordResetAsync(string tenant, string identification, IdentificationType? identificationType, PasswordResetFinishResult result, PasswordLogin login)
        {
            // Try to retrieve an incomplete verification attempt first
            var attempt = LoginSecurityEvents
                .OfType<PasswordResetAttempt>()
                .Where(WhereTenantEqualsForSecurityEventExpression<PasswordResetAttempt>(tenant))
                .OrderByDescending(e => e.TimeOfEvent) // try retrieving the most recent events first
                .FirstOrDefault(e =>
                e.FinishTime == null &&
                e.RequestCodeResult != null &&
                e.RequestCodeResult.Result == PasswordResetRequestResultType.ResetCodeIssued &&
                e.IdentificationType == identificationType &&
                ((login == null && e.LoginIdentification == identification) || (login != null && e.Login == login)));

            if (attempt != null) // if the verification attempt is not complete, then complete it
            {
                attempt.FinishResetResult = result;
                attempt.FinishTime = DateTimeOffset.Now;
            }
            else // otherwise, record a new event.
            {
                attempt = new PasswordResetAttempt
                {
                    Tenant = tenant,
                    LoginIdentification = identification,
                    IdentificationType = identificationType,
                    Login = login,
                    TimeOfEvent = DateTimeOffset.Now,
                    FinishResetResult = result
                };
            }

            return await RecordSecurityEventAsync(attempt);
        }

        public virtual Task<PasswordResetAttempt> RecordAttemptForPasswordResetAsync(string tenant, string identification, IdentificationType? identificationType, PasswordResetRequestResult result, PasswordLogin login)
        {
            return RecordSecurityEventAsync(new PasswordResetAttempt
            {
                Tenant = tenant,
                LoginIdentification = identification,
                IdentificationType = identificationType,
                RequestCodeResult = result,
                Login = login,
                TimeOfEvent = DateTime.Now
            });
        }

        /// <summary>
        /// Persists the given security event in the backing data store and returns the created record either by creating a new record or updating the existing record.
        /// </summary>
        /// <typeparam name="TSecurityEvent">The type of the event that should be persisted.</typeparam>
        /// <param name="securityEvent">The event that should be persisted.</param>
        /// <returns></returns>
        protected abstract Task<TSecurityEvent> RecordSecurityEventAsync<TSecurityEvent>(TSecurityEvent securityEvent)
            where TSecurityEvent : LoginSecurityEvent;
        #endregion

        #region Not Implemented
        public abstract Task<TAccount> AddAccountAsync(TAccount account);
        public abstract Task<TAccount> CreateAccountAsync();
        public abstract Task<TLogin> CreateLoginAsync<TLogin>() where TLogin : Login;
        #endregion
    }
}
