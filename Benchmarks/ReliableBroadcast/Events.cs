// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

// Coyote benchmark: Reliable Broadcast

using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Coyote.Actors;

namespace ReliableBroadcast
{
    [DataContract]
    public class RegisterProcessEvent : Event
    {
        /// <summary>
        /// The server id that is being registered.
        /// </summary>
        [DataMember]
        public ActorId ProcessId;
    }

    [DataContract]
    public class BroadcastRequestEvent : Event
    {
        /// <summary>
        /// The server id that is being registered.
        /// </summary>
        [DataMember]
        public string Message;
    }

    [DataContract]
    public class MessageEvent : Event
    {
        [DataMember]
        public int MessageId;

        [DataMember]
        public ActorId Sender;

        [DataMember]
        public ActorId Receiver;

        [DataMember]
        public string Message;

        public MessageEvent(int messageId, ActorId sender, string message, ActorId receiver)
        {
            this.MessageId = messageId;
            this.Sender = sender;
            this.Message = message;
            this.Receiver = receiver;
        }
    }

    [DataContract]
    public class RbBroadcastEvent : Event
    {
        [DataMember]
        public MessageEvent MsgEvent;

        public RbBroadcastEvent(MessageEvent msgEvent)
        {
            this.MsgEvent = msgEvent;
        }
    }

    [DataContract]
    public class SingleMessageEvent : Event
    {
        [DataMember]
        public MessageEvent MsgEvent;

        public SingleMessageEvent(MessageEvent msgEvent)
        {
            this.MsgEvent = msgEvent;
        }
    }

    [DataContract]
    public class GetMessagesEvent : Event
    {
        [DataMember]
        public ActorId Sender;

        public GetMessagesEvent(ActorId sender)
        {
            this.Sender = sender;
        }
    }

    public class GetMessagesResponseEvent : Event
    {
        [DataMember]
        public List<string> Messages;

        public GetMessagesResponseEvent(List<string> messages)
        {
            this.Messages = messages;
        }
    }

    public class CrashEvent : Event { }
}
