using System;
namespace AkkaChatMessages
{
    public class NickRequest
    {
		public string OldUsername { get; set; }
		public string NewUsername { get; set; }

        public NickRequest(string newUsername)
        {
            NewUsername = newUsername;
        }
    }
}
