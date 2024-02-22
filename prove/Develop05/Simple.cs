//Simple goal class
class SimpleGoal : Activity
{
    private bool isCompleted;

    public SimpleGoal(string name, string description, int value) : base(name, description, value)
    {
        isCompleted = false;
    }

    public override bool IsCompleted()
    {
        return isCompleted;
    }
    
    public override void RecordEvent()
    {
        isCompleted = true;
    }
}