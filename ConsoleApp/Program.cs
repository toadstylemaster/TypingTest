// See https://aka.ms/new-console-template for more information


using TypingTestConsoleUI;
using System;
using System.Diagnostics;
using TypingTestBrain;
using System.Reflection.Metadata;

class Program
{
    private static string _basePath = "";
    private static string _textPath = "";
    private static bool _isFinished = false;
    public static Statistics Statistics = new Statistics();
    public static Lines Lines = new Lines();
    public static Word Word = new Word();
    public static TTBrain TtBrain = new TTBrain(Lines, Statistics);
    static bool isTimerRuning;
    private static Stopwatch watch = new Stopwatch();


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
        _isFinished = false;
        TtBrain.GetLinesWithWords(_textPath);


        // TODO: Need to get normal input from GetLines().
        // So lines one by one and then just remember current index and compare to readkey..
        var wordIndex = 0;
        var letterIndex = 0;

        var wrongStreak = 0;

        var rightStr = "";
        var wrongStr = "";

        Console.Clear();
        Console.WriteLine("Press any key to start typing test.");
        Console.ReadKey();

        watch.Start();

        for (int i = 0; i < TtBrain.Lines.AllLines.Count; i++)
        {
            if (_isFinished)
            {
                break;
            }
            Console.Clear();
            var lastWord = TtBrain.Lines.AllLines[i].Split(" ").Last();
            

            WriteLines(i);

            HandleKeys(wordIndex, letterIndex, wrongStreak, lastWord, rightStr, wrongStr);

            wordIndex = (i + 1) * 10;
        }

        Console.WriteLine(rightStr);
        Console.WriteLine(wrongStr);

        return "";

    }

    private static void WriteLines(int i)
    {

        if (i != 0)
        {
            Console.ForegroundColor = ConsoleColor.Gray; 
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (TtBrain.Lines.AllLines[i - 1].Length / 2)) + "}", TtBrain.Lines.AllLines[i - 1]));
            Console.WriteLine();
        }

        Console.ForegroundColor = ConsoleColor.Blue; 
        Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (TtBrain.Lines.AllLines[i].Length / 2)) + "}", TtBrain.Lines.AllLines[i]));
        Console.WriteLine();

        if (i != TtBrain.Lines.AllLines.Count - 1)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (TtBrain.Lines.AllLines[i + 1].Length / 2)) + "}", TtBrain.Lines.AllLines[i + 1]));
        }
        Console.ForegroundColor = ConsoleColor.White;

        Console.WriteLine();
        Console.WriteLine();
    }


    private static void HandleKeys(int wordIndex, int letterIndex, int wrongStreak, string lastWord, string rightLetters, string wrongLetters)
    {   

        while (true)
        {
            if(watch.ElapsedMilliseconds >= 10000) // 60000 milliseconds is 1 minute
            {
                watch.Stop();
                _isFinished = true;
                break;
            }

            if (wordIndex > 0 && lastWord == TtBrain.Lines.AllWords[wordIndex - 1] || wordIndex == 1000)
            {
                break;
            }

            var key = Console.ReadKey(true); // KeyChar reads key as on keyboard, f.e. Spacebar is just space not "Spacebar"

            if (key.Key == ConsoleKey.Spacebar)
            {
                if (letterIndex < TtBrain.Lines.AllWords[wordIndex].Length || wrongStreak > 0)
                {
                    for (var j = 0; j < wrongStreak; j++)
                    {
                        Console.Write("\b \b");
                    }
                    letterIndex -= wrongStreak;
                    for (var k = letterIndex; k < TtBrain.Lines.AllWords[wordIndex].Length; k++)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(TtBrain.Lines.AllWords[wordIndex][k]);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }

                Console.Write(" ");
                wordIndex++;
                letterIndex = 0;
                wrongStreak = 0;
                continue;
            }
            else if (key.Key == ConsoleKey.Backspace)
            {
                if (letterIndex == 0)
                {
                    continue;
                }
                else
                {
                    if (wrongStreak > 0)
                    {
                        wrongStreak--;
                        wrongLetters = wrongLetters.Substring(0, wrongLetters.Length - 1);
                    }
                    else
                    {
                        rightLetters = rightLetters[..^1];
                    }
                    letterIndex -= 1;
                    Console.Write("\b \b");
                }

            }
            else if (letterIndex < TtBrain.Lines.AllWords[wordIndex].Length && key.KeyChar.ToString().Equals(TtBrain.Lines.AllWords[wordIndex][letterIndex].ToString()))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(key.KeyChar.ToString());
                Console.ForegroundColor = ConsoleColor.White;
                rightLetters += key.KeyChar.ToString();
                wrongStreak = 0;
            }
            else   // TODO: If the letter is wrong, make it red
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(key.KeyChar.ToString());
                Console.ForegroundColor = ConsoleColor.White;
                wrongLetters += key.KeyChar.ToString();
                wrongStreak++;
            }

            if (key.Key != ConsoleKey.Backspace)
            {
                letterIndex++;
            }
        }

        Console.WriteLine(rightLetters.ToString());
        Console.WriteLine(wrongLetters.ToString());
    }
}