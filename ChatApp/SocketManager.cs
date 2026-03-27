namespace ChatApp;
using SocketIOClient;

public class SocketManager
{
    private static SocketIO _client;
    private static readonly string Path = "/sys25d";

    public static async Task Connect(string username)
    {
        _client = new SocketIO(new Uri("wss://api.leetcode.se"), new SocketIOOptions
        {
            Path = Path
        });

        _client.On("message", async response =>
        {
            string message = response.GetValue<string>(0);
            Console.WriteLine(message);
            
            await Task.CompletedTask;
        });
        
        _client.On("join", async response =>
        {
            string user = response.GetValue<string>(0);
            Console.WriteLine($"{user} joined the chat! (type exit to leave)");
            
            await Task.CompletedTask;
        });
        
        _client.On("leave", async response =>
        {
            string user = response.GetValue<string>(0);
            Console.WriteLine($"{user} left the chat");

            await Task.CompletedTask;
        });

        _client.OnConnected += (sender, args) =>
        {
            Console.WriteLine($"{username} has joined the chat!");

            _client.EmitAsync("join", new object[] { username });
        };

        _client.OnDisconnected += (sender, args) =>
        {
            Console.WriteLine($"{username} has disconnected from the chat!");
        };

        await _client.ConnectAsync();

        await Task.Delay(2000);
    }

    public static async Task SendMessage(Message message)
    {
        await _client.EmitAsync("message", new object[] { message });
    }

    public static async Task Disconnect()
    {
        Console.Clear();
        await _client.DisconnectAsync();
    }
}