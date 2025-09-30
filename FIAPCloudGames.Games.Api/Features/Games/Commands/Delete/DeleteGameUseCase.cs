using FIAPCloudGames.Games.Api.Features.Games.Models;
using FIAPCloudGames.Games.Api.Features.Games.Repositories;
using FIAPCloudGames.Games.Domain.Abstractions.Repositories;
using Serilog;

namespace FIAPCloudGames.Games.Api.Features.Games.Commands.Delete;

public sealed class DeleteGameUseCase
{
    private readonly IGameRepository _gameRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteGameUseCase(IGameRepository gameRepository, IUnitOfWork unitOfWork)
    {
        _gameRepository = gameRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task HandleAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Log.Information("Deleting game with ID {GameId}", id);

        Game? game = await _gameRepository.GetByIdAsync(id, cancellationToken);

        if (game is null)
        {
            Log.Warning("Game with ID {GameId} not found", id);

            throw new KeyNotFoundException($"Game with ID {id} not found.");
        }

        game.Delete();

        _gameRepository.Update(game, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        Log.Information("Game with ID {GameId} deleted successfully", id);
    }
}
