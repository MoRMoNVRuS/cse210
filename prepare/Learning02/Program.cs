using System;
using System.Reflection.Emit;

class Program
{    static void Main(string[] args)
    {
        Job j1 = new Job();
        j1._company = "(Cybermatrix)";
        j1._jobTitle = "Founder";
        j1._startYear = "2022";
        j1._endYear = "2024";

        Job j2 = new Job();
    
        j2._company = "(FX Lovers)";
        j2._jobTitle = "C.E.O";
        j2._startYear = "2020";
        j2._endYear = "2024";

        Resume myResume = new Resume();
        myResume._name = "James-ann Umoh";
        

        myResume._jobs.Add(j1);
        myResume._jobs.Add(j2);

        myResume.Display();
            
    }
}