using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ChatModule;

public class User
{
    private string username { get; set; }
    private string userID { get; set; }
    
    private TcpClient _tcpClient;
    private NetworkStream _networkStream;
    
    //Basic Methods
    public User(string Username)
    {
        this.username = Username;
        this.userID = UserID;
    }
    
    //Getter & Setter

    public string Username
    {
        get => username;
        set => username = value;
    }

    public string UserID
    {
        get => userID;
        set => userID = value;
    }
    
    
    
    
       
    //init TCP&Network 
    //bool?
    public void InitUserNetworkStream(TcpClient tcpClient)
    {
        this._tcpClient = tcpClient;
        this._networkStream = tcpClient.GetStream();
    }

    public void SendMessage(string message)
    {
        //try{
          //  if(this._networkStream.CanWrite)
            byte[] data = Encoding.UTF8.GetBytes(message);
            _networkStream.Write(data, 0, data.Length);
    }

    public string ReceiveMessage()
    {
        byte[] buffer = new byte[1024];
        int byteCount = _networkStream.Read(buffer, 0, buffer.Length);
        
        return Encoding.UTF8.GetString(buffer, 0, byteCount); 
    }

    public void Disconnect()
    {
        _networkStream.Close();
        _tcpClient.Close();
    }
    
}