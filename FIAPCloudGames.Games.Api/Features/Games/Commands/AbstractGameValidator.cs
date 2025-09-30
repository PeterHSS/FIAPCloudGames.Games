using System.Linq.Expressions;
using FluentValidation;

namespace FIAPCloudGames.Games.Api.Features.Games.Commands;

public abstract class AbstractGameValidator<T> : AbstractValidator<T>
{
    protected void AddNameRule(Expression<Func<T, string>> name)
    {
        RuleFor(name)
            .NotEmpty().WithMessage("The name is required.")
            .MaximumLength(500).WithMessage("The name must be at most 500 characters long.");
    }

    protected void AddDescriptionRule(Expression<Func<T, string>> description)
    {
        RuleFor(description)
            .MaximumLength(1000).WithMessage("The description must be at most 1000 characters long.");
    }

    protected void AddReleasedAtRule(Expression<Func<T, DateTime>> releasedAt)
    {
        RuleFor(releasedAt)
            .NotEmpty().WithMessage("The release date is required.");
    }

    protected void AddPriceRule(Expression<Func<T, decimal>> price)
    {
        RuleFor(price)
            .NotNull().WithMessage("The price is required.")
            .GreaterThanOrEqualTo(0).WithMessage("The price must be greater than or equal to zero.");
    }

    protected void AddGenreRule(Expression<Func<T, string>> genre)
    {
        RuleFor(genre)
            .NotEmpty().WithMessage("The genre is required.")
            .MaximumLength(50).WithMessage("The genre must be at most 50 characters long.");
    }
}
