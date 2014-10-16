using System;

namespace RealMembership
{
    /// <summary>
    /// Defines a class that represents the result of authenticating a user.
    /// </summary>
    public sealed class AuthenticationResult : IResult
    {
        public static AuthenticationResult NotFound
        {
            get
            {
                return new AuthenticationResult
                {
                    Successful = false,
                    Result = AuthenticationResultType.NotFound
                };
            }
        }

        public static AuthenticationResult ValidCredentialProvided
        {
            get
            {
                return new AuthenticationResult
                {
                    Successful = false,
                    Result = AuthenticationResultType.ValidCredentialsProvided
                };
            }
        }

        public static AuthenticationResult InvalidCredentialsProvided
        {
            get
            {
                return new AuthenticationResult
                {
                    Successful = false,
                    Result = AuthenticationResultType.InvalidCredentialsProvided
                };
            }
        }

        public static AuthenticationResult IncorrectAuthenticationType
        {
            get
            {
                return new AuthenticationResult
                {
                    Successful = false,
                    Result = AuthenticationResultType.IncorrectAuthenticationType
                };
            }
        }

        public static AuthenticationResult GoodButRequiresTwoFactor
        {
            get
            {
                return new AuthenticationResult
                {
                    Successful = true,
                    Result = AuthenticationResultType.GoodButRequiresTwoFactor
                };
            }
        }

        public static AuthenticationResult InvalidAndRequiresTwoFactor
        {
            get
            {
                return new AuthenticationResult
                {
                    Successful = false,
                    Result = AuthenticationResultType.InvalidAndRequiresTwoFactor
                };
            }
        }

        public static AuthenticationResult AccountNotActive
        {
            get
            {
                return new AuthenticationResult
                {
                    Successful = false,
                    Result = AuthenticationResultType.AccountNotActive
                };
            }
        }

        public static AuthenticationResult LoginNotVerified
        {
            get
            {
                return new AuthenticationResult
                {
                    Successful = false,
                    Result = AuthenticationResultType.LoginNotVerified
                };
            }
        }

        public static AuthenticationResult AccountLockedOut
        {
            get
            {
                return new AuthenticationResult
                {
                    Successful = false,
                    Result = AuthenticationResultType.AccountLockedOut
                };
            }
        }

        /// <summary>
        /// Gets or sets the message that describes the result.
        /// </summary>
        public string Message
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets whether the operation was successful.
        /// </summary>
        public bool Successful
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the value that describes the type of the authentication result.
        /// </summary>
        /// <returns></returns>
        public AuthenticationResultType Result
        {
            get;
            set;
        }
    }

    /// <summary>
    /// Defines a list of values that describe the result of an authentication attempt.
    /// </summary>
    public enum AuthenticationResultType
    {
        /// <summary>
        /// Defines that the authentication attempt was successful because valid credentials were provided.
        /// </summary>
        ValidCredentialsProvided,

        /// <summary>
        /// Defines that the login was not found through the provided identification.
        /// </summary>
        NotFound,

        /// <summary>
        /// Defines that the authentication attempt was unsucessful because invalid credentials were provided.
        /// </summary>
        InvalidCredentialsProvided,

        /// <summary>
        /// Defines that the authentication attempt was unsucessful because the credentials provided cannot be used to authenticate against the login. (i.e. trying to use a password on a code)
        /// </summary>
        IncorrectAuthenticationType,

        /// <summary>
        /// Defines that the given credentials were valid, but that a second factor is required before being completely authenticated.
        /// </summary>
        GoodButRequiresTwoFactor,

        /// <summary>
        /// Defines that the given credentials were invalid because they were not valid for use in two factor authentication.
        /// </summary>
        InvalidAndRequiresTwoFactor,

        /// <summary>
        /// Defines that the account is not active, meaning that is has either been closed or not been activated by an admin yet.
        /// </summary>
        AccountNotActive,

        /// <summary>
        /// Defines that the login has not been verified yet.
        /// </summary>
        LoginNotVerified,

        /// <summary>
        /// Defines that the account is locked due to too many incorrect login attempts in a row.
        /// </summary>
        AccountLockedOut
    }
}