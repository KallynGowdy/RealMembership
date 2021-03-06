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
    /// Defines an interface for an object that can send SMS texts to a phone number.
    /// </summary>
    public interface ISmsService
    {
        /// <summary>
        /// Sends the given string of text to the given phone number over SMS.
        /// </summary>
        /// <param name="phoneNumber">The phone number that the SMS should be sent to.</param>
        /// <param name="text">The text that should be sent to the phone number.</param>
        void SendSms(string phoneNumber, string text);
    }
}
