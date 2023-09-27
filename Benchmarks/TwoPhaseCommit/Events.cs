// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

// Coyote benchmark: TwoPhase Commit

using System.Runtime.Serialization;
using Microsoft.Coyote.Actors;

namespace TwoPhaseCommit
{
    /// <summary>
    /// Notifies a server that it has joined the Raft
    /// service and can start executing.
    /// </summary>
    [DataContract]
    public class RegisterServerEvent : Event
    {
        /// <summary>
        /// The server id that is being registered.
        /// </summary>
        public ActorId ServerId;
    }

    /// <summary>
    /// Used to issue a client request.
    /// </summary>
    [DataContract]
    public class ClientRequestEvent : Event
    {
        [DataMember]
        public readonly ActorId Sender;

        [DataMember]
        public readonly string Command;

        public ClientRequestEvent(ActorId sender, string command)
        {
            this.Sender = sender;
            this.Command = command;
        }
    }

    /// <summary>
    /// Used to issue a client response.
    /// </summary>
    [DataContract]
    public class ClientResponseEvent : Event
    {
        [DataMember]
        public readonly bool IsSuccessful;

        public ClientResponseEvent(bool isSuccessful)
        {
            this.IsSuccessful = isSuccessful;
        }
    }

    internal class GlobalAbortEvent : Event { }

    internal class GlobalCommitEvent : Event { }

    internal class AbortEvent : Event
    {
        [DataMember]
        public readonly ActorId Sender;

        /// <summary>
        /// The reason for voting abort.
        /// </summary>
        [DataMember]
        public readonly string Reason;

        public AbortEvent(ActorId sender, string reason)
        {
            this.Sender = sender;
            this.Reason = reason;
        }
    }

    internal class PreparedEvent : Event
    {
        /// <summary>
        /// The coordinator/sender of the event
        /// </summary>
        [DataMember]
        public readonly ActorId Sender;

        public PreparedEvent(ActorId sender)
        {
            this.Sender = sender;
        }
    }

    internal class RequestEvent : Event
    {
        /// <summary>
        /// The coordinator/sender of the event
        /// </summary>
        [DataMember]
        public readonly ActorId Sender;

        public RequestEvent(ActorId sender)
        {
            this.Sender = sender;
        }
    }
}
