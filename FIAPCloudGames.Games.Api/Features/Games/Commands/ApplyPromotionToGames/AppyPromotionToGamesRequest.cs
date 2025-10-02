namespace FIAPCloudGames.Games.Api.Features.Games.Commands.ApplyPromotionToGames;

public record ApplyPromotionToGamesRequest(Guid PromotionId, IEnumerable<Guid> GamesId);