using FIAPCloudGames.Games.Api.Features.Games.Models;
using FIAPCloudGames.Games.Api.Features.Games.Repositories;
using Serilog;

namespace FIAPCloudGames.Games.Api.Features.Games.Queries.GetGamesByIds;

public sealed class GetGamesByIdsUseCase(IGameRepository gameRepository)
{
    public async Task<IEnumerable<GameResponse>> HandleAsync(GetGamesByIdsRequest request, CancellationToken cancellationToken)
    {
        IEnumerable<Game> games = await gameRepository.GetByIdListAsync(request.GamesIds, cancellationToken);

        Log.Information("Retrieved {Count} games.", games.Count());

        return games.Select(GameResponse.Create);
    }
}
