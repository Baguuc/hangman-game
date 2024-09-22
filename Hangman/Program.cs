class Game
{
    string[] words;
    string correctWord;
    HashSet<char> correctWords;
    HashSet<char> wrongWords;

    public Game(string wordListPath)
    {
        this.words = File.ReadAllText(wordListPath).Split('\n');
        this.correctWords = new HashSet<char>();
        this.wrongWords = new HashSet<char>();

        Random random = new Random();
        int randomWordIndex = random.Next(0, words.Length - 1);
        string randomWord = words[randomWordIndex];

        this.correctWord = randomWord;
    }
}