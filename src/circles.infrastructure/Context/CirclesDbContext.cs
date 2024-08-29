using circles.domain.Abstractions;
using circles.domain.Activities;
using circles.domain.ActivityMembers;
using circles.domain.Circles;
using circles.domain.Goals;
using circles.domain.Members;
using circles.domain.MemberSkills;
using circles.domain.Skills;

using MediatR;
using Microsoft.EntityFrameworkCore;

namespace circles.infrastructure.Context;
public class CirclesDbContext(DbContextOptions<CirclesDbContext> options, IMediator mediator) : DbContext(options)
{
    public DbSet<Circle> Circles { get; set; } = null!;
    public DbSet<CircleMember> CircleMembers { get; set; } = null!;
    public DbSet<CircleSkill> CircleSkills { get; set; } = null!;
    public DbSet<CircleGoal> CircleGoals { get; set; } = null!;
    public DbSet<MemberSkill> MemberSkills { get; set; } = null!;
    public DbSet<CircleActivity> CircleActivities { get; set; } = null!;
    public DbSet<ActivityMember> ActivityMembers { get; set; } = null!;

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<decimal>().HavePrecision(18, 2);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        List<IDomainEvent> domainEvents = [];

        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            var data = entry.Entity.GetDomainEvents();
            domainEvents.AddRange(data);
            entry.Entity.ClearDomainEvents();

            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAtUtc = DateTime.UtcNow;
                entry.Entity.UpdatedAtUtc = DateTime.UtcNow;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedAtUtc = DateTime.UtcNow;
            }
        }

        var result = await base.SaveChangesAsync(cancellationToken);

        foreach (var domainEvent in domainEvents)
        {
            await mediator.Publish(domainEvent, cancellationToken);
        }
        return result;
    }

}
