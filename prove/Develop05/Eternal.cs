class EternalGoal : Activity
{
    public EternalGoal(string name, string description, int value) : base(name, description, value)
    {
    }
    public override bool IsCompleted()
    {
        return false;
    }
    public override void RecordEvent()
    {
    
    }
}