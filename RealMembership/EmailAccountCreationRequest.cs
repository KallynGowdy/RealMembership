namespace RealMembership
{
    /// <summary>
    /// Defines a class that represents a request for an account to be created with the given email address.
    /// </summary>
    public class EmailAccountCreationRequest : AccountCreationRequest
    {
        /// <summary>
        /// Gets or sets the Email address that the created account should have.
        /// </summary>
        /// <returns></returns>
        public string Email
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the password that the created account should have.
        /// </summary>
        /// <returns></returns>
        public string Password
        {
            get;
            set;
        }
    }
}