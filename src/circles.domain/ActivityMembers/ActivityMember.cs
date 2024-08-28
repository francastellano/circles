using circles.domain.Abstractions;
using circles.domain.Activities;
using circles.domain.Members;

namespace circles.domain.ActivityMembers;

public class ActivityMember : BaseEntity
{
    internal ActivityMember() { }

    internal ActivityMember(CircleMember member, CircleActivity activity)
    {
        Activity = activity;
        Member = member;
    }

    public CircleMember Member { get; set; }
    public CircleActivity Activity { get; set; }

    public static ActivityMember Create(CircleMember member, CircleActivity activity)
    {
        var item = new ActivityMember(member, activity);
        item.Id = Guid.NewGuid();
        return item;
    }

}
