using System;
using Akka.Actor;

namespace AkkaChatMessages
{
    public class ChannelsResponse
    {
        public IActorRef[] channels { get; set; }

        public ChannelsResponse()
        {
        }
    }
}
