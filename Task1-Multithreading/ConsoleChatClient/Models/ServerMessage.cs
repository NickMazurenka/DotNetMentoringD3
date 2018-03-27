using System;

namespace ConsoleChatClient.Models
{
    public class ServerMessage
    {
        public string SenderName { get; set; }
        public bool ClientMessage { get; set; }
        public string Text { get; set; }
        public DateTime Created { get; set; }
    }
}