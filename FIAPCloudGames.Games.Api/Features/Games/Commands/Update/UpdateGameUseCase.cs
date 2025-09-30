using FIAPCloudGames.Games.Api.Features.Games.Models;
using FIAPCloudGames.Games.Api.Features.Games.Repositories;
using FIAPCloudGames.Games.Domain.Abstractions.Repositories;
using Serilog;

namespace FIAPCloudGames.Games.Api.Features.Games.Commands.Update;

public sealed class UpdateGameUseCase
{
    private readonly IGameRepository _gameRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateGameUseCase(IGameRepository gameRepository, IUnitOfWork unitOfWork)
    {
        _gameRepository = gameRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task HandleAsync(Guid id, UpdateGameRequest request, CancellationToken cancellationToken = default)
    {
        Log.Information("Start updating game. {@GameId} {@Request}", id, request);

        Game? game = await _gameRepository.GetByIdAsync(id, cancellationToken);

        if (game is null)
        {
            Log.Warning("Game not found. {@GameId}", id);

            throw new KeyNotFoundException($"Game with ID {id} not found.");
        }

        game.Update(request.Name, request.Description, request.ReleasedAt, request.Price, request.Genre);

        _gameRepository.Update(game);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        Log.Information("Game updated successfully. {@GameId}", id);
    }
}
