using System.ComponentModel;
using System.Runtime.CompilerServices;
using MeerKatChatAvalonia.Models;


namespace MeerKatChatAvalonia.ViewModels;

public class ChatViewModel : INotifyPropertyChanged
{
    private User _user;

    public User CurrentUser
    {
        get => _user;
        set
        {
            _user = value;
            OnPropertyChanged();
        }
    }

    public ChatViewModel()
    {
        CurrentUser = new User("Nikolai Belinski", "Nikollll");
    }
    
    
    // Implementazione di INotifyPropertyChanged
    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}