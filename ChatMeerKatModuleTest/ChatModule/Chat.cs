namespace ChatModule;

public class Chat
{
    private List<Message> messages;
    private List<User> users;
    
    public Chat()
    {
        messages = new List<Message>();
        users = new List<User>();
    }

    
    //Chat Methods --> Boolean?
    public void AddMessage(Message message)
    {
        messages.Add(message);
        Console.WriteLine($"{message.Sender}:,{message},{message.TimeStamp}");
        BroadcastMessage(message.ToString());
    }
    
    public void AddUser(User user)
    {
        users.Add(user);
    }

    public void BroadcastMessage(string message)
    {
        foreach (var user in users)
        {
            try
            {
                user.SendMessage(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore nell'invio del messaggio a {user.Username}: {ex.Message}");
            }
        }
    }
}