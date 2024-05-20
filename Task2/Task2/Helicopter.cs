namespace Task2;

public class Helicopter : Aircraft
{
    public override bool TakeOff()
    {
        DisplayMessage("--!----ВЕТРОЛЕТ ВЗЛЕТЕЛ!----!--");
        return true;
    }
    
    public override bool Land()
    {
        DisplayMessage("--!----ВЕТРОЛЕТ ПРИЗЕМЛИЛСЯ!----!--");
        return true;
    }
}


