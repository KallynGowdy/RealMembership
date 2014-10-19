namespace RealMembership
{
    using Logins;

    /// <summary>
    /// Defines a class that represents the result of creating an account with a <see cref="IEmailPasswordLogin{TAccount, TDateTime}"/>.
    /// </summary>
    /// <typeparam name="TAccount">The type of the account being used.</typeparam>
    /// <typeparam name="TDateTime">The type of the dates being used.</typeparam>
    public class EmailAccountCreationResult<TAccount> : AccountCreationResult<TAccount>
        where TAccount : UserAccount
    {
        /// <summary>
        /// Gets or sets the result of the attempt to set the login's password.
        /// </summary>
        /// <returns></returns>
        public SetPasswordResult SetPasswordResult
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the result of the attempt to set the login's email address.
        /// </summary>
        /// <returns></returns>
        public SetEmailResult SetEmailResult
        {
            get;
            set;
        }
    }
}