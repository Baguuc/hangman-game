﻿class Game
{
    string[] words;
    string correctWord;
    HashSet<char> correctWords;
    HashSet<char> wrongChars;
    char visibleChar;

    public Game(string wordListPath)
    {
        this.words = File.ReadAllText(wordListPath).Split('\n');
        this.correctWords = new HashSet<char>();
        this.wrongChars = new HashSet<char>();

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
        return "Attempts left: " + (6 - this.wrongChars.Count);
    }


    public string GetGuessesString()
    {
        // GET ✓ CHAR WITH ESCAPED GREEN COLOR
        string concatinated = "\x1b[32m✓\x1b[37m ";
        int correctCharsCount = this.correctWords.Count();
        int wrongCharsCount = this.wrongChars.Count();


        if (correctCharsCount == 0)
        {
            concatinated += "(0): -";
        } else
        {
            concatinated += " (" + correctCharsCount + "): ";
            foreach (char c in this.correctWords)
            {
                concatinated += c + " ";
            }
        }
        
        concatinated += "\n";
        // GET ✗ CHAR WITH ESCAPED RED COLOR
        concatinated += "\x1b[31m✗\x1b[37m ";

        if (wrongCharsCount == 0)
        {
            concatinated += "(0): -";
        } else
        {
            concatinated += " (" + wrongCharsCount + "): ";
            foreach (char c in this.wrongChars)
            {
                concatinated += c + " ";
            }
        }

        return concatinated;
    }

}