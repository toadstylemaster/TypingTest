// See https://aka.ms/new-console-template for more information


using BattleShipConsoleUI;
using TypingTestBrain;

class Program
{
    private static string _basePath = "";
    private static string _textPath = "";
    public static Statistics Statistics = new Statistics();
    public static Lines Lines = new Lines();
    public static Word Word = new Word();
    public static TTBrain TtBrain = new TTBrain(Lines, Statistics);

    static void Main(string[] args)
    {
        _basePath = args.Length == 1 ? args[0] : Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "..";
        _textPath = args.Length == 1 ? args[0] : Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "typingText.txt";
        Console.WriteLine(_basePath);
        Console.WriteLine(_textPath);
        Menu mainMenu = new Menu("BattleShip Main", EMenuLevel.Root);
        mainMenu.AddMenuItems(new List<MenuItem>()
        {
            new("S", "Start Typing Test", StartTypingTest),
            // new("R", "Previous Results", See Previous Results)
               
        });
        mainMenu.Run();
    }

    public static string StartTypingTest()
    {
        TtBrain.GetLines(_textPath);


        // TODO: Need to get normal input from GetLines().
        // So lines one by one and then just remember current index and compare to readkey..

        for (int i = 0; i < TtBrain.Lines.AllLines.Count; i++)
        {
            if(i != 0)
            {
                Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (TtBrain.Lines.AllLines[i - 1].Length / 2)) + "}", TtBrain.Lines.AllLines[i - 1]), ConsoleColor.Gray);
            }

            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (TtBrain.Lines.AllLines[i].Length / 2)) + "}", TtBrain.Lines.AllLines[i]));
            
            if (i != TtBrain.Lines.AllLines.Count - 1)
            {
                Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (TtBrain.Lines.AllLines[i + 1].Length / 2)) + "}", TtBrain.Lines.AllLines[i + 1]), ConsoleColor.Gray);
            }

            for (int j = 0; j < TtBrain.Lines.AllLines[i].Length; j++)
            {
                while (j < TtBrain.Lines.AllLines[i].Length)
                {
                    var key = Console.ReadKey().KeyChar.ToString();
                    if (key.Equals(" "))
                    {
                        Console.SetCursorPosition(0, Console.CursorTop - 1);
                        do { Console.Write("\b \b"); } while (Console.CursorLeft > 0);
                        //Console.WriteLine(String.Format(" " + (Console.WindowWidth / 2) + " "));
                    }
                    else if (key.Equals(TtBrain.Lines.AllLines[i][j]))
                    {
                        Console.ReadKey().KeyChar.ToString(); // KeyChar reads key as on keyboard, f.e. Spacebar is just space not "Spacebar"
                                                                             // Remove entered character from console
                        
                    }
                    else
                    {
                        Console.ReadKey().KeyChar.ToString();

                        
                    }
                }
            }
        }


        return "";

        
    }
}