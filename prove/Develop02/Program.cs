using System;
using System.Collections.Generic;
using System.IO;

namespace JournalApp
{
    class Program
    {
        static List<Entry> journal = new List<Entry>();
        static string[] prompts = {
            "How are you doing today?",
            "Are you happy with the way your day is turning out?",
            "WWhats is exciting you today?",
            "If there is one thing you can undo, what would it be?",
            "I am here to share your day with you, what can you tell me?",
            "No matter how things are turning out, just know you can do it. Did I encourage you?",
        };

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                DisplayMenuItem();

                int choice;
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            WriteNewEntry();
                            break;
                        case 2:
                            DisplayJournal();
                            break;
                        case 3:
                            SaveJournal();
                            break;
                        case 4:
                            LoadJournal();
                            break;
                        case 5:
                            Console.WriteLine("Exiting journal...");
                            return;
                        default:
                            Console.WriteLine("Invalid choice. Please enter a number between 1 and 5.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Wrong Entry. Please enter a number.");
                }

                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
            }
        
        }

    //Menu Items Display
        static void DisplayMenuItem()
        {
            Console.WriteLine("***** My Journal *****");
            Console.WriteLine("_____************______");
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display the journal");
            Console.WriteLine("3. Save journal to file");
            Console.WriteLine("4. Load journal from file");
            Console.WriteLine("5. Exit");
            Console.Write("Enter your choice: ");
        }

        static void WriteNewEntry()
        {
            string randomPrompt = prompts[new Random().Next(prompts.Length)];
            Console.WriteLine(randomPrompt);
            string response = Console.ReadLine();
            journal.Add(new Entry(response, randomPrompt, DateTime.Now));
            Console.WriteLine("Entry saved!");
        }

        static void DisplayJournal()
        {
            if (journal.Count == 0)
            {
                Console.WriteLine("The journal is currently empty.");
            }
            else
            {
                foreach (Entry entry in journal)
                {
                    Console.WriteLine($"{entry.Date.ToString("MM/dd/yyyy")} - Prompt: {entry.Prompt}");
                    Console.WriteLine(entry.Response);
                }
            }
        }

        static void SaveJournal()
        {
            Console.Write("Enter the you wan to name your journal file: ");
            string filename = Console.ReadLine();

            using (StreamWriter writer = new StreamWriter(filename))
            {
                foreach (Entry entry in journal)
                {
                    writer.WriteLine($"{entry.Date.ToString("MM/dd/yyyy")}|{entry.Prompt}|{entry.Response}");
                }
            }
            Console.WriteLine("Journal saved successfully!");
        }

    //Loading Journal file
        static void LoadJournal()
        {
            Console.Write("Enter the filename to load the journal: ");
            string filename = Console.ReadLine();

            if (File.Exists(filename))
            {
                journal.Clear();

                using (StreamReader reader = new StreamReader(filename))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split('|');
                        DateTime date = DateTime.Parse(parts[0]);
                        string prompt = parts[1];
                        string response = parts[2];
                        journal.Add(new Entry(response, prompt, date));
                    }
                }

                Console.WriteLine("Journal loaded successfully!");
            }
            else
            {
                Console.WriteLine("File not found.");
            }
        }
    }
    
    class Entry
    {
        public string Response { get; set; }
        public string Prompt { get; set; }
        public DateTime Date { get; set; }

        public Entry(string response, string prompt, DateTime date)
        {
            Response = response;
            Prompt = prompt;
            Date = date;
        }
    }
}
