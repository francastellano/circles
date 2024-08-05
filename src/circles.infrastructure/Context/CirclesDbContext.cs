using circles.domain.Circles;
using Microsoft.EntityFrameworkCore;

namespace circles.infrastructure.Context;
public class CirclesDbContext(DbContextOptions<CirclesDbContext> options) : DbContext(options)
{
    public DbSet<Circle> Circles { get; set; } = null!;

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<decimal>().HavePrecision(18, 2);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {

        var result = await base.SaveChangesAsync(cancellationToken);

        return result;
    }

}
