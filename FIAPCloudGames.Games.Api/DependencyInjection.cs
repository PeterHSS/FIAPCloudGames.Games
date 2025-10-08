using System.Reflection;
using Carter;
using Elastic.Clients.Elasticsearch;
using FIAPCloudGames.Games.Api.Commom.Interfaces;
using FIAPCloudGames.Games.Api.Commom.Middlewares;
using FIAPCloudGames.Games.Api.Features.Games.Commands.Create;
using FIAPCloudGames.Games.Api.Features.Games.Commands.Delete;
using FIAPCloudGames.Games.Api.Features.Games.Commands.Recomendation;
using FIAPCloudGames.Games.Api.Features.Games.Commands.Update;
using FIAPCloudGames.Games.Api.Features.Games.Models;
using FIAPCloudGames.Games.Api.Features.Games.Queries.GetAll;
using FIAPCloudGames.Games.Api.Features.Games.Queries.GetById;
using FIAPCloudGames.Games.Api.Features.Games.Queries.GetGamesByIds;
using FIAPCloudGames.Games.Api.Features.Games.Repositories;
using FIAPCloudGames.Games.Api.Infrastructure.Persistence.Context;
using FIAPCloudGames.Games.Api.Infrastructure.Persistence.Repositories;
using FIAPCloudGames.Games.Api.Infrastructure.Services;
using FIAPCloudGames.Games.Api.Infrastructure.Services.Elasticsearch;
using FIAPCloudGames.Games.Api.Infrastructure.Settings;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FIAPCloudGames.Games.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddDependecyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddPresentation()
            .AddApplication(configuration)
            .AddInfrastructure(configuration);

        return services;
    }

    private static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();

        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen();

        services.AddCarter();

        services.AddProblemDetails(configure =>
        {
            configure.CustomizeProblemDetails = (context) =>
            {
                context.ProblemDetails.Extensions.TryAdd("requestId", context.HttpContext.TraceIdentifier);
            };
        });

        services.AddExceptionHandler<ValidationExceptionHandlerMiddleware>();

        services.AddExceptionHandler<GlobalExceptionHandlerMiddleware>();

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddSingleton<IElasticService, ElasticService>();

        return services;
    }

    private static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddServices()
            .AddRepositories(configuration)
            .AddSettings(configuration);

        return services;
    }

    private static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddUseCases()
            .AddValidators()
            .AddHttpClients(configuration);

        return services;
    }

    private static IServiceCollection AddHttpClients(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient<IPromotionService, PromotionService>(HttpClient =>
        {
            string baseAddress = configuration.GetValue<string>("PromotionApi:BaseAddress")!;

            HttpClient.BaseAddress = new Uri(baseAddress);
        });
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

        services.AddScoped<GetGamesByIdsUseCase>();

        services.AddScoped<GameRecommendationUseCase>();

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

    private static IServiceCollection AddSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ElasticSettings>(configuration.GetSection(ElasticSettings.SectionName));

        return services;
    }
}
