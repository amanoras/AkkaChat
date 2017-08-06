using System;
using System.Linq;
using Akka.Actor;
using AkkaChatMessages;

namespace AkkaChatClient.Actors
{
    public class InputProcessorActor : ReceiveActor
    {
        public InputProcessorActor()
        {
            Receive<NewInputMessage>(msg => HandleNewInput(msg));
        }

        private void HandleNewInput(NewInputMessage msg)
        {
            var input = msg.Input;
			if (input.StartsWith("/", StringComparison.InvariantCultureIgnoreCase))
			{
				var parts = input.Split(" ");
				var cmd = parts[0].ToLowerInvariant();
				var rest = string.Join(" ", parts.Skip(1));

				if (cmd == "/nick")
				{
					Sender.Tell(new NickRequest(rest));
				}

				if (cmd == "/exit")
				{
					Sender.Tell(new DisconnectRequest());
				}
			}
			else
			{
				Sender.Tell(new SayRequest(input));
			}
        }
    }
}
