namespace circles.domain.Circles;

public sealed class Circle
{

    internal Circle() { }

    public Circle(string denomination)
    {
        Denomination = denomination;
    }
    public Guid Id { get; set; }
    public string Denomination { get; set; } = string.Empty;
}
