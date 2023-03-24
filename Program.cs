namespace MJU23v_D10_inl_sveng
{
    internal class Program
    {
        static List<SweEngGloss> dictionary = new();
        class SweEngGloss
        {
            public string word_swe, word_eng;
            public SweEngGloss(string word_swe, string word_eng)
            {
                this.word_swe = word_swe; this.word_eng = word_eng;
            }
            public SweEngGloss(string line)
            {
                string[] words = line.Split('|');
                this.word_swe = words[0]; this.word_eng = words[1];
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the dictionary app!");
            do
            {
                Help();
                Console.Write("> ");
                string[] input = Console.ReadLine().Split();
                string command = input[0];
                Console.Clear();
                if (command == "quit")
                {
                    Console.WriteLine("Goodbye!");
                    break;
                }
                else if (command == "load")
                {
                    if (input.Length == 2)
                    {
                        Load(input);
                    }
                    else if (input.Length == 1)
                    {
                        Load(input);
                    }
                }
                else if (command == "list")
                {
                    foreach (SweEngGloss gloss in dictionary)
                    {
                        Console.WriteLine($"{gloss.word_swe,-10}  - {gloss.word_eng}");
                    }
                }
                else if (command == "new")
                {
                    if (input.Length == 3)
                    {
                        dictionary.Add(new SweEngGloss(input[1], input[2]));
                    }
                    else if (input.Length == 1)
                    {
                        string sweWord, engWord;
                        GetInput(out sweWord, out engWord);
                        dictionary.Add(new SweEngGloss(sweWord, engWord));
                    }
                }
                else if (command == "delete")
                {
                    if (input.Length == 3) 
                    {
                        try
                        {
                            int index = -1;
                            for (int i = 0; i < dictionary.Count; i++)
                            {
                                SweEngGloss gloss = dictionary[i];
                                if (string.Equals(gloss.word_swe, input[1],StringComparison.OrdinalIgnoreCase) 
                                    && string.Equals(gloss.word_eng, input[2], StringComparison.OrdinalIgnoreCase))
                                    index = i;
                            }
                            dictionary.RemoveAt(index);
                        }
                        catch (ArgumentOutOfRangeException e)
                        {
                            Console.WriteLine($"An error occurred while removing the entry: {e.Message}");
                        }
                    }
                    else if (input.Length == 1)
                    {
                        try
                        {
                            string sweWord, engWord;
                            GetInput(out sweWord, out engWord);
                            int index = -1;
                            for (int i = 0; i < dictionary.Count; i++)
                            {
                                SweEngGloss gloss = dictionary[i];
                                if (string.Equals(gloss.word_swe, sweWord, StringComparison.OrdinalIgnoreCase) 
                                    && string.Equals(gloss.word_eng, engWord, StringComparison.OrdinalIgnoreCase))
                                    index = i;
                            }
                            dictionary.RemoveAt(index);
                        }
                        catch (ArgumentOutOfRangeException e) 
                        { 
                            Console.WriteLine($"An error occurred while removing the entry: {e.Message}"); 
                        }
                    }
                }
                else if (command == "translate")
                {
                    if (input.Length == 2)   
                    {
                        input[0] = input[1];
                        Translate(input);
                    }
                    else if (input.Length == 1)
                    {
                        Console.WriteLine("Write word to be translated: ");
                        input[0] = Console.ReadLine();
                        Translate(input);
                    }
                }
                else
                {
                    Console.WriteLine($"Unknown command: '{command}'");
                }
            }
            while (true);
        }
        private static void Help()
        {
            Console.WriteLine($"\nEnter one of the following commands:\n\n"
                + $"load\t\t\t\t\t- to load default list\n"
                + $"load 'filepath'\t\t\t\t- to load custom list\n"
                + $"list\t\t\t\t\t- to show current list\n"
                + $"new\t\t\t\t\t- to add new entry to curren list\n"
                + $"new 'swedish word' 'english word'\t- to add complete entry\n"
                + $"delete\t\t\t\t\t- to remove entry\n"
                + $"delete 'word' 'word in other lang'\t- to remove entry\n"
                + $"translate\t\t\t\t- show translation to word\n"
                + $"translate 'word'\t\t\t- show translation to word\n"
                + $"quit\t\t\t\t\t- to quit program\n");
        }

        private static void Translate(string[] argument)
        {
            try
            {
                foreach (SweEngGloss gloss in dictionary)  //Bryt ut foreach till båda translate.
                {
                    if (gloss.word_swe == argument[0])
                        Console.WriteLine($"English for {gloss.word_swe} is {gloss.word_eng}");
                    if (gloss.word_eng == argument[0])
                        Console.WriteLine($"Swedish for {gloss.word_eng} is {gloss.word_swe}");
                }
            }
            catch (IndexOutOfRangeException e) { Console.WriteLine(e.Message); }
        }

        private static void GetInput(out string sweWord, out string engWord)
        {
            Console.WriteLine("Write word in Swedish: ");
            sweWord = Console.ReadLine();
            Console.Write("Write word in English: ");
            engWord = Console.ReadLine();
        }

        private static void Load(string[] argument)
        {
            try
            {
                string input = "..\\..\\..\\dict\\sweeng.lis";
                if (argument.Length == 2) input = argument[1];
                using (StreamReader sr = new StreamReader(input))
                {
                    dictionary.Clear();
                    string line = sr.ReadLine();
                    while (line != null)
                    {
                        SweEngGloss gloss = new SweEngGloss(line);
                        dictionary.Add(gloss);
                        line = sr.ReadLine();
                    }
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine($"The file '{e.FileName}' cannot be found");
            }
            catch (Exception e) { Console.WriteLine($"A problem has occured while reading the file: {e.Message}"); } //Säkrar här upp för eventuellt avvikande problem utöver "FileNotFound".
        }
    }
}

