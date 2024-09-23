public class Program
{
    public static void Main(string[] args)
    {
        Game game = new Game("E:\\words.txt");
        game.GameLoop();
    }
}


class Game
{
    string[] words;
    string correctWord;
    HashSet<char> correctChars;
    HashSet<char> wrongChars;
    char visibleChar;
    bool running;

    public Game(string wordListPath)
    {
        this.words = File.ReadAllText(wordListPath).Split('\n');
        this.correctChars = new HashSet<char>();
        this.wrongChars = new HashSet<char>();

        Random random = new Random();
        int randomWordIndex = random.Next(0, words.Length - 1);
        string randomWord = words[randomWordIndex];
        int randomCharIndex = random.Next(0, randomWord.Length - 1);
        char randomChar = randomWord[randomCharIndex];

        this.correctWord = randomWord.Trim();
        this.visibleChar = randomChar;
        correctChars.Add(randomChar);

        running = true;
    }
    
    public void GameLoop()
    {
        while (this.running)
        {
            Console.Clear();

            if (this.IsGameWon() || this.IsGameLost())
            {
                this.EndGame();
            }

            Console.WriteLine(this.GetStateString());
            char input = this.ReadInput();
            this.ValidateInput(input);
        }
    }


    public string GetWordHidden()
    {
        string concatinated = "";

        foreach (char c in this.correctWord)
        {
            if (c == this.visibleChar || this.correctChars.Contains(c))
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
        int correctCharsCount = this.correctChars.Count();
        int wrongCharsCount = this.wrongChars.Count();


        if (correctCharsCount == 0)
        {
            concatinated += "(0): -";
        } else
        {
            concatinated += " (" + correctCharsCount + "): ";
            foreach (char c in this.correctChars)
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


    public string GetStateString()
    {
        string concatinated = "";
        concatinated += GetWordHidden() + "\n";
        concatinated += GetAttemptCountString() + "\n";
        concatinated += GetGuessesString() + "\n";

        return concatinated;
    }

    public bool IsGameWon()
    {
        foreach (char c in this.correctWord)
        {
            if (!this.correctChars.Contains(c))
            {
                return false;
            }
        }

        return true;
    }
    
    public bool IsGameLost()
    {
        return this.wrongChars.Count() >= 6;
    }

   
    public char ReadInput()
    {
        char input = Console.ReadKey().KeyChar;

        return input;
    }

    
    public bool ValidateInput(char input)
    {
        char[] allowedChars = {
            'q',
            'w',
            'e',
            'r',
            't',
            'y',
            'u',
            'i',
            'o',
            'p',
            'a',
            's',
            'd',
            'f',
            'g',
            'h',
            'j',
            'k',
            'l',
            'z',
            'x',
            'c',
            'v',
            'b',
            'n',
            'm'
        };


        if (!allowedChars.Contains(input))
        {
            return false;
        }

        if(this.correctChars.Contains(input))
        {
            return true; 
        }

        if (this.wrongChars.Contains(input))
        {
            return false;
        }

        if (this.correctWord.Contains(input))
        {
            this.correctChars.Add(input);
            return true;
        }
        else
        {
            this.wrongChars.Add(input);
            return false;
        }
    }


    public void EndGame()
    {
        this.running = false;
        Console.WriteLine("The word was: " + this.correctWord);

        if (this.IsGameWon())
        {
            Console.WriteLine("Congratulations! You won!");
        } else
        {
            Console.WriteLine("You lost! Better luck next time!");
        }


        Console.WriteLine("Press any key to exit...");
        Console.ReadKey(); // it can be ANY key so we don't need to store it or validate it
        Environment.Exit(0);
    }
}
