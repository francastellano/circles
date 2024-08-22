namespace circles.application.Exceptions;

public class ItemCantBeFoundException : System.Exception
{
    public ItemCantBeFoundException(string message)
           : base(message)
    {
    }

}