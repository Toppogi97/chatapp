namespace ChatApp;

public class Message : User
{
    public string Text { get; set; }
    public DateTime Time { get; set; } = DateTime.UtcNow;
    
    public Message(string username, string text, DateTime time) : base(username)
    {
        Text = text;
        Time = time;
    }
}