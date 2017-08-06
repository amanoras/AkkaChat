using System;

namespace AkkaChatMessages
{
    public class ConnectRequest
    {
        public string Username { get; set; }

        public ConnectRequest(string username)
        {
            Username = username;
        }
    }
}
