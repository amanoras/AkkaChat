using System;
namespace AkkaChatMessages
{
    public class SayRequest
    {
		public string Username { get; set; }
		public string Text { get; set; }

        public SayRequest(string text)
        {
            Text = text;
        }
    }
}
