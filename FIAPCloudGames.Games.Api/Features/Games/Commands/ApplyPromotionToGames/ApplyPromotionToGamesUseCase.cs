using FIAPCloudGames.Games.Api.Commom.Interfaces;
using FIAPCloudGames.Games.Api.Features.Games.Models;
using FIAPCloudGames.Games.Api.Features.Games.Repositories;
using Serilog;

namespace FIAPCloudGames.Games.Api.Features.Games.Commands.ApplyPromotionToGames;

internal sealed class ApplyPromotionToGamesUseCase(IGameRepository gameRepository, IUnitOfWork unitOfWork)
{
    internal async Task HandleAsync(ApplyPromotionToGamesRequest request, CancellationToken cancellationToken)
    {
        IEnumerable<Game> games = await gameRepository.GetByIdListAsync(request.GamesId, cancellationToken);

        var returnedIds = games.Select(g => g.Id).ToHashSet();

        var missingIds = request.GamesId.Except(returnedIds).ToList();

        if (missingIds.Any())
            Log.Warning("Games not found for IDs: {MissingIds}", string.Join(", ", missingIds));

        games.ToList().ForEach(game => game.ApplyPromotion(request.PromotionId));

        gameRepository.UpdateRange(games);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
