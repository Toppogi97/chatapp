namespace ChatApp;

class Program
{
    static async Task Main(string[] args)
    {
        string username = GetUsername();
        
        Console.WriteLine("Connecting...");
        await SocketManager.Connect(username);

        while (true)
        {
            string text = Console.ReadLine();

            if (text.ToLower() == "exit")
                break;
            
            var message = new Message(username, text, DateTime.UtcNow);
            
            await SocketManager.SendMessage(message);
        }
        
        await SocketManager.Disconnect();
    }
    
    static string GetUsername()
    {
        Console.WriteLine("Enter username: ");
        string username = Console.ReadLine();

        if (!string.IsNullOrWhiteSpace(username))
        {
            return username;
        }
        else
        {
            Console.WriteLine("Username cannot be empty!");
            return GetUsername();
        }
    }
}