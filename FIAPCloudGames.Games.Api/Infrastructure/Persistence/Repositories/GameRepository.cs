using FIAPCloudGames.Games.Api.Features.Games.Models;
using FIAPCloudGames.Games.Api.Features.Games.Repositories;
using FIAPCloudGames.Games.Api.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace FIAPCloudGames.Games.Api.Infrastructure.Persistence.Repositories;

internal sealed class GameRepository : IGameRepository
{
    private readonly DbSet<Game> _dbSet;

    public GameRepository(GamesDbContext dbContext)
    {
        _dbSet = dbContext.Set<Game>();
    }

    public async Task AddAsync(Game game, CancellationToken cancellationToken = default) 
        => await _dbSet.AddAsync(game, cancellationToken);

    public void Delete(Game game) 
        => _dbSet.Remove(game);

    public async Task<IEnumerable<Game>> GetAllAsync(CancellationToken cancellationToken = default) 
        => await _dbSet.AsNoTracking().ToListAsync(cancellationToken);

    public async Task<Game?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) 
        => await _dbSet.FirstOrDefaultAsync(entity => entity.Id == id, cancellationToken);

    public async Task<IEnumerable<Game>> GetByIdListAsync(IEnumerable<Guid> guids, CancellationToken cancellationToken = default) 
        => await _dbSet.Where(game => guids.Contains(game.Id)).ToListAsync(cancellationToken);

    public void Update(Game game) 
        => _dbSet.Update(game);

    public void UpdateRange(IEnumerable<Game> retrievedGames) 
        => _dbSet.UpdateRange(retrievedGames);
}
