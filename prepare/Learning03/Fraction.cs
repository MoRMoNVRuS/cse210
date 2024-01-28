public class Fraction 
{
    private int _top;
    private int _bottom;

    public Fraction(){
        _top = 1;
        _bottom = 1;
    }
    public Fraction(int number){
        _top = number;
        _bottom = 5;
    }

    public Fraction(int num, int den){
        _top = num;
        _bottom = den;
    }

    //Getters
    public string GetFractionString(){
        string text = ($" {_top}/{_bottom}");
        return text;
    }

    public double GetDouble(){
        return(double)_top / (double) _bottom;
    }
}