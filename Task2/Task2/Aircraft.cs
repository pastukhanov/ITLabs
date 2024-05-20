namespace Task2;
using System;

public abstract class Aircraft
{
    private double _altitude;
    
    public double Altitude
    {
        get { return _altitude; }
        set
        {
            _altitude = value;
        }
    }
    
    public abstract bool TakeOff();
    public abstract bool Land();
    protected void DisplayMessage(string message)
    {
        Console.WriteLine(message);
    }
}
