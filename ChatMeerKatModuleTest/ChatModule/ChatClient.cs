using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class ChatClient
{
    private string username;
    private TcpClient client;
    private NetworkStream stream;

    public ChatClient(string username)
    {
        this.username = username;
    }

    public void Connect(string serverAddress, int port)
    {
        try
        {
            client = new TcpClient(serverAddress, port);
            stream = client.GetStream();

            Console.WriteLine($"Connesso al server {serverAddress}:{port}");

            // Invia il nome utente al server
            SendMessage(username);

            // Avvia un thread per ricevere i messaggi
            Thread receiveThread = new Thread(ReceiveMessages);
            receiveThread.Start();

            // Gestisce l'invio di messaggi
            SendMessages();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Errore durante la connessione: {ex.Message}");
        }
    }

    private void SendMessages()
    {
        Console.WriteLine("Puoi iniziare a scrivere messaggi. Digita 'exit' per uscire.");
        while (true)
        {
            string message = Console.ReadLine();

            if (message?.ToLower() == "exit")
            {
                CloseConnection();
                break;
            }

            SendMessage(message);
        }
    }

    private void SendMessage(string message)
    {
        try
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            stream.Write(data, 0, data.Length);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Errore durante l'invio del messaggio: {ex.Message}");
        }
    }

    private void ReceiveMessages()
    {
        try
        {
            while (true)
            {
                byte[] buffer = new byte[1024];
                int byteCount = stream.Read(buffer, 0, buffer.Length);
                string message = Encoding.UTF8.GetString(buffer, 0, byteCount);

                Console.WriteLine(message);
            }
        }
        catch (Exception)
        {
            Console.WriteLine("Connessione chiusa dal server.");
            CloseConnection();
        }
    }

    private void CloseConnection()
    {
        stream?.Close();
        client?.Close();
        Console.WriteLine("Connessione chiusa.");
        Environment.Exit(0);
    }

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
