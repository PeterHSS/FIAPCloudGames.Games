using FIAPCloudGames.Games.Api.Features.Games.Models;

namespace FIAPCloudGames.Games.Api.Features.Games.Queries;

public record GameResponse(Guid Id, string Name, string Description, DateTime ReleasedAt, decimal Price, string Genre)
{
    public static GameResponse Create(Game game)
    {
        return new GameResponse(game.Id, game.Name, game.Description, game.ReleasedAt, game.Price, game.Genre);
    }
}

