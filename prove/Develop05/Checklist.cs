//Checklist Class

class ChecklistGoal : Activity
{
    private int _times;
    private int _timesToAccomplish;

    public int Recorded
    {
        get { return _times; }
        set { _times = value; }
    }

    public ChecklistGoal(string name, string description, int value, int timesToAccomplish) : base(name, description, value)
    {
        this._timesToAccomplish = timesToAccomplish;
        _times = 0;
    }

    public override bool IsCompleted()
    {
        return _times >= _timesToAccomplish;
    }

    public override void RecordEvent()
    {
        _times++;
    }

    
    public override void DisplayDetails(int index)
    {
        Console.WriteLine($"[{(IsCompleted() ? "X" : " ")}] {index}  {Name} ({Description} -- currently completed: {_times}/{_timesToAccomplish})");
    }

    public override string ToString()
    {
        return $"{GetType().Name},{Name} ({Description}),Value:{Value},Recorded:{_times}/{_timesToAccomplish}";
    }
}