

namespace RealMembership
{
    /// <summary>
    /// Defines a class that represents the result of verifiying a login.
    /// </summary>
    public class VerificationResult : ResultBase
    {
        /// <summary>
        /// Gets or sets the type that represents the specific result of the verification.
        /// </summary>
        /// <returns></returns>
        public VerificationResultType Result
        {
            get;
            set;
        }

    }

    /// <summary>
    /// Defines a list of values that represent the result of a login verification attempt.
    /// </summary>
    public enum VerificationResultType
    {
        /// <summary>
        /// Defines that the given code was valid and that the login is now verified.
        /// </summary>
        LoginVerified,

        /// <summary>
        /// Defines that the given validation code was not found in the backing database.
        /// </summary>
        CodeNotFound,

        /// <summary>
        /// Defines that the login is not active and therefore cannot be verified at this moment.
        /// </summary>
        LoginNotActive,

        /// <summary>
        /// Defines that the provided validation code was invalid.
        /// </summary>
        InvalidCode,

        /// <summary>
        /// Defines that the login has already been verified.
        /// </summary>
        AlreadyVerified
    }
}