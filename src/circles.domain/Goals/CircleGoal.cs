using circles.domain.Abstractions;
using circles.domain.Circles;

namespace circles.domain.Goals;

public class CircleGoal : BaseEntity
{
    internal CircleGoal() { }

    internal CircleGoal(Circle circle, string denomination)
    {
        Denomination = denomination;
        Circle = circle;
    }

    public Circle Circle { get; set; }
    public string Denomination { get; set; } = string.Empty;

    public static CircleGoal Create(Circle circle, string denomination)
    {
        var item = new CircleGoal(circle, denomination);
        return item;
    }
}
