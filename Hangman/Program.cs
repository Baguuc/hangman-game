class Game
{
    string[] words;
    string correctWord;
    HashSet<char> correctWords;
    HashSet<char> wrongWords;
    char visibleChar;

    public Game(string wordListPath)
    {
        this.words = File.ReadAllText(wordListPath).Split('\n');
        this.correctWords = new HashSet<char>();
        this.wrongWords = new HashSet<char>();

        Random random = new Random();
        int randomWordIndex = random.Next(0, words.Length - 1);
        string randomWord = words[randomWordIndex];
        int randomCharIndex = random.Next(0, randomWord.Length);
        char randomChar = randomWord[randomCharIndex];

        this.correctWord = randomWord.Trim();
        this.visibleChar = randomChar;
    }

    public string GetWordHidden()
    {
        string concatinated = "";

        foreach (char c in this.correctWord)
        {
            if (c == this.visibleChar || this.correctWords.Contains(c))
            {
                concatinated += c + " ";
            }
            else
            {
                concatinated += "_ ";
            }
        }

        return concatinated;
    }


    public string GetAttemptCountString()
    {
        return "Attempts left: " + (6 - this.wrongWords.Count);
    }


    public string GetGuessesString()
    {
        string concatinated = "✔";

        foreach (char c in this.correctWords)
        {
            concatinated += c + " ";
        }
        concatinated += "\n✘";

        foreach (char c in this.wrongWords)
        {
            concatinated += "\u0336" + c + " ";
        }

        return concatinated;
    }

}