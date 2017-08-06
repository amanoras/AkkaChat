using System;
using System.Collections.Generic;
using Akka.Actor;
using AkkaChatMessages;

namespace AkkaChatServer.Actors
{
    public class ChatServerActor : ReceiveActor
    {
        private readonly HashSet<IActorRef> _clients = new HashSet<IActorRef>();

		public ChatServerActor()
        {
            Receive<ConnectRequest>(msg => HandleConnectRequest(msg));
            Receive<NickRequest>(msg => HandleNickRequest(msg));
            Receive<SayRequest>(msg => HandleSayRequest(msg));
            Receive<ChannelsRequest>(msg => HandleChannelsRequest(msg));
            Receive<DisconnectRequest>(msg => HandleDisconnectRequest(msg));
        }

        public void HandleConnectRequest(ConnectRequest msg)
        {
            var response = new ConnectResponse
            {
                Message = string.Format("{0} has joined the conversation", msg.Username)
            };

            foreach(var client in _clients)
            {
                client.Tell(response);    
            }

			_clients.Add(this.Sender);
			Sender.Tell(new ConnectResponse
			{
				Message = "Hello and welcome to Akka.NET Chat",
			}, Self);
        }

		public void HandleNickRequest(NickRequest msg)
		{
			var response = new NickResponse
			{
				OldUsername = msg.OldUsername,
				NewUsername = msg.NewUsername,
			};

            foreach (var client in _clients)
            {
                client.Tell(response, Self);
            }
		}

		public void HandleSayRequest(SayRequest msg)
		{
			var response = new SayResponse
			{
				Username = msg.Username,
				Text = msg.Text,
			};

            foreach (var client in _clients)
            {
                client.Tell(response, Self);
            }
		}

		public void HandleChannelsRequest(ChannelsRequest msg)
		{

		}

        public void HandleDisconnectRequest(DisconnectRequest msg)
		{
            _clients.Remove(Sender);
            var response = new DisconnectResponse
            {
                Username = msg.Username
            };

            foreach(var client in _clients)
            {
                client.Tell(response, Self);
            }
		}
    }
}
