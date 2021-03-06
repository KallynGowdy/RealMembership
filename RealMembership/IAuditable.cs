//   Copyright 2014 Kallyn Gowdy
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.

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