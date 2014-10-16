namespace RealMembership
{
    /// <summary>
    /// Defines an interface for objects that contain information about themselves.
    /// </summary>
    public interface IAuditable<TDate> where TDate : struct
    {
        /// <summary>
        /// Gets or sets the time that this object was created at.
        /// </summary>
        /// <returns></returns>
        TDate CreationTime
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the time that this object was last updated on if it has been updated.
        /// </summary>
        /// <returns></returns>
        TDate? TimeLastUpdated
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the time that this object was deleted on if it has been deleted.
        /// </summary>
        /// <returns></returns>
        TDate? DeletionTime
        {
            get;
            set;
        }
    }
}