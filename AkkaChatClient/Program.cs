using System;
using System.Linq;
using Akka.Actor;
using AkkaChatMessages;
using AkkaChatClient.Actors;

namespace AkkaChatClient
{
    class Program
    {
        static void Main(string[] args)
        {
            using(var system = ActorSystem.Create("AkkaChatClient"))
            {
                var chatClient = system.ActorOf(Props.Create<ChatClientActor>(), "Client");
                chatClient.Tell(new ConnectRequest("Anto"));
                var systemTerminated = false;
                system.RegisterOnTermination(() => systemTerminated = true);

                while(!systemTerminated)
                {
                    var input = Console.ReadLine();
                    chatClient.Tell(new NewInputMessage
                    {
                        Input = input    
                    });
                }

                //system.Terminate().Wait();
			}
        }
    }
}
