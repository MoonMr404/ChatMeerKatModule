using System;

namespace MeerKatChatAvalonia.Models;

public class User
{
    private string _username;
    private string _displayName;


    public string Username
    {
        get => _username;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Username cannot be null or empty.");
            _username = value;
        }
    }

    public User(string username, string displayName)
    {
        Username = username;
        DisplayName = displayName;
    }
    
    public string DisplayName
    {
        get => _displayName;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Display Name cannot be null or empty.");
            _displayName = value;
        }
    }
    
    // ToString override for debugging or display purposes
    public override string ToString()
    {
        return $"{DisplayName} ({Username})";
    }

    
}