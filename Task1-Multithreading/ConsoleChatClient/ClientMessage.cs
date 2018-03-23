using System;

public class ClientMessage
{
    public ClientMessage() { }
    public ClientMessage(string text, DateTime? created = null)
    {
        Text = text;
        Created = created.HasValue ? created.Value : DateTime.Now;
    }
    public string Text { get; set; }
    public DateTime Created { get; set; }
}