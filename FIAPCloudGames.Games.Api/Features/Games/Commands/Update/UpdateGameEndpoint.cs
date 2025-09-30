using Carter;
using FIAPCloudGames.Games.Api.Endpoints;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace FIAPCloudGames.Games.Api.Features.Games.Commands.Update;

public sealed class 
    UpdateGameEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/games/{id:guid}",
            async ([FromQuery] Guid id,
                   [FromBody] UpdateGameRequest request,
                   [FromServices] UpdateGameUseCase useCase,
                   [FromServices] IValidator<UpdateGameRequest> validator,
                   CancellationToken cancellationToken) =>
            {
                validator.ValidateAndThrow(request);

                await useCase.HandleAsync(id, request, cancellationToken);

                return Results.NoContent();
            }).WithTags(Tags.Games);
    }
}
