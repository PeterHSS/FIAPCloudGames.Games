namespace FIAPCloudGames.Games.Api.Features.Games.Commands.Create;

public record CreateGameRequest(string Name, string Description, DateTime ReleasedAt, decimal Price, string Genre);