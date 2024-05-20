namespace Task2;
public class Airplane : Aircraft
{
    public double RunwayLength = 500;
    
    public override bool TakeOff()
    {
        if (RunwayLength >= 500)
        {
            DisplayMessage("--!----САМОЛЕТ УСПЕШНО ВЗЛЕТЕЛ!----!--");
            return true;
        }
        else
        {
            DisplayMessage("---!!!---ВЗЛЕТОЧНАЯ ПОЛОСА КОРОТКАЯ (МЕНЬШЕ 500 МЕТРОВ)!---!!!---");
            return false;
        }
    }
    
    public override bool Land()
    {
        DisplayMessage("--!----САМОЛЕТ УСПЕШНО ПРИЗЕМЛИЛСЯ!----!--");
        return true;
    }
}

