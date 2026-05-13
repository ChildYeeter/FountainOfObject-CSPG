Console.Title = "Foutain of Objects";




Player p1 = new("Player1");
Game game = new();
while (true)
{
    Console.WriteLine("----------------------------------------------------------");
    game.Start(p1);
    Console.WriteLine("----------------------------------------------------------");
    if (p1.CanExit)
    {
        Console.WriteLine("Press any key to exit.");
        Console.ReadKey(true);
        break;
    }
}





public class Game
{
    public void Start(Player player)
    {
        if (!player.CanExit)
            Console.WriteLine($"\nRow:{player.Row} \nColumn:{player.Column} \nIs the fountain on: {player.IsFountainOn}\n");

        GetDiscription(player);

        Console.Write("What do you wanna do?: ");

        Console.ForegroundColor = ConsoleColor.Cyan;

        player.Input = Console.ReadLine()?.ToLower() switch
        {
            "move north" => new MoveNorth(),
            "move south" => new MoveSouth(),
            "move east" => new MoveEast(),
            "move west" => new MoveWest(),
            "enable fountain" => new EnableFountain(),
            "disable fountain" => new DisableFountain(),
            "exit" => new Exit(),
            _ => new MoveNorth()
        };
        Console.ForegroundColor = ConsoleColor.White;

        player.Input.Move(player);
    }

    public void GetDiscription(Player player)
    {
        if(player.Row == 0 && player.Column == 0)
            Console.WriteLine("You see light coming from the cavern entrance");
        if ((player.Row == 0 && player.Column == 2) && player.IsFountainOn)
            Console.WriteLine("You hear the rushing waters from the Fountain of Objects. It has been reactivated!");
        else if ((player.Row == 0 && player.Column == 2) && !player.IsFountainOn)
            Console.WriteLine("You hear water dripping in this room. The Fountain of Objects is here!");
    }
}
public class Player
{
    public string? Name { get; set; }
    public byte Row { get; set; } = 0;
    public byte Column { get; set; } = 0;
    public bool IsFountainOn { get; set; } = false;
    public UserInput? Input { get; set; }
    public bool CanExit { get; set; } = false;
    public Player(string? name)
    {
        this.Name = name;
    }
}

public interface UserInput
{
    public void Move(Player player);
}

public class MoveWest : UserInput
{
    public void Move(Player player)
    {
        if(player.Row <= 0)
            Console.WriteLine("Sorry, you can't move forward!");
        
        else if (player.Row >= 0 && player.Row <= 3)
            player.Row -= 1;
    }
}
public class MoveEast : UserInput
{
    public void Move(Player player)
    {
        if (player.Row >= 3)
            Console.WriteLine("Sorry, you can't move forward!");
        
        else if(player.Row >= 0 && player.Row <= 3) 
            player.Row += 1;
    }
}
public class MoveNorth : UserInput
{
    public void Move(Player player)
    {
        if (player.Column <= 0)
            Console.WriteLine("Sorry, you can't move forward!");

        else if (player.Column >= 0 && player.Column <= 3)
            player.Column -= 1;
    }
}
public class MoveSouth : UserInput
{
    public void Move(Player player)
    {
        if (player.Column >= 3)
            Console.WriteLine("Sorry, you can't move forward!");

        else if (player.Column >= 0 && player.Column <= 3)
            player.Column += 1;
    }
}

public class EnableFountain : UserInput
{
    public void Move(Player player)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        if (player.IsFountainOn)
            Console.WriteLine("The Fountain is already enabled. Escape while you still can!!");

        else if (player.Row == 0 && player.Column == 2)
        {
            player.IsFountainOn = true;
            Console.Beep();
            Console.WriteLine("You've enabled The Fountain of Object!! It's time to escape!");

        }

        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("You see no fountain around in this room. Better keep searching");
        }
            

        Console.ForegroundColor = ConsoleColor.White;
    }
}

public class DisableFountain : UserInput
{
    public void Move(Player player)
    {
        if (!player.IsFountainOn)
            Console.WriteLine("The Fountain is already disabled!");

        else if (player.Row == 0 && player.Column == 2)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            player.IsFountainOn = false;
            Console.WriteLine("ummm......sure man whatever, why not am i right?"); 
            Console.ForegroundColor = ConsoleColor.White;
        }
        else
            Console.WriteLine("You see no fountain around in this room. Better keep searching");
    }
}

public class Exit : UserInput
{
    public void Move(Player player)
    {
        if ((player.Row == 0 && player.Column == 0) && !player.IsFountainOn)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Beep();
            Console.WriteLine("You cannot escape while the fountain is still disabled!!");
            Console.ForegroundColor = ConsoleColor.White;
        }

        else if ((player.Row == 0 && player.Column == 0) && player.IsFountainOn)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("You've escape with your life intact!");
            Console.ForegroundColor = ConsoleColor.White;
            player.CanExit = true;
        }
    }
}