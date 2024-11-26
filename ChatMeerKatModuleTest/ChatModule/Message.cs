namespace ChatModule;

public class Message
{
  public User sender { get; private set; }
  public string content { get; private set; }
  public DateTime timestamp { get; private set; }

  //Basic Methods
  public Message(User sender, string content)
  {
      Sender = sender;
      Content = content;
  }
  /* giovabbu*/
  public User Sender
  {
    get => sender;
    private set => sender = value;
  }

  public string Content
  {
    get => content;
    private set => content = value;
  }

  public DateTime Timestamp
  {
    get => timestamp;
    
  }
  
  public override string ToString()
  {
    return $"{Timestamp}: {Sender}: {Content}";
  }
    
  
  
}