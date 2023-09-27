// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Runtime.Serialization;
using Microsoft.Coyote.Actors;

namespace Microsoft.Coyote.Samples.CloudMessaging.Events
{
    /// <summary>
    /// Used to issue a client request.
    /// </summary>
    [DataContract]
    public class ClientRequestEvent : Event
    {
        [DataMember]
        public readonly string Command;

        public ClientRequestEvent(string command)
        {
            this.Command = command;
        }
    }
}
