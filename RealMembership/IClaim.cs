using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealMembership
{
    /// <summary>
    /// Defines an interface for objects that define a claim. That is, a value that represents a property that another object might posses.
    /// For example, a user might have an 'Admin' claim, where the type of the claim is 'Role' and the value of the claim is 'Admin'.
    /// </summary>
    public interface IClaim : IEquatable<IClaim>
    {
        /// <summary>
        /// Gets or sets the type of the claim. What sort of value that the claim represents.
        /// </summary>
        /// <returns></returns>
        string Type
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the value that the claim posseses.
        /// </summary>
        /// <returns></returns>
        string Value
        {
            get;
            set;
        }
    }
}
