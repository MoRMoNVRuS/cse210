class GoalManager
{
    private List<Activity> activities;
    private int availablePoints; 


    public GoalManager()
    {
        activities = new List<Activity>();
        availablePoints = 0; 
    }


    public int AvailablePoints 
    {
        get { return availablePoints; }
        private set { availablePoints = value; }
    }

    public List<Activity> Activities
    {
        get { return activities; }
        private set { activities = value; }
    }

    public void AddActivity(Activity activity)
    {
        activities.Add(activity);
    }

    public void AddPoints(int points)
    {
        availablePoints += points;
    }

    public void ListGoals()
    {
        if (activities.Count == 0)
        {
            Console.WriteLine("Sorry!! No goals available currently.");
            return;
        }

        for (int i = 0; i < activities.Count; i++)
        {
            Activity activity = activities[i];
            activity.DisplayDetails(i + 1);
        }
    }

    public void CreateNewGoal(string goalType)
    {
        Console.WriteLine("Enter goal name: ");
        string goalName = Console.ReadLine();
        Console.WriteLine("Enter goal description: ");
        string goalDescription = Console.ReadLine();
        Console.WriteLine("Enter points to be awarded: ");
        int pointsAwarded;
        while (!int.TryParse(Console.ReadLine(), out pointsAwarded))
        {
            Console.WriteLine("Invalid input. Please enter an integers.");
        }

        int _timesToAccomplish = 0; 

        switch (goalType)
        {
            case "1":
                AddActivity(new SimpleGoal(goalName, goalDescription, pointsAwarded));
                break;
            case "2":
                AddActivity(new EternalGoal(goalName, goalDescription, pointsAwarded));
                break;
            case "3":
                Console.WriteLine("Enter number of times the goal needs to be accomplished: ");
                while (!int.TryParse(Console.ReadLine(), out _timesToAccomplish) || _timesToAccomplish <= 0)
                {
                    Console.WriteLine("Invalid input for times to accomplish. Please enter a positive integer.");
                }
                AddActivity(new ChecklistGoal(goalName, goalDescription, pointsAwarded, _timesToAccomplish));
                break;
            default:
                Console.WriteLine("Invalid goal type.");
                break;
        }
    }


    public void LoadGoalsFromFile(string fileName)
    {
        if (File.Exists(fileName))
        {
            activities.Clear(); 

            using (StreamReader reader = new StreamReader(fileName)) 
            {
                string totalPointsLine = reader.ReadLine();
                if (int.TryParse(totalPointsLine, out int totalPoints))
                {
                    availablePoints = totalPoints; 
                }

                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(',');
                    if (parts.Length >= 3)
                    {
                        string goalType = parts[0];
                        string[] details = parts[1].Split('(');
                        string name = details[0];
                        string description = details[1].Trim(')');
                        int value;
                        if (int.TryParse(parts[2].Split(':')[1], out value))
                        {
                            switch (goalType)
                            {
                                case nameof(SimpleGoal):
                                    AddActivity(new SimpleGoal(name, description, value));
                                    break;
                                case nameof(EternalGoal):
                                    AddActivity(new EternalGoal(name, description, value));
                                    break;
                                case nameof(ChecklistGoal):
                                    int recorded;
                                    int toAccomplish;
                                    string[] recordDetails = parts[3].Split(':')[1].Split('/');
                                    if (recordDetails.Length == 2 && int.TryParse(recordDetails[0], out recorded) && int.TryParse(recordDetails[1], out toAccomplish))
                                    {
                                        AddActivity(new ChecklistGoal(name, description, value, toAccomplish) { Recorded = recorded });
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Invalid data format in line: {line}");
                                    }
                                    break;
                                default:
                                    Console.WriteLine($"Invalid goal type '{goalType}' in file '{fileName}'.");
                                    break;
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Invalid value for goal '{name}' in file '{fileName}'.");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Invalid data format in line: {line}");
                    }
                }
            }

            Console.WriteLine("Goals loaded successfully.");
        }
        else
        {
            Console.WriteLine($"File '{fileName}' not found.");
        }
    }

    public void SaveGoals(string fileName)
    {
        using (StreamWriter writer = new StreamWriter(fileName)) 
        {
            writer.WriteLine(availablePoints); 
            foreach (Activity activity in activities)
            {
                writer.WriteLine(activity.ToString());
            }
        }

        Console.WriteLine("Saved successfully.");
    }

    public void RecordEvent(string goalName)
    {
        bool found = false;
        foreach (Activity activity in activities)
        {
            if (activity.Name == goalName)
            {
                
                if (activity is SimpleGoal simpleGoal)
                {
                    
                    simpleGoal.RecordEvent();
                    int pointsEarned = simpleGoal.Value;
                    availablePoints += pointsEarned;
                    Console.WriteLine("");
                    Console.WriteLine($"Congratulations! You have earned {pointsEarned} points.");
                }
                else if (activity is EternalGoal eternalGoal)
                {
                   
                    int pointsEarned = eternalGoal.Value;
                    availablePoints += pointsEarned;
                    Console.WriteLine("");
                    Console.WriteLine($"Congratulations! You have earned {pointsEarned} points.");
                }
                else if (activity is ChecklistGoal checklistGoal)
                {
                    
                    checklistGoal.RecordEvent(); 
                    if (checklistGoal.IsCompleted())
                    {
                       
                        int pointsEarned = checklistGoal.Value;
                        availablePoints += pointsEarned;
                        Console.WriteLine("");
                        Console.WriteLine($"Congratulations! You have earned {pointsEarned} points");
                    }
                }
                found = true;
                break;
            }
        }

        if (!found)
        {
            Console.WriteLine($"Goal '{goalName}' not found.");
        }
    }
}