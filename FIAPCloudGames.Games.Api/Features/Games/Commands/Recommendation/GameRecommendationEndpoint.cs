using Carter;
using FIAPCloudGames.Games.Api.Commom;
using Microsoft.AspNetCore.Mvc;

namespace FIAPCloudGames.Games.Api.Features.Games.Commands.Recomendation;

public class GameRecommendationEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("games/recommendation",
            async ([FromBody] GameRecommendationRequest request,
                   [FromServices] GameRecommendationUseCase useCase,
                   CancellationToken cancellationToken) =>
            {
                var response = await useCase.HandleAsync(request);

                return Results.Ok(response);
            })
            .WithTags(Tags.Games);
    }
}