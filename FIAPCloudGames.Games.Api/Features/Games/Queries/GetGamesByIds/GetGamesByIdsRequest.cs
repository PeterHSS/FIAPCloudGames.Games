namespace FIAPCloudGames.Games.Api.Features.Games.Queries.GetGamesByIds;

public sealed record GetGamesByIdsRequest(IEnumerable<Guid> GamesIds);
