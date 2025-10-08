using FIAPCloudGames.Games.Api.Commom;
using FIAPCloudGames.Games.Api.Commom.Interfaces;
using FIAPCloudGames.Games.Api.Features.Games.Models;
using FIAPCloudGames.Games.Api.Features.Games.Repositories;
using FIAPCloudGames.Games.Api.Infrastructure.Services.Elasticsearch;
using Serilog;

namespace FIAPCloudGames.Games.Api.Features.Games.Commands.Update;

public sealed class UpdateGameUseCase(IGameRepository gameRepository, IUnitOfWork unitOfWork, IElasticService elastic)
{
    public async Task HandleAsync(Guid id, UpdateGameRequest request, CancellationToken cancellationToken = default)
    {
        Log.Information("Start updating game. {@GameId} {@Request}", id, request);

        Game? game = await gameRepository.GetByIdAsync(id, cancellationToken);

        if (game is null)
        {
            Log.Warning("Game not found. {@GameId}", id);

            throw new KeyNotFoundException($"Game with ID {id} not found.");
        }

        game.Update(request.Name, request.Description, request.ReleasedAt, request.Price, request.Genre);

        gameRepository.Update(game);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        await elastic.AddOrUpdateAsync(new GameLog(game.Id, game.Name, game.Description, game.Price), ElasticIndexName.Games);

        Log.Information("Game updated successfully. {@GameId}", id);
    }
}
