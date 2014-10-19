using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealMembership.Logins;
using RealMembership.Logins.SecurityEvents;

namespace RealMembership.Implementation.EF
{
    /// <summary>
    /// Defines a class that provides an implementation of <see cref="ILoginRepository{TAccount, DateTimeOffset}"/>.
    /// </summary>
    public class LoginRepository<TAccount> : QueryableLoginRepository<TAccount>
        where TAccount : UserAccount<TAccount>
    {
        public LoginRepository(UserAccountDbContext<TAccount> context)
        {
            if (context == null) throw new ArgumentNullException("context");
            this.Context = context;
        }

        protected UserAccountDbContext<TAccount> Context
        {
            get;
            private set;
        }

        protected override IQueryable<TAccount> Accounts
        {
            get
            {
                return Context.Accounts;
            }
        }

        protected override IQueryable<Login<TAccount, DateTimeOffset>> Logins
        {
            get
            {
                return Context.Logins;
            }
        }

        protected override IQueryable<LoginSecurityEvent<TAccount, DateTimeOffset>> LoginSecurityEvents
        {
            get
            {
                return Context.SecurityEvents;
            }
        }

        public async override Task<TAccount> AddAccountAsync(TAccount account)
        {
            account = Context.Accounts.Add(account);
            await Context.SaveChangesAsync();
            return account;
        }

        public override Task<TAccount> CreateAccountAsync()
        {
            return Task.FromResult(Context.Accounts.Create());
        }

        public override Task<TLogin> CreateLoginAsync<TLogin>()
        {
            return Task.FromResult(Context.Logins.Create<TLogin>());
        }        

        protected async override Task<TSecurityEvent> RecordSecurityEventAsync<TSecurityEvent>(TSecurityEvent securityEvent)
        {
            securityEvent = (TSecurityEvent)Context.SecurityEvents.Add(securityEvent);
            await Context.SaveChangesAsync();
            return securityEvent;
        }
    }
}
