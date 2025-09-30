using Carter;
using FIAPCloudGames.Games.Api.Commom;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace FIAPCloudGames.Games.Api.Features.Games.Commands.Create;

public sealed class CreateGameEndpoint : ICarterModule

{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("games",
            async ([FromBody] CreateGameRequest request,
                   [FromServices] CreateGameUseCase useCase,
                   [FromServices] IValidator<CreateGameRequest> validator,
                   CancellationToken cancellationToken) =>
            {
                validator.ValidateAndThrow(request);

                await useCase.HandleAsync(request, cancellationToken);

                return Results.Created();
            })
            .WithTags(Tags.Games);
    }
}
