using System;
using System.Collections.Generic;
using System.Linq;


class Program
{
    static void Main(string[] args)
    {
        string book = "Proverbs";
        int chapter = 3, startVerse = 5, endVerse = 6;

        Reference userReference = new Reference(book, chapter, startVerse, endVerse);

        string userText = "Trust in the LORD with all your heart and lean not on your own understanding; in all your ways submit to him, and he will make your paths straight.";

        Scripture savedScripture = new Scripture(userReference, userText);

        Console.Clear();
        DisplayScripture(savedScripture);

        while (savedScripture.HasVisibleWords())
        {
            Console.Write("Press Enter to hide words or type 'quit' to exit: ");
            string input = Console.ReadLine();

            if (input.ToLowerInvariant() == "quit")
            {
                break;
            }

            savedScripture.HideRandomWords();
            Console.Clear();
            DisplayScripture(savedScripture);
        }
    }

    static void DisplayScripture(Scripture scripture)
    {
        Console.WriteLine(scripture.Reference.ToString());
        Console.WriteLine(scripture.GetText());
    }
}


class Scripture
{
    public Reference Reference { get; }
    public string Text { get; }

    private List<Word> words = new List<Word>();

    public Scripture(Reference reference, string text)
    {
        Reference = reference;
        Text = text;
        words.AddRange(text.Split(' ').Select(w => new Word(w)));
    }

    public bool HasVisibleWords()
    {
        return words.Any(word => !word.hidden);
    }

    public void HideRandomWords()
    {
        int wordsToHide = 2;
        Random random = new Random();

        for (int i = 0; i < wordsToHide; i++)
        {
            int index = random.Next(words.Count);
            words[index].Hide();
        }
    }


    public string GetText()
    {
        return string.Join(" ", words.Select(w => w.hidden ? "____" : w.Text));
    }
}

class Reference
{
    public string Book { get; }
    public int Chapter { get; }
    public int StartVerse { get; }
    public int? EndVerse { get; }

    public Reference(string book, int chapter, int startVerse)
    {
        Book = book;
        Chapter = chapter;
        StartVerse = startVerse;
    }

    public Reference(string book, int chapter, int startVerse, int endVerse)
    {
        Book = book;
        Chapter = chapter;
        StartVerse = startVerse;
        EndVerse = endVerse;
    }

    public override string ToString()
    {
        return $"{Book} {Chapter}:{StartVerse}{(EndVerse.HasValue ? "-" + EndVerse : "")}";
    }
}


class Word
{
    public string Text { get; }
    public bool hidden { get; private set; }

    public Word(string text)
    {
        Text = text;
    }

    public void Hide()
    {
        hidden = true;
    }
}
