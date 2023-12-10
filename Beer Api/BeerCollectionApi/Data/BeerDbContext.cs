using Microsoft.EntityFrameworkCore;

public class BeerDbContext : DbContext
{
    public DbSet<Beer> Beers { get; set; }

    public BeerDbContext(DbContextOptions<BeerDbContext> options)
        : base(options)
    {
    }
}
