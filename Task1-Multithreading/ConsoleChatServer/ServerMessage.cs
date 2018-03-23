using System;

public class ServerMessage
{
    public ServerMessage() {}

    public ServerMessage(string text, string senderName = "Server", DateTime? created = null) {
        Text = text;
        SenderName = senderName;
        Created = created.HasValue ? created.Value : DateTime.Now;
    }

    public string SenderName { get; set; }
    public bool ClientMessage { get; set; }
    public string Text { get; set; }
    public DateTime Created { get; set; }
}