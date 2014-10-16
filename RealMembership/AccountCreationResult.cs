namespace RealMembership
{
    /// <summary>
    /// Defines a class that represents the result of the account creation process.
    /// </summary>
    public sealed class AccountCreationResult : IResult
    {
        /// <summary>
        /// Gets or sets whether the creation was successful.
        /// </summary>
        /// <returns></returns>
        public bool Successful
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the type that defines why the result was successful or not.
        /// </summary>
        /// <returns></returns>
        public AccountCreationResultType Result
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the message that describes the result.
        /// </summary>
        /// <returns></returns>
        public string Message
        {
            get;
            set;
        }
    }

    /// <summary>
    /// Defines a list of values that represent the type of result that came of the account creation process.
    /// </summary>
    public enum AccountCreationResultType
    {
        /// <summary>
        /// Defines that the account was created successfully and the verification code was sent.
        /// </summary>
        CreatedAndSentCode,

        /// <summary>
        /// Defines that the account was created, but the verification code was not sent.
        /// </summary>
        CreatedButCodeNotSent,
        
        /// <summary>
        /// Defines that the given password was invalid.
        /// </summary>
        InvalidPassword,

        /// <summary>
        /// Defines that the given username was invalid.
        /// </summary>
        InvalidUsername,

        /// <summary>
        /// Defines that the given email was invalid.
        /// </summary>
        InvalidEmail
    }
}