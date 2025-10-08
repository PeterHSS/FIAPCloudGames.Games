using FIAPCloudGames.Games.Api.Infrastructure.Services.Elasticsearch;

namespace FIAPCloudGames.Games.Api.Features.Games.Commands.Recomendation;

public record GameRecommendationResponse(IEnumerable<GameLog> Games);
