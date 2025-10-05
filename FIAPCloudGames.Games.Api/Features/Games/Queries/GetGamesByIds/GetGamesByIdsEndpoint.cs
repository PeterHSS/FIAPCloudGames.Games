using Carter;
using FIAPCloudGames.Games.Api.Commom;
using Microsoft.AspNetCore.Mvc;

namespace FIAPCloudGames.Games.Api.Features.Games.Queries.GetGamesByIds;

public class GetGamesByIdsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("games/by-ids",
            async ([FromBody] GetGamesByIdsRequest ids,
                   [FromServices] GetGamesByIdsUseCase useCase,
                   CancellationToken cancellationToken) =>
            {
                IEnumerable<GameResponse> response = await useCase.HandleAsync(ids, cancellationToken);

                return Results.Ok(response);
            })
            .WithTags(Tags.Games);
    }
}
