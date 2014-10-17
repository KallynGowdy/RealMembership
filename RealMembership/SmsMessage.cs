namespace RealMembership
{
    /// <summary>
    /// Defines a class that represents a simple SMS text message.
    /// </summary>
    public class SmsMessage
    {
        /// <summary>
        /// Gets or sets the phone number that the message should be sent to.
        /// </summary>
        /// <returns></returns>
        public string PhoneNumber
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the message body that should be sent to the phone number.
        /// </summary>
        /// <returns></returns>
        public string Body
        {
            get;
            set;
        }
    }
}