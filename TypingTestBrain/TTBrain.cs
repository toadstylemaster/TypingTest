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

    public void GetLines(string filePath)
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


        var linesList = new List<string>();
        for (int i = 0; i < text.Length / 50; i++)
        {
            linesList.Add(text.Substring(i * 50, text.Length - i * 50 >= 50 ? 50 : text.Length - i * 50));
        }

        Shuffle(wordsList);
        Shuffle(linesList);

        Lines.AllLines = linesList;
        Lines.AllWords = wordsList;
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