using FIAPCloudGames.Games.Api.Commom.Interfaces;
using FIAPCloudGames.Games.Api.Features.Games.Models;
using FIAPCloudGames.Games.Api.Features.Games.Repositories;
using Serilog;

namespace FIAPCloudGames.Games.Api.Features.Games.Commands.Create;

public sealed class CreateGameUseCase(IGameRepository gameRepository, IUnitOfWork unitOfWork)
{
    public async Task HandleAsync(CreateGameRequest request, CancellationToken cancellationToken)
    {
        Log.Information("Creating game with name: {GameName}", request.Name);

        Game game = Game.Create(request.Name, request.Description, request.ReleasedAt, request.Price, request.Genre);

        await gameRepository.AddAsync(game, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        Log.Information("Game created successfully with ID: {GameId}", game.Id);
    }
}
