namespace RealMembership
{
    /// <summary>
    /// Defines an interface for objects that represent the result of an operation.
    /// </summary>
    public interface IResult
    {
        /// <summary>
        /// Gets the message that describes the result.
        /// </summary>
        /// <returns></returns>
        string Message { get; }

        /// <summary>
        /// Gets whether the operation was successful.
        /// </summary>
        /// <returns></returns>
        bool Successful { get; }
    }
}