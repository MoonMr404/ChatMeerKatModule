using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ChatModule
{
    public class ChatClient
    {
        private TcpClient tcpClient;
        private NetworkStream networkStream;
        private string username;

        public ChatClient(string ipAddress, int port, string username)
        {
            this.username = username;
            tcpClient = new TcpClient(ipAddress, port);
            networkStream = tcpClient.GetStream();
        }

        // Metodo per inviare il nome utente al server
        public void SendUsername()
        {
            byte[] data = Encoding.UTF8.GetBytes(username);
            networkStream.Write(data, 0, data.Length);
        }

        // Metodo per inviare un messaggio al server
        public void SendMessage(string message)
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            networkStream.Write(data, 0, data.Length);
        }

        // Metodo per ricevere i messaggi dal server
        public void ReceiveMessages()
        {
            byte[] buffer = new byte[1024];

            while (true)
            {
                try
                {
                    int byteCount = networkStream.Read(buffer, 0, buffer.Length);
                    if (byteCount == 0)
                        break;

                    string message = Encoding.UTF8.GetString(buffer, 0, byteCount);
                    Console.WriteLine(message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Errore nella ricezione del messaggio: {ex.Message}");
                    break;
                }
            }
        }

        // Metodo per chiudere la connessione
        public void Disconnect()
        {
            networkStream.Close();
            tcpClient.Close();
        }

        // Metodo per avviare il client
        public void Start()
        {
            // Invio del nome utente al server
            SendUsername();

            // Avvio del thread per ricevere i messaggi
            Thread receiveThread = new Thread(ReceiveMessages);
            receiveThread.Start();

            // Invio dei messaggi dal client
            while (true)
            {
                string message = Console.ReadLine();
                if (message.ToLower() == "exit")
                {
                    Disconnect();
                    break;
                }

                SendMessage(message);
            }
        }
    }
}