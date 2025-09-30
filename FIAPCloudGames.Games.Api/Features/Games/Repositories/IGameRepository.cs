using FIAPCloudGames.Games.Api.Features.Games.Models;

namespace FIAPCloudGames.Games.Api.Features.Games.Repositories;

public interface IGameRepository
{
    Task<IEnumerable<Game>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Game?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(Game game, CancellationToken cancellationToken = default);
    void Update(Game game);
    void Delete(Game game);
    Task<IEnumerable<Game>> GetByIdListAsync(IEnumerable<Guid> guids, CancellationToken cancellationToken = default);
    void UpdateRange(IEnumerable<Game> retrievedGames);
}
