using Avalonia.Controls;
using MeerKatChatAvalonia.ViewModels;

namespace MeerKatChatAvalonia.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Imposta il ViewModel come DataContext
            DataContext = new MainWindowViewModel();
        }
    }
}