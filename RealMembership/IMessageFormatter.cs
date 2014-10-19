using RealMembership.Logins;
using System.Threading.Tasks;

namespace RealMembership
{
    /// <summary>
    /// Defines an interface for an object that can format messages that should be sent to a user.
    /// </summary>
    public interface IMessageFormatter<TAccount, TDateTime>
        where TAccount : IUserAccount<TAccount, TDateTime>
        where TDateTime : struct
    {
        /// <summary>
        /// Formats the given password reset code into a 'confirm your login' message intended for the given login.
        /// </summary>
        /// <param name="verificationCode">The verification code that should be included in the message.</param>
        /// <param name="login">The login that the message will be sent to.</param>
        /// <returns>Returns a string representing the message that should be sent.</returns>
        Task<string> FormatVerifyLoginMessageAsync(string verificationCode, ILogin<TAccount, TDateTime> login);

        /// <summary>
        /// Gets the subject for the 'confirm your login' message intended for the given login.
        /// </summary>
        /// <param name="login">The login that the message will be sent to.</param>
        /// <returns>Returns a string representing the subject that should be sent for the message.</returns>
        Task<string> FormatVerifyLoginSubjectAsync(ILogin<TAccount, TDateTime> login);

        /// <summary>
        /// Gets the 'your login was verified' message intended for the given login.
        /// </summary>
        /// <param name="login">The login that the message will be sent to.</param>
        /// <returns>Returns a string representing the message that should be sent.</returns>
        Task<string> FormatVerifiedMessageAsync(ILogin<TAccount, TDateTime> login);

        /// <summary>
        /// Gets the subject for the 'your login was verified' message intended for the given login.
        /// </summary>
        /// <param name="login">The login that the message will be sent to.</param>
        /// <returns>Returns a string representing the message that should be sent.</returns>
        Task<string> FormatVerifiedSubjectAsync(ILogin<TAccount, TDateTime> login);

        /// <summary>
        /// Formats the given password reset code into a 'complete password reset' message intended for the given login.
        /// Can return HTML if the given login is a <see cref="IEmailPasswordLogin{TAccount, TDateTime}"/>.
        /// </summary>
        /// <param name="resetCode">The reset code that should be included in the message.</param>
        /// <param name="login">The login that the message will be sent to.</param>
        /// <returns>Returns a string representing the message that should be sent.</returns>
        Task<string> FormatPasswordResetMessageAsync(string resetCode, IPasswordLogin<TAccount, TDateTime> login);

        /// <summary>
        /// Gets the subject for the 'complete password reset' message intended for the given login.
        /// </summary>
        /// <param name="login">The login that the message will be sent to.</param>
        /// <returns>Returns a string representing the subject that should be sent for the message.</returns>
        Task<string> FormatPasswordResetSubjectAsync(IPasswordLogin<TAccount, TDateTime> login);

        /// <summary>
        /// Gets the 'your password was changed' message for the given login.
        /// </summary>
        /// <param name="login">The login that the message will be sent to.</param>
        /// <returns>Returns a string representing the message that should be sent.</returns>
        Task<string> FormatPasswordChangedMessageAsync(ILogin<TAccount, TDateTime> login);

        /// <summary>
        /// Gets the subject for the 'your password was changed' message for the given login.
        /// </summary>
        /// <param name="login">The login that the message will be sent to.</param>
        /// <returns>Returns a string representing the message that should be sent.</returns>
        Task<string> FormatPasswordChangedSubjectAsync(ILogin<TAccount, TDateTime> login);
    }
}