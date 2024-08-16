using circles.domain.Abstractions;
using circles.domain.Circles;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace circles.infrastructure.Context;
public class CirclesDbContext(DbContextOptions<CirclesDbContext> options, IMediator mediator) : DbContext(options)
{
    public DbSet<Circle> Circles { get; set; } = null!;

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
        }

        var result = await base.SaveChangesAsync(cancellationToken);

        foreach (var domainEvent in domainEvents)
        {
            await mediator.Publish(domainEvent, cancellationToken);
        }

        return result;
    }

}
