using System;

[Serializable]
public class InputEntry
{
    public string inputName;
    public int highScore;

    public InputEntry(string name, int score)
    {
        inputName = name;
        highScore = score;
    }
}