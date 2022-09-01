using ChatServer;
using DaL.Models;

const string REGISTER = "1";
const string LOGIN = "2";
const string EXIT = "exit";
ChatUser chatUser;
Server server = new Server();

ShowWellcomeMenu();

void ShowWellcomeMenu()
{
    Console.WriteLine("Wellcome To Chat Demo. Select your choice");
    Console.WriteLine("1. Register");
    Console.WriteLine("2. Login");

    string? userChoice = Console.ReadLine();

    switch (userChoice)
    {
        case REGISTER:
            Register();
            break;
        case LOGIN:
            Login();
            break;

    }
}

void Register()
{
    chatUser = new ChatUser();

    Console.WriteLine("");
    Console.WriteLine("Enter Your Phone Number (only numbers)");
    chatUser.Phone = Console.ReadLine();

    Console.WriteLine("Enter Your Password");
    chatUser.Password = Console.ReadLine();

    bool success = false;
    try
    {
        chatUser = server.RegisterNewUser(chatUser);
    }
    catch (Exception e)
    {
        Console.WriteLine("");
        Console.WriteLine(e.Message);
        Console.WriteLine("");
    }
   

    if (success)
    {
        Console.WriteLine("");
        Console.WriteLine("User Saved");
        Console.WriteLine("");
        
    }

    ShowWellcomeMenu();
}

void Login()
{
    chatUser = new ChatUser();

    Console.WriteLine("");
    Console.WriteLine("Enter Your Phone Number (only numbers)");
    chatUser.Phone = Console.ReadLine();

    Console.WriteLine("Enter Your Password");
    chatUser.Password = Console.ReadLine();
       
    try
    {
        chatUser = server.Login(chatUser);
    }
    catch (Exception e)
    {
        Console.WriteLine("");
        Console.WriteLine(e.Message);
        Console.WriteLine("");

        ShowWellcomeMenu();
    }

   

    if(chatUser != null)
    {
        RunChatDemo();
    }
}

void RunChatDemo()
{
    bool connected = server.ConnectToChat(chatUser);
    if (connected)
    {
        server.StartListen(callback);
        Console.WriteLine("");
        Console.WriteLine("Wellcome to chat demo, type a Message to send. to exit chat type EXIT");
        inputWaiting();

    } else
    {
        Console.WriteLine("Error connect");
    }
}

void inputWaiting()
{
    string message = Console.ReadLine();
    if (message != null)
    {
        if(message.ToLower() == EXIT)
        {
            server.Disconnect(chatUser);
            return;
        }

        bool sent = server.SendMessage(chatUser, message);
        if (!sent)
        {
            Console.WriteLine("Error Send Message");
        }
        inputWaiting();
    }
    
}

void callback(ChatMessage message)
{
    if(message != null)
    {
        Console.WriteLine($"{message.userPhone}:{message.body}");
        inputWaiting();
    }
    
}
 




    