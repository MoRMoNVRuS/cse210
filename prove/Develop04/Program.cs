using System;
using System.Threading;

namespace RelaxationApp
{

    class Program
    {
        

        static void Main(string[] args)
        {
            Console.Clear();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Welcome to the Relaxation App!");
                Console.WriteLine("\nChoose an activity:");
                Console.WriteLine("1. Breathing");
                Console.WriteLine("2. Reflection");
                Console.WriteLine("3. Listing");
                Console.WriteLine("4. Exit");

                int choice = int.Parse(Console.ReadLine());

                Activity activity;

                switch (choice)
                {
                    case 1:
                        activity = new BreathingActivity();
                        break;
                    case 2:
                        activity = new ReflectionActivity();
                        break;
                    case 3:
                        activity = new ListingActivity();
                        break;
                    case 4:
                        Console.WriteLine("Thank you for using the app! ");
                        return;
                    default:
                        Console.WriteLine("Wrong choice! Try again.");
                        continue;
                }

                activity.Start();
                activity.PerformActivity();
                activity.End();
            }
        }
    }

    abstract class Activity
    {
        protected string ActivityName;
        protected string Description;
        protected int Duration;

        public abstract void Start();
        public abstract void PerformActivity();
        public abstract void End();

        protected void ShowAnimation(int seconds)
        {
            for (int i = 0; i < seconds; i++)
            {
                Console.Write(".");
                Thread.Sleep(1000);
            }
            Console.WriteLine();
        }

        protected void ShowCountdown(int seconds)
        {
            for (int i = seconds; i > 0; i--)
            {
                Console.Write($"{i} ");
                Thread.Sleep(1000);
            }
            Console.WriteLine();
        }
    }

    class BreathingActivity : Activity
    {
        public BreathingActivity()
        {
            ActivityName = "Breathing";
            Description = "This activity will help you relax by guiding you through breathing in and out slowly. Clear your mind and focus on your breathing.";
        }

        public override void Start()
        {
            Console.Clear();
            Console.WriteLine($"Starting {ActivityName} Activity");
            Console.WriteLine(Description);
            Console.Write("Enter the duration in seconds: ");
            Duration = int.Parse(Console.ReadLine());

            Console.WriteLine("Prepare to begin...");
            ShowAnimation(3);
        }

        public override void PerformActivity()
        {
            for (int i = 0; i < Duration; i += 4)
            {
                Console.WriteLine("Breathe in...");
                ShowCountdown(4);
                Console.WriteLine("Breathe out...");
                ShowCountdown(4);
            }
        }

        public override void End()
        {
            Console.WriteLine("You did a great job! ");
            ShowAnimation(1);
            Console.WriteLine("You have completed the activity.");
            ShowAnimation(1);
            Console.WriteLine("Press enter key to continue!");
            Console.ReadLine();
        }
    }

    class ReflectionActivity : Activity
    {
        private string[] prompts = {
            "Think of a time when you stood up for someone else.",
            "Think of a time when you did something really difficult.",
            "Think of a time when you helped someone in need.",
            "Think of a time when you did something truly selfless."
        };

        private string[] questions = {
            "Why was this experience meaningful to you?",
            "Have you ever done anything like this before?",
            "Are you glad to be here?",
            "Would you want to do this again?"
        };

        public ReflectionActivity()
        {
            ActivityName = "Reflection";
            Description = "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.";
        }

        public override void Start()
        {
            Console.Clear();
            Console.WriteLine($"Starting {ActivityName} Activity");
            Console.WriteLine(Description);
            Console.Write("Enter the duration in seconds: ");
            Duration = int.Parse(Console.ReadLine());

            Console.WriteLine("Prepare to begin...");
            ShowAnimation(2);
        }

        public override void PerformActivity()
        {
            Console.WriteLine(prompts[new Random().Next(prompts.Length)]);
            ShowCountdown(5);

            int elapsedTime = 0;
            while (elapsedTime < Duration)
            {
                Console.WriteLine(questions[new Random().Next(questions.Length)]);
                ShowCountdown(5);
                elapsedTime += 5;
            }
        }

        public override void End()
        {
            Console.WriteLine("You did a great job! ");
            ShowAnimation(2);
            Console.WriteLine("You have completed the activity.");
            ShowAnimation(2);
            Console.WriteLine("Press enter key to continue!");
            Console.ReadLine();
        }
    }

    class ListingActivity : Activity
    {
        private string[] prompts = {
            "Who are people that you appreciate?",
            "What are personal strengths of yours?",
            "Who are people that you have helped this week?",
            "When have you felt the Holy Ghost this month?",
            "Who are some of your personal heroes?"
        };

        private static bool activityTimedOut = false;
        public ListingActivity()
        {
            ActivityName = "Listing";
            Description = "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.";
        }

        public override void Start()
        {
            Console.Clear();
            Console.WriteLine($"Starting {ActivityName} Activity");
            Console.WriteLine(Description);
            Console.Write("Enter the duration in seconds: ");
            Duration = int.Parse(Console.ReadLine());

            Console.WriteLine("Prepare to begin...");
            ShowAnimation(3);
        }

        public override void PerformActivity()
        {
            Console.WriteLine(prompts[new Random().Next(prompts.Length)]);
            ShowCountdown(5);

            int elapsedTime = 0;
            int itemCount = 0;

            Thread timeoutThread = new Thread(() =>
            {
                Thread.Sleep(Duration * 1000);
                Console.WriteLine("Activity timed out!");
                activityTimedOut = true;
            });

            timeoutThread.Start();

            while (elapsedTime < Duration && !activityTimedOut)
            {
                Console.Write("Enter an item: ");
                string item = Console.ReadLine();
                itemCount++;
                elapsedTime += 2;
            }

            timeoutThread.Join();

            Console.WriteLine($"You listed {itemCount} items.");
        }

        public override void End()
        {
            Console.WriteLine("You did a great job! ");
            ShowAnimation(2);
            Console.WriteLine("You have completed the activity.");
            ShowAnimation(2);
            Console.WriteLine("Press enter key to continue!");
            Console.ReadLine();
        }
    }
}
