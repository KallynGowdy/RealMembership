namespace RealMembership
{
    /// <summary>
    /// Defines a class that represents a simple email message.
    /// </summary>
    public class EmailMessage
    {
        /// <summary>
        /// Gets or sets the email address that the message should be sent to.
        /// </summary>
        /// <returns></returns>
        public string Recipent
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the basic subject of the email.
        /// </summary>
        /// <returns></returns>
        public string Subject
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the HTML body of the message.
        /// </summary>
        /// <returns></returns>
        public string Html
        {
            get;
            set;
        }
    }
}