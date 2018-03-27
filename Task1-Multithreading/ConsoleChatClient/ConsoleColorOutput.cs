using System;
using ConsoleChatClient.Models;

public class ConsoleChatOutput
{
    public void WriteMessage(ServerMessage message)
    {
        var previousColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Write(message.Created.ToString("yy/MM/dd HHmmss "));
        if (message.SenderName == "Server")
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
        }
        else if (message.ClientMessage)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
        }
        Console.Write(message.SenderName + " ");
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine(message.Text);
        Console.ForegroundColor = previousColor;
    }
}