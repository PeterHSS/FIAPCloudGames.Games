namespace FIAPCloudGames.Games.Api.Features.Games.Queries;

public sealed record PromotionResponse(Guid Id, string Name, DateTime StartDate, DateTime EndDate, decimal DiscountPercentage, string Description, IEnumerable<GameResponse> Games);