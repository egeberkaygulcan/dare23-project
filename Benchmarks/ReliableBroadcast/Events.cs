using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Coyote.Actors;

namespace ReliableBroadcast;
{
    [DataContract]
    public class RegisterProcessEvent : Event
    {
        /// <summary>
        /// The server id that is being registered.
        /// </summary>
        public ActorId ProcessId;
    }

    [DataContract]
    public class MessageEvent : Event 
    {
        [DataMember]
        public ActorId Sender;

        [DataMember]
        public string Message;

        public MessageEvent(ActorId sender, string message)
        {
            this.Sender = sender;
            this.Message = message;
        }
    }


}