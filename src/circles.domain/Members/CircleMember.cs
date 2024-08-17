using circles.domain.Abstractions;
using circles.domain.Circles;

namespace circles.domain.Members;

public class CircleMember : BaseEntity
{
    internal CircleMember() { }

    public CircleMember(Circle circle, string email)
    {
        Email = email;
        Circle = circle;
    }

    public Circle Circle { get; set; }
    public string Email { get; set; } = string.Empty;

}