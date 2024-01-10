using System.Text;

namespace TypingTestBrain;

public class TTBrain
{
    public Lines Lines;
    public Statistics Statistics;
    private static Random rng = new Random();

    public TTBrain(Lines lines, Statistics stats)
    {
        Lines = lines;
        Statistics = stats;
    }

    public void GetLinesWithWords(string filePath)
    {
        string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);
        string text = "";

        foreach (var line in lines)
        {
            text += " " + line;
        }

        text = text.Trim().Replace(",", "").Replace(".", "").ToLower();

        var wordsList = new List<string>();

        foreach (var word in text.Split(" "))
        {
            wordsList.Add(word);
        }

        Shuffle(wordsList);
        Lines.AllWords = wordsList;
        GetLines();
    }

    public void GetLines()
    {
        var line = "";

        var lines = new List<string>();
        var iterator = 1;

        for (int i = 0; i < Lines.AllWords.Count; i++)
        {
            line += Lines.AllWords[i] + " ";

            if(iterator != 1 && iterator % 10 == 0 || i == Lines.AllWords.Count - 1)
            {
                line = line.Trim();
                lines.Add(line);
                line = "";
            }
            iterator++;
        }
        Lines.AllLines = lines;
    }


    public static void Shuffle(List<String> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            string value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}