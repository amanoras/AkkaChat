using System;
using Akka.Actor;
using AkkaChatMessages;

namespace AkkaChatClient.Actors
{
    public class ChatClientActor : ReceiveActor
    {
		private string _nick = "Anto";
		private readonly ActorSelection _server = Context.ActorSelection("akka.tcp://AkkaChatServer@localhost:8081/user/ChatServer");
        private readonly IActorRef _inputProcessor;

        public ChatClientActor()
        {
            _inputProcessor = Context.ActorOf<InputProcessorActor>("InputProcessor");

            Receive<NewInputMessage>(msg => HandleNewInput(msg));
            Receive<ConnectRequest>(msg => HandleConnectionRequest(msg));
            Receive<ConnectResponse>(msg => HandleConnectionResponse(msg));
            Receive<NickRequest>(msg => HandleNickRequest(msg));
            Receive<NickResponse>(msg => HandleNickResponse(msg));
            Receive<SayRequest>(msg => HandleSayRequest(msg));
            Receive<SayResponse>(msg => HandleSayResponse(msg));
            Receive<DisconnectRequest>(msg => HandleDisconnectRequest(msg));
            Receive<DisconnectResponse>(msg => HandleDisconnectResponse(msg));
            Receive<Terminated>(msg => Console.Write("Server died"));
		}

        private void HandleConnectionRequest(ConnectRequest msg)
        {
			Console.WriteLine("Connecting....");
			_server.Tell(msg);
        }

        private void HandleConnectionResponse(ConnectResponse msg)
        {
			Console.WriteLine("Connected!");
			Console.WriteLine(msg.Message);
        }

        private void HandleNickRequest(NickRequest msg)
        {
			msg.OldUsername = this._nick;
			Console.WriteLine("Changing nick to {0}", msg.NewUsername);
			this._nick = msg.NewUsername;
			_server.Tell(msg);
        }

        private void HandleNickResponse(NickResponse msg)
        {
            Console.WriteLine("{0} is now known as {1}", msg.OldUsername, msg.NewUsername);
        }

        private void HandleSayRequest(SayRequest msg)
        {
			msg.Username = this._nick;
			_server.Tell(msg);
        }

        private void HandleSayResponse(SayResponse msg)
        {
            Console.WriteLine("{0}: {1}", msg.Username, msg.Text);
        }

        private void HandleDisconnectRequest(DisconnectRequest msg)
        {
			Console.WriteLine("Exiting...");
            msg.Username = this._nick;
            _server.Tell(msg);
            Context.System.Terminate();
		}

        private void HandleDisconnectResponse(DisconnectResponse msg)
        {
            Console.WriteLine("{0}: Has left the conversation.", msg.Username);
        }

        private void HandleNewInput(NewInputMessage msg)
        {
            _inputProcessor.Tell(msg);
        }
    }
}
