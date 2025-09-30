using Carter;
using FIAPCloudGames.Games.Api.Endpoints;
using Microsoft.AspNetCore.Mvc;

namespace FIAPCloudGames.Games.Api.Features.Games.Queries.GetById;

public sealed class GetGameByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("games/{id:guid}",
            async ([FromQuery] Guid id,
                   [FromServices] GetGameByIdUseCase useCase,
                   CancellationToken cancellationToken) =>
            {
                GameResponse response = await useCase.HandleAsync(id, cancellationToken);

                return Results.Ok(response);
            })
            .WithTags(Tags.Games);
    }
}
