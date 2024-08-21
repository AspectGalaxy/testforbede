 public abstract class Slot
{
    protected char character;
    protected decimal coeffcient;

    //this is a percentage chance out of 100
    public int probabilityToAppear;

    //if this slot has been selected this method will write the character assinged to this slot to the screen.
    public void display()
    {
        Console.Write(character);
    }
    public decimal getCoeffcient()
    {
        return coeffcient;
    }
}