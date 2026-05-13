Console.Title = "Foutain of Objects";




Player p1 = new("Player1");
Pit pit = new();
Game game = new();

Console.WriteLine("You enter the Cavern of Objects, a maze of rooms filled with dangerous pits in search of the Fountain of Objects." +
    "\nLight is visible only in the entrance, and no other light is seen anywhere in the caverns." +
    "\nFind the Fountain of Objects, activate it, and return to the entrance." +
    "\n\nLook out for pits. You will feel a breeze if a pit is in an adjacent room. If you enter a room with a pit, you will die." +
    "\n\nType \"HELP\" to know more!");


while (true)
{

    pit.PitTrigger(p1, game);

    if (p1.IsPitTriggered)
    {
        game.EndGame();
        break;
    }

    game.Start(p1);

    if (p1.CanExit)
    {
        game.EndGame();
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

        this.CreateDivider();

        Console.Write("What do you wanna do?: ");

        Console.ForegroundColor = ConsoleColor.Cyan;

        player.Input = Console.ReadLine()?.ToLower() switch
        {
            "move north"        or "north"   => new MoveNorth(),
            "move south"        or "south"   => new MoveSouth(),
            "move east"         or "east"    => new MoveEast(),
            "move west"         or "west"    => new MoveWest(),
            "enable fountain"   or "enable"  => new EnableFountain(),
            "disable fountain"  or "disable" => new DisableFountain(),
                 "exit"                      => new Exit(),
                 "help"                      => new Help(),
                 _                           => new Help()
        };
        Console.ForegroundColor = ConsoleColor.White;

        player.Input.Move(player, this);
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
    public void EndGame()
    {
        Console.WriteLine("Press any key to exit.");
        Console.ReadKey(true);
    }

    public void CreateDivider()
    {
        Console.ForegroundColor= ConsoleColor.White;
        Console.WriteLine("----------------------------------------------------------");
        Console.ForegroundColor = ConsoleColor.White;
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
    public bool IsPitTriggered { get; set; } = false;
    public Player(string? name)
    {
        this.Name = name;
    }
}

public interface UserInput
{
    public void Move(Player player, Game game);
}

public class MoveWest : UserInput
{
    public void Move(Player player, Game game)
    {
        if(player.Row <= 0)
            Console.WriteLine("Sorry, you can't move forward!");
        
        else if (player.Row >= 0 && player.Row <= 3)
            player.Row -= 1;
    }
}
public class MoveEast : UserInput
{
    public void Move(Player player, Game game)
    {
        if (player.Row >= 3)
            Console.WriteLine("Sorry, you can't move forward!");
        
        else if(player.Row >= 0 && player.Row <= 3) 
            player.Row += 1;
    }
}
public class MoveNorth : UserInput
{
    public void Move(Player player, Game game)
    {
        if (player.Column <= 0)
            Console.WriteLine("Sorry, you can't move forward!");

        else if (player.Column >= 0 && player.Column <= 3)
            player.Column -= 1;
    }
}
public class MoveSouth : UserInput
{
    public void Move(Player player, Game game)
    {
        if (player.Column >= 3)
            Console.WriteLine("Sorry, you can't move forward!");

        else if (player.Column >= 0 && player.Column <= 3)
            player.Column += 1;
    }
}

public class EnableFountain : UserInput
{
    public void Move(Player player, Game game)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        if (player.IsFountainOn)
        {
            Console.WriteLine("The Fountain is already enabled. Escape while you still can!!");
        }

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
    public void Move(Player player, Game game)
    {
        if (!player.IsFountainOn)
        {
            Console.WriteLine("The Fountain is already disabled!");
        }

        else if (player.Row == 0 && player.Column == 2)
        {
            player.IsFountainOn = false;
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("ummm......sure man whatever, why not am i right?");
            Console.ForegroundColor = ConsoleColor.White;
        }
        else
        {
            Console.WriteLine("You see no fountain around in this room. Better keep searching");
        }
    }
}

public class Exit : UserInput
{
    public void Move(Player player, Game game)
    {
        if ((player.Row == 0 && player.Column == 0) && !player.IsFountainOn)
        {
            Console.Beep();
            Console.ForegroundColor = ConsoleColor.Red;
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

public class Help : UserInput
{
    public void Move(Player player, Game game)
    {
        game.CreateDivider();
        Console.WriteLine("HELP SECTION!!\n" +
                          "\"move north\" -> allows you to move in the north direction\n" +
                          "\"move south\" -> allows you to move in the south direction\n" +
                          "\"move west\" -> allows you to move in the west direction\n" +
                          "\"move east\" -> allows you to move in the east direction\n" +
                          "\"enable fountain\" -> allows you to enable the fountain\n" +
                          "\"disable fountain\" -> allows you to disable the fountain\n");
    }
}
public class Pit
{
    public void PitTrigger(Player player, Game game)
    {
        if (player.Row == 2 && player.Column == 2)
        {
            Console.ForegroundColor= ConsoleColor.Red;
            Console.WriteLine("You've triggered the pit!\nGAME OVER!!!!");
            player.IsPitTriggered = true;
            Console.ForegroundColor = ConsoleColor.White;
            
        }
        else if((player.Row == 1 && player.Column >= 1) || (player.Row == 3 && player.Column >= 1)
                || (player.Row >= 1 && player.Column == 1) || (player.Row == 1 && player.Column >= 3))
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("There is a pit nearby!");
        }
        game.CreateDivider();
    }
}