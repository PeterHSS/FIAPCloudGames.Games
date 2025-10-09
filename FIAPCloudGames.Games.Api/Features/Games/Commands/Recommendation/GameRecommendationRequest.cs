namespace FIAPCloudGames.Games.Api.Features.Games.Commands.Recomendation;

public record GameRecommendationRequest(Guid GameId, int Top = 5);