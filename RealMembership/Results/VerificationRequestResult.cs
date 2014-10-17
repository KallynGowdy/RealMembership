namespace RealMembership
{
    /// <summary>
    /// Defines a class that represents the result of a request for a verification code for a login.
    /// </summary>
    public class VerificationRequestResult : ResultBase
    {
        /// <summary>
        /// Gets or sets the verification code that was issued for login verification.
        /// Null if verification was not issued.
        /// </summary>
        /// <returns></returns>
        public string Code
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the result type of the request.
        /// </summary>
        /// <returns></returns>
        public VerificationRequestResultType Result
        {
            get;
            set;
        }

    }

    /// <summary>
    /// Defines a list of values that represent the type of result from a verification request.
    /// </summary>
    public enum VerificationRequestResultType
    {
        /// <summary>
        /// Defines that a new login verification code was created and should be the code used to verify the account.
        /// </summary>
        NewCodeCreated,

        /// <summary>
        /// Defines that the login is not active and therefore cannot be verified.
        /// </summary>
        LoginNotActive,

        /// <summary>
        /// Defines that the login has already been verified and that a new code does not need to be issued..
        /// </summary>
        AlreadyVerified,

        /// <summary>
        /// Defines that the login was not found through the provided identification.
        /// </summary>
        NotFound
    }
}