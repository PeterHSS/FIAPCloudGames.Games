using FIAPCloudGames.Games.Api.Commom.Interfaces;

namespace FIAPCloudGames.Games.Api.Features.Games.Commands.Recomendation;

public class GameRecommendationUseCase(IElasticService elastic)
{
    public async Task<GameRecommendationResponse> HandleAsync(GameRecommendationRequest request)
    {
        var recommendedGames = await elastic.RecommendGameAsync(request.GameId.ToString(), request.Top);
        
        return new GameRecommendationResponse(recommendedGames ?? []);
    }
}
