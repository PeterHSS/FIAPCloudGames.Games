namespace FIAPCloudGames.Games.Api.Features.Games.Commands.Create;

public sealed class CreateGameValidator : AbstractGameValidator<CreateGameRequest>
{
    public CreateGameValidator()
    {
        AddNameRule(request => request.Name);

        AddDescriptionRule(request => request.Description);

        AddReleasedAtRule(request => request.ReleasedAt);

        AddPriceRule(request => request.Price);

        AddGenreRule(request => request.Genre);
    }
}

