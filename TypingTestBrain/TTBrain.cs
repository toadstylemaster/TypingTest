using System.Text;

namespace TypingTestBrain;

public class TTBrain
{
    public Lines Lines;
    public Statistics Statistics;
    
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


        var linesList = new List<string>();
        for (int i = 0; i < text.Length / 50; i++)
        {
            linesList.Add(text.Substring(i * 50, text.Length - i * 50 >= 50 ? 50 : text.Length - i * 50));
        }
        
        Lines.AllLines = linesList;
        Lines.AllWords = text.Split(' ').ToList();
    }
}