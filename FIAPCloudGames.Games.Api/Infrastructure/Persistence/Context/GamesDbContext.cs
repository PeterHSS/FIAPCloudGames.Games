using FIAPCloudGames.Games.Api.Features.Games.Models;
using Microsoft.EntityFrameworkCore;

namespace FIAPCloudGames.Games.Api.Infrastructure.Persistence.Context;

public sealed class GamesDbContext : DbContext
{
    public DbSet<Game> Games { get; set; }

    public GamesDbContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(GamesDbContext).Assembly);
    }
}
