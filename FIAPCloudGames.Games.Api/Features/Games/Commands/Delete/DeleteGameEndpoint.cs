using Carter;
using FIAPCloudGames.Games.Api.Endpoints;
using Microsoft.AspNetCore.Mvc;

namespace FIAPCloudGames.Games.Api.Features.Games.Commands.Delete;

public sealed class DeleteGameEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("games/{id:guid}",
            async ([FromQuery] Guid id,
                   [FromServices] DeleteGameUseCase useCase,
                   CancellationToken cancellationToken) =>
            {
                await useCase.HandleAsync(id, cancellationToken);

                return Results.NoContent();
            })
            .WithTags(Tags.Games);
    }
}
