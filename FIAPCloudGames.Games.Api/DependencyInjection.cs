using System.Reflection;
using FIAPCloudGames.Games.Api.Commom.Interfaces;
using FIAPCloudGames.Games.Api.Features.Games.Commands.Create;
using FIAPCloudGames.Games.Api.Features.Games.Commands.Delete;
using FIAPCloudGames.Games.Api.Features.Games.Commands.Update;
using FIAPCloudGames.Games.Api.Features.Games.Models;
using FIAPCloudGames.Games.Api.Features.Games.Queries.GetAll;
using FIAPCloudGames.Games.Api.Features.Games.Queries.GetById;
using FIAPCloudGames.Games.Api.Features.Games.Repositories;
using FIAPCloudGames.Games.Api.Infrastructure.Persistence.Context;
using FIAPCloudGames.Games.Api.Infrastructure.Persistence.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FIAPCloudGames.Games.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddRepositories(configuration);

        return services;
    }

    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services
            .AddUseCases()
            .AddValidators();

        return services;
    }

    public static void ApplyMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<GamesDbContext>();

        dbContext.Database.Migrate();
    }

    private static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        services.AddScoped<CreateGameUseCase>();

        services.AddScoped<UpdateGameUseCase>();

        services.AddScoped<GetGameByIdUseCase>();

        services.AddScoped<DeleteGameUseCase>();

        services.AddScoped<GetAllGamesUseCase>();

        return services;
    }

    private static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), ServiceLifetime.Scoped, includeInternalTypes: true);

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<GamesDbContext>(options => options.UseNpgsql(configuration.GetConnectionString(nameof(Game))));

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IGameRepository, GameRepository>();

        return services;
    }
}
