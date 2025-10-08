using FIAPCloudGames.Games.Api.Commom;
using FIAPCloudGames.Games.Api.Commom.Interfaces;
using FIAPCloudGames.Games.Api.Features.Games.Models;
using FIAPCloudGames.Games.Api.Features.Games.Repositories;
using FIAPCloudGames.Games.Api.Infrastructure.Services.Elasticsearch;
using Serilog;

namespace FIAPCloudGames.Games.Api.Features.Games.Commands.Delete;

public sealed class DeleteGameUseCase(IGameRepository gameRepository, IUnitOfWork unitOfWork, IElasticService elastic)
{
    public async Task HandleAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Log.Information("Deleting game with ID {GameId}", id);

        Game? game = await gameRepository.GetByIdAsync(id, cancellationToken);

        if (game is null)
        {
            Log.Warning("Game with ID {GameId} not found", id);

            throw new KeyNotFoundException($"Game with ID {id} not found.");
        }

        game.Delete();

        gameRepository.Update(game);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        await elastic.RemoveAsync(new GameLog(game.Id, game.Name, game.Description, game.Price), ElasticIndexName.Games);

        Log.Information("Game with ID {GameId} deleted successfully", id);
    }
}
