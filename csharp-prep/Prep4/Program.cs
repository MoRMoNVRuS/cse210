using System;
using System.Diagnostics.CodeAnalysis;

class Program
{
    static void Main(string[] args)
    {
        List<int> numbers = new List<int>();
        Console.WriteLine("Enter a number type 0 to exit: ");
        int num = -1;
        while (num != 0){
        Console.WriteLine("What is your number? ")  ;
            string addNum = Console.ReadLine();
            num = int.Parse(addNum);
            if (num != 0){
                numbers.Add(num);
            }  
        }
        int sum = 0;
        //SUM
        foreach (int number in numbers){
            sum += number ;
        }
        //Largest Number
        int largest = numbers[0];
        foreach (int number in numbers){
            if (number > largest){
                largest = number;
            }
        }  
        float average = ((float)sum) / numbers.Count;
        Console.WriteLine($"The sum is: {sum}");
        Console.WriteLine($"The avaerage is: {average}");
        Console.WriteLine($"The largest number is: {largest}");
    }
}