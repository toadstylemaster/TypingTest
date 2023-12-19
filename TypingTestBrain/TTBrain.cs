using System.Text;

namespace TypingTestBrain;

public class TTBrain
{
    public Text Text;
    public Statistics Statistics;
    
    public TTBrain(Text text, Statistics stats)
    {
        Text = text;
        Statistics = stats;
    }

    public void GetLines(string filePath)
    {
        string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);
        Lines allLines = new Lines();
        var linesList = new List<string>();

        foreach (var line in lines)
        {
            linesList.Add(line);
        }

        allLines.AllLines = linesList;
        Text.Lines = allLines;
    }
}