using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealMembership
{
    /// <summary>
    /// Defines a base class for <see cref="IResult"/> objects.
    /// </summary>
    public abstract class ResultBase : IResult
    {
        /// <summary>
        /// Gets or sets the ID of the result.
        /// </summary>
        /// <returns></returns>
        public virtual long Id
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the message that describes the result.
        /// </summary>
        public virtual string Message { get; set; }

        /// <summary>
        /// Gets or sets whether the operation was successful.
        /// </summary>
        public virtual bool Successful { get; set; }
    }
}
