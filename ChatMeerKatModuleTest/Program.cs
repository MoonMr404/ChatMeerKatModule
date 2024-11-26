using System;

namespace ChatModule
{
    class Program
    {
        static void Main(string[] args)
        {
            // Indirizzo IP e porta per il server
            string ipAddress = "127.0.0.1"; // Usa l'indirizzo IP locale per il server
            int port = 5000; // Porta di ascolto per il server

            // Creazione e avvio del server
            ChatServer chatServer = new ChatServer(ipAddress, port);
            chatServer.Start();
        }
    }
}