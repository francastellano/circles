namespace circles.domain.Circles;

public sealed class Circle
{

    internal Circle() { }

    internal Circle(string denomination)
    {
        Denomination = denomination;
    }
    public Guid Id { get; set; }
    public string Denomination { get; set; } = string.Empty;

    public static Circle Create(string denomination)
    {
        Circle circle = new Circle(denomination);
        return circle;
    }
}
