using Carter;
using FIAPCloudGames.Games.Api.Commom;
using Microsoft.AspNetCore.Mvc;

namespace FIAPCloudGames.Games.Api.Features.Games.Queries.GetAll;

public sealed class GetAllGamesEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("games",
            async ([FromServices] GetAllGamesUseCase useCase, 
                   CancellationToken cancellationToken) =>
            {
                IEnumerable<GameResponse> response = await useCase.HandleAsync(cancellationToken);

                return Results.Ok(response);
            })
            .WithTags(Tags.Games);
    }
}
