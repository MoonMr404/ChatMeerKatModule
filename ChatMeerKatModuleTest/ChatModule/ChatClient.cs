using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class ChatClient
{
    private TcpClient client;
    private NetworkStream stream;
    private string username;

    // Costruttore che inizializza il client e la connessione al server
    public ChatClient(string serverAddress, int port, string username)
    {
        client = new TcpClient(serverAddress, port);
        stream = client.GetStream();
        this.username = username;

        // Avvia un thread per ricevere i messaggi dal server
        Thread receiveThread = new Thread(ReceiveMessages);
        receiveThread.Start();
    }

    // Metodo per inviare i messaggi al server
    public void SendMessage(string message)
    {
        string fullMessage = $"{username}: {message}";
        byte[] data = Encoding.UTF8.GetBytes(fullMessage);
        stream.Write(data, 0, data.Length);
    }

    // Metodo per ricevere i messaggi dal server
    private void ReceiveMessages()
    {
        byte[] buffer = new byte[1024];
        int byteCount;
        while ((byteCount = stream.Read(buffer, 0, buffer.Length)) > 0)
        {
            string message = Encoding.UTF8.GetString(buffer, 0, byteCount);
            Console.WriteLine(message); // Stampa i messaggi ricevuti sulla console
        }
    }

    // Metodo per chiudere la connessione al server
    public void CloseConnection()
    {
        stream.Close();
        client.Close();
    }
}