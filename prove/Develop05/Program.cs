using System;
using System.Collections.Generic;
using System.IO;


//Main Program
class Program
{
    static void Main(string[] args)
    {
        GoalManager goalManager = new GoalManager();

        while (true)
        {
            Console.WriteLine($"Points: {goalManager.AvailablePoints}");
            Console.WriteLine("Menu Options:");
            Console.WriteLine("1. Create New goal");
            Console.WriteLine("2. List Goal");
            Console.WriteLine("3. Save goal");
            Console.WriteLine("4. Load Goals");
            Console.WriteLine("5. Record Event");
            Console.WriteLine("6. Quit");
            Console.Write("Select your choice: ");
            int choice;
            if (int.TryParse(Console.ReadLine(), out choice))
            {
                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Types of goals:");
                        Console.WriteLine("1. Simple goal");
                        Console.WriteLine("2. Eternal goal");
                        Console.WriteLine("3. Checklist goal");
                        Console.Write("Which type of goal would you like to create? ");
                        string goalType = Console.ReadLine();
                        goalManager.CreateNewGoal(goalType);
                        break;
                    case 2:
                        Console.WriteLine("");
                        Console.WriteLine("List of Goals:");
                        goalManager.ListGoals();
                        break;
                    case 3:
                        Console.Write("Enter file name to save the goal "); 
                        string fileName = Console.ReadLine();
                        goalManager.SaveGoals(fileName ); 
                        break;
                    case 4:
                        Console.Write("Enter the file name to load goals: ");
                        string loadFileName = Console.ReadLine();
                        goalManager.LoadGoalsFromFile(loadFileName ); 
                        break;
                    case 5:
                        Console.WriteLine("");
                        Console.WriteLine("List of Saved Goals:");
                        goalManager.ListGoals();
                        Console.Write("Select the event: ");
                        if (int.TryParse(Console.ReadLine(), out int selectedGoalIndex))
                        {
                            if (selectedGoalIndex >= 1 && selectedGoalIndex <= goalManager.Activities.Count)
                            {
                                Activity selectedGoal = goalManager.Activities[selectedGoalIndex - 1];
                                goalManager.RecordEvent(selectedGoal.Name);
                            }
                            else
                            {
                                Console.WriteLine("Invalid goal selected. Please type in one of the numbers on tghe list.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("rror!! Enter a number.");
                        }
                        break;

                    case 6:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Error!! Enter a number that is on the list.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Please try again");
            }
        }
    }
}
