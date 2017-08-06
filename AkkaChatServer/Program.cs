using System;
using Akka.Actor;
using AkkaChatServer.Actors;

namespace AkkaChatServer
{
    class Program
    {
        static void Main(string[] args)
        {
            using(var system = ActorSystem.Create("AkkaChatServer"))
            {
                system.ActorOf<ChatServerActor>("ChatServer");

                Console.WriteLine("Chat server running. Press any key to exit.");
                Console.ReadLine();
            }
        }
    }
}
