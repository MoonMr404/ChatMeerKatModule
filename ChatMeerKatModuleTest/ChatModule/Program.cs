namespace ChatModule;

public class Program
{
    public static void Main(string[] args)
    {
        Console.Write("Inserisci il tuo nome utente: ");
        string username = Console.ReadLine();

        Console.Write("Inserisci l'indirizzo del server (default: 127.0.0.1): ");
        string serverAddress = Console.ReadLine();
        if (string.IsNullOrEmpty(serverAddress))
        {
            serverAddress = "127.0.0.1";
        }

        Console.Write("Inserisci la porta del server (default: 8888): ");
        string portInput = Console.ReadLine();
        int port = string.IsNullOrEmpty(portInput) ? 8888 : int.Parse(portInput);

        ChatClient client = new ChatClient(username);
        client.Connect(serverAddress, port);
    }
}