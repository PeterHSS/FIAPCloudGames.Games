using Carter;
using FIAPCloudGames.Games.Api.Commom;
using Microsoft.AspNetCore.Mvc;

namespace FIAPCloudGames.Games.Api.Features.Games.Commands.ApplyPromotionToGames;

public sealed class ApplyPromotionToGamesEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("games/apply-promotion",
            async ([FromBody] ApplyPromotionToGamesRequest request,
                   [FromServices] ApplyPromotionToGamesUseCase useCase,
                   CancellationToken cancellationToken) =>
        {
            await useCase.HandleAsync(request, cancellationToken);

            return Results.Accepted();
        })
        .WithTags(Tags.Games);
    }
}
