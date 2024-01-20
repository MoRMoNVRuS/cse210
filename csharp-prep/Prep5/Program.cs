using System;
using Microsoft.VisualBasic;

class Program
{
    static void Main(string[] args)
    {   
        DisplayWelcomeMessage();
        string userName = PromptUserName();
        int favouriteNumber = PromptUserNumber();
        int squaredNumber = SquareNumber(favouriteNumber);
        DisplayResult(userName, squaredNumber);     
    }
    static void DisplayWelcomeMessage(){
        Console.WriteLine("Welcome to the Program!");
    }
    static string PromptUserName(){
        Console.Write("Please enter your name: ");
        string name = Console.ReadLine();
        return name;
    }
    static int PromptUserNumber(){
        Console.Write("Please enter your favorite number: ");
        int number = int.Parse(Console.ReadLine());
        return number;
    }
    static int SquareNumber(int num){
        int squared = num * num;
        return squared;
    }

    static void DisplayResult (string userName, int squaredNum){
        Console.WriteLine($"{userName}, the square of your number is {squaredNum}");
    }
    
}