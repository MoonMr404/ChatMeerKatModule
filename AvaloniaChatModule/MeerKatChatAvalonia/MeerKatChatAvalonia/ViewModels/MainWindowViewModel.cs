using MeerKatChatAvalonia.Models;

namespace MeerKatChatAvalonia.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        public string Greeting => "Test";

        // Aggiungi la proprietà CurrentUser di tipo User
        public User CurrentUser { get; }

        public MainWindowViewModel()
        {
            // Imposta un esempio di utente
            CurrentUser = new User("john_doe", "John Doe");
        }
    }
}