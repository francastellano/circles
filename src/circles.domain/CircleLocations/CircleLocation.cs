using circles.domain.Abstractions;
using circles.domain.Circles;

namespace circles.domain.ActivityLocations;

public class CircleLocation : BaseEntity 
{
    internal CircleLocation(){}
    internal CircleLocation(Circle circle, string denomination, double? longitude, double? latitude)
    {
        Circle = circle;
        Denomination = denomination;
        Longitude = longitude;
        Latitude = latitude;
    }
    
    public Circle Circle { get; set; }
    public string Denomination { get; set; } = string.Empty;
    public double? Latitude { get; set; }
    public double? Longitude {get;set;}

    public static CircleLocation Create (Circle circle, string denomination, double? longitude, double? latitude){
        var item = new CircleLocation(circle, denomination, longitude, latitude);
        item.Id = Guid.NewGuid();
        return item;
    }
}
