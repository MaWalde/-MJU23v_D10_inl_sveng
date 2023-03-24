namespace MJU23v_D10_inl_sveng
{
    internal class Program
    {
        static List<SweEngGloss> dictionary;  //Skapa ny lista? Omvandla till dictionary?
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
            string defaultFile = "..\\..\\..\\dict\\sweeng.lis";
            Console.WriteLine("Welcome to the dictionary app!");
            do
            {
                Console.Write("> ");                                //Lägg till tillgängliga kommandon? ev. hjälp-metod?
                string[] argument = Console.ReadLine().Split();
                string command = argument[0];
                if (command == "quit")
                {
                    Console.WriteLine("Goodbye!");
                }
                else if (command == "load")
                {
                    if(argument.Length == 2)                      // TODO: Lägg till Try-Catch för "argument[1]"
                    {
                        using (StreamReader sr = new StreamReader(argument[1]))
                        {
                            dictionary = new List<SweEngGloss>(); // Empty it! TODO: Byt till "Clear"
                            string line = sr.ReadLine();
                            while (line != null)
                            {
                                SweEngGloss gloss = new SweEngGloss(line);
                                dictionary.Add(gloss);
                                line = sr.ReadLine();
                            }
                        }
                    }
                    else if(argument.Length == 1)
                    {
                        using (StreamReader sr = new StreamReader(defaultFile))
                        {
                            dictionary = new List<SweEngGloss>(); // Empty it! TODO: Byt till "Clear"
                            string line = sr.ReadLine();
                            while (line != null)
                            {
                                SweEngGloss gloss = new SweEngGloss(line);
                                dictionary.Add(gloss);
                                line = sr.ReadLine();
                            }
                        }
                    }
                }
                else if (command == "list")
                {
                    foreach(SweEngGloss gloss in dictionary)
                    {
                        Console.WriteLine($"{gloss.word_swe,-10}  - {gloss.word_eng,-10}"); // -10? Återkom efter testkör
                    }
                }
                else if (command == "new")
                {
                    if (argument.Length == 3)
                    {
                        dictionary.Add(new SweEngGloss(argument[1], argument[2]));          //Lirar mot första buildern
                    }
                    else if(argument.Length == 1) //Ändra variablenamn till lättläsligt
                    {
                        Console.WriteLine("Write word in Swedish: "); //Kod lika som på delete, bryt ut och kalla på?
                        string s = Console.ReadLine();
                        Console.Write("Write word in English: ");
                        string e = Console.ReadLine();
                        dictionary.Add(new SweEngGloss(s, e));
                    }
                }
                else if (command == "delete") 
                {
                    if (argument.Length == 3) //TBD: Lägg till kod som ignorerar versaler/gemener, samt try-catch
                    {
                        int index = -1;
                        for (int i = 0; i < dictionary.Count; i++) {
                            SweEngGloss gloss = dictionary[i];
                            if (gloss.word_swe == argument[1] && gloss.word_eng == argument[2])
                                index = i;
                        }
                        dictionary.RemoveAt(index);
                    }
                    else if (argument.Length == 1) //Fixa variabelnamn,
                    {
                        Console.WriteLine("Write word in Swedish: "); //Kod lika som på new, bryt ut och kalla på?
                        string s = Console.ReadLine();
                        Console.Write("Write word in English: ");
                        string e = Console.ReadLine();
                        int index = -1;
                        for (int i = 0; i < dictionary.Count; i++)
                        {
                            SweEngGloss gloss = dictionary[i];
                            if (gloss.word_swe == s && gloss.word_eng == e)
                                index = i;
                        }
                        dictionary.RemoveAt(index);
                    }
                }
                else if (command == "translate")
                {
                    if (argument.Length == 2)  //Try-catch, 
                    {
                        foreach(SweEngGloss gloss in dictionary)  //Bryt ut foreach till båda translate.
                        {
                            if (gloss.word_swe == argument[1])
                                Console.WriteLine($"English for {gloss.word_swe} is {gloss.word_eng}");
                            if (gloss.word_eng == argument[1])
                                Console.WriteLine($"Swedish for {gloss.word_eng} is {gloss.word_swe}");
                        }
                    }
                    else if (argument.Length == 1)
                    {
                        Console.WriteLine("Write word to be translated: ");
                        string s = Console.ReadLine();
                        foreach (SweEngGloss gloss in dictionary)
                        {
                            if (gloss.word_swe == s)
                                Console.WriteLine($"English for {gloss.word_swe} is {gloss.word_eng}");
                            if (gloss.word_eng == s)
                                Console.WriteLine($"Swedish for {gloss.word_eng} is {gloss.word_swe}");
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"Unknown command: '{command}'");
                }
            }
            while (true);
        }
    }
}