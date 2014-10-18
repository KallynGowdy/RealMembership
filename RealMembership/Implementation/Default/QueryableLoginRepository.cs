using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealMembership.Logins;
using RealMembership.Logins.SecurityEvents;
using System.Linq.Expressions;

namespace RealMembership.Implementation.Default
{
    /// <summary>
    /// Defines an abstract class that provides a basic implementation of <see cref="ILoginRepository{TAccount, TDateTime}"/>.
    /// </summary>
    /// <typeparam name="TAccount">The type of accounts used.</typeparam>
    public abstract class QueryableLoginRepository<TAccount> : QueryableLoginRepository<TAccount, DateTimeOffset>
        where TAccount : IUserAccount<TAccount, DateTimeOffset>
    {
        /// <summary>
        /// Gets the queryable list of login security events that have been stored in the backing data store.
        /// </summary>
        /// <returns></returns>
        protected abstract IQueryable<LoginSecurityEvent<TAccount, DateTimeOffset>> LoginSecurityEvents
        {
            get;
        }

        public override async Task<LoginAttempt<TAccount, DateTimeOffset>> RecordAttemptForLoginAsync(string tenant, string identification, IdentificationType? identificationType, AuthenticationResult result, Login<TAccount, DateTimeOffset> login)
        {
            return await RecordSecurityEventAsync(new LoginAttempt<TAccount, DateTimeOffset>()
            {
                Tenant = tenant,
                LoginIdentification = identification,
                IdentificationType = identificationType,
                Result = result,
                Login = login,
                TimeOfEvent = DateTime.Now
            });
        }

        public override async Task<VerificationRequestAttempt<TAccount, DateTimeOffset>> RecordAttemptForLoginVerificationAsync(string tenant, string identification, IdentificationType? identificationType, VerificationRequestResult result, Login<TAccount, DateTimeOffset> login)
        {
            return await RecordSecurityEventAsync(new VerificationRequestAttempt<TAccount, DateTimeOffset>()
            {
                Tenant = tenant,
                LoginIdentification = identification,
                IdentificationType = identificationType,
                RequestAttemptResult = result,
                Login = login,
                TimeOfEvent = DateTime.Now
            });
        }

        public override async Task<VerificationRequestAttempt<TAccount, DateTimeOffset>> RecordAttemptForLoginVerificationAsync(string tenant, string identification, IdentificationType? identificationType, VerificationResult result, Login<TAccount, DateTimeOffset> login)
        {
            // Try to retrieve an incomplete verification attempt first
            var attempt = LoginSecurityEvents
                .OfType<VerificationRequestAttempt<TAccount, DateTimeOffset>>()
                .Where(WhereTenantEqualsForSecurityEventExpression<VerificationRequestAttempt<TAccount, DateTimeOffset>>(tenant))
                .OrderByDescending(e => e.TimeOfEvent) // try retrieving the most recent events first
                .FirstOrDefault(e =>
                e.FinishTime == null &&
                e.RequestAttemptResult != null &&
                e.RequestAttemptResult.Result == VerificationRequestResultType.NewCodeCreated &&
                e.IdentificationType == identificationType &&
                ((login == null && e.LoginIdentification == identification) || (login != null && e.Login == login)));

            if(attempt != null) // if the verification attempt is not complete, then complete it
            {
                attempt.VerificationAttemptResult = result;
                attempt.FinishTime = DateTimeOffset.Now;
            }
            else // otherwise, record a new event.
            {
                attempt = new VerificationRequestAttempt<TAccount, DateTimeOffset>
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

        public override async Task<PasswordResetAttempt<TAccount, DateTimeOffset>> RecordAttemptForPasswordResetAsync(string tenant, string identification, IdentificationType? identificationType, PasswordResetFinishResult<TAccount, DateTimeOffset> result, PasswordLogin<TAccount, DateTimeOffset> login)
        {
            // Try to retrieve an incomplete verification attempt first
            var attempt = LoginSecurityEvents
                .OfType<PasswordResetAttempt<TAccount, DateTimeOffset>>()
                .Where(WhereTenantEqualsForSecurityEventExpression<PasswordResetAttempt<TAccount, DateTimeOffset>>(tenant))
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
                attempt = new PasswordResetAttempt<TAccount, DateTimeOffset>
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

        /// <summary>
        /// Persists the given security event in the backing data store and returns the created record either by creating a new record or updating the existing record.
        /// </summary>
        /// <typeparam name="TSecurityEvent">The type of the event that should be persisted.</typeparam>
        /// <param name="securityEvent">The event that should be persisted.</param>
        /// <returns></returns>
        protected abstract Task<TSecurityEvent> RecordSecurityEventAsync<TSecurityEvent>(TSecurityEvent securityEvent)
            where TSecurityEvent : LoginSecurityEvent<TAccount, DateTimeOffset>;
    }
}
