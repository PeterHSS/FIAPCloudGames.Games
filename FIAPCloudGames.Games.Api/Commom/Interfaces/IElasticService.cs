using FIAPCloudGames.Games.Api.Infrastructure.Services.Elasticsearch;

namespace FIAPCloudGames.Games.Api.Commom.Interfaces;

public interface IElasticService
{
    Task CreateIndexIfNotExistsAsync(string indexName);
    Task<bool> AddOrUpdateAsync(GameLog document, string indexName);
    Task<bool> AddOrUpdateBulkAsync(IEnumerable<GameLog> documents, string indexName);
    Task<GameLog?> GetAsync(string key, string indexName);
    Task<IReadOnlyCollection<GameLog>?> GetAllAsync(string indexName);
    Task<bool> RemoveAsync(GameLog document, string indexName);
    Task<long?> RemoveAllAsync(string indexName);
    Task<IEnumerable<GameLog>?> RecommendGameAsync(string gameId, int top);
}
