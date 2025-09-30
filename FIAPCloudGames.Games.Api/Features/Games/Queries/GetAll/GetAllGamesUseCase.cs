using FIAPCloudGames.Games.Api.Features.Games.Queries;
using FIAPCloudGames.Games.Api.Features.Games.Repositories;
using FIAPCloudGames.Games.Domain.Abstractions.Repositories;
using FIAPCloudGames.Games.Domain.Entities;
using Serilog;

namespace FIAPCloudGames.Games.Api.Features.Games.Queries.GetAll;

public sealed class GetAllGamesUseCase
{
    private readonly IGameRepository _gameRepository;

    public GetAllGamesUseCase(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    public async Task<IEnumerable<GameResponse>> HandleAsync(CancellationToken cancellationToken)
    {
        Log.Information("Retrieving all games...");

        IEnumerable<Game> games = await _gameRepository.GetAllAsync(cancellationToken);

        Log.Information("Retrieved {Count} games.", games.Count());

        return games.Select(GameResponse.Create);
    }
}
