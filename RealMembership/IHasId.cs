using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealMembership
{
    /// <summary>
    /// Defines an interface for objects that have an indentifier.
    /// </summary>
    public interface IHasId
    {
        /// <summary>
        /// Gets or sets the ID of the object.
        /// </summary>
        /// <returns></returns>
        long Id
        {
            get;
            set;
        }
    }
}
