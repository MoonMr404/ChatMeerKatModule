using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ChatModule;

public class ChatServer
{
    private TcpListener listener;
    private Chat chat;

    public ChatServer(string ipAddress, int port)
    {
        listener = new TcpListener(IPAddress.Parse(ipAddress), port);
        chat = new Chat();
    }

    public void Start()
    {
        listener.Start();
        Console.WriteLine("Server avviato...");
        while (true)
        {
            TcpClient client = listener.AcceptTcpClient();
            Thread clientThread = new Thread(HandleClient);
            clientThread.Start(client);
        }
    }

    private void HandleClient(object clientObj)
    {
        TcpClient client = (TcpClient)clientObj;

        // Ricezione del nome utente
        NetworkStream stream = client.GetStream();
        byte[] buffer = new byte[1024];
        int byteCount = stream.Read(buffer, 0, buffer.Length);
        string username = Encoding.UTF8.GetString(buffer, 0, byteCount).Trim();

        // Creazione e inizializzazione dell'utente
        User user = new User(username);
        user.InitUserNetworkStream(client);
        chat.AddUser(user);

        Console.WriteLine($"{username} si è connesso.");
        string welcomeMessage = $"Benvenuto {username} nella chat!";
        user.SendMessage(welcomeMessage);

        while (true)
        {
            try
            {
                // Ricezione del messaggio
                string messageContent = user.ReceiveMessage().Trim();
                if (string.IsNullOrEmpty(messageContent)) break;

                // Aggiunta del messaggio alla chat
                chat.AddMessage(new Message(user, messageContent));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{username} si è disconnesso. Errore: {ex.Message}");
                break;
            }
        }

        // Disconnessione dell'utente
        user.Disconnect();
        chat.DisplayChatHistory();
    }
}