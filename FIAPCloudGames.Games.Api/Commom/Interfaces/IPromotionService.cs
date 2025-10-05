using FIAPCloudGames.Games.Api.Features.Games.Queries;

namespace FIAPCloudGames.Games.Api.Commom.Interfaces;

public interface IPromotionService
{
    Task<IEnumerable<PromotionResponse>> GetActivePromotionsAsync(CancellationToken cancellationToken);
}
