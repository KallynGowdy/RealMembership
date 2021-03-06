﻿//   Copyright 2014 Kallyn Gowdy
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealMembership
{
    /// <summary>
    /// Defines an interface for an object that can send emails.
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Sends an email to the given recipiant with the given subject and html.
        /// </summary>
        /// <param name="recipiant">The address that the email should be sent to.</param>
        /// <param name="subject">The subject of the email.</param>
        /// <param name="html">The HTML body of the email.</param>
        void SendEmail(string recipiant, string subject, string html);
    }
}
