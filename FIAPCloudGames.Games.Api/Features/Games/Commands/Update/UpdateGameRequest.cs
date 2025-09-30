namespace FIAPCloudGames.Games.Api.Features.Games.Commands.Update;

public record UpdateGameRequest(string Name, string Description, DateTime ReleasedAt, decimal Price, string Genre);

