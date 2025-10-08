using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using FIAPCloudGames.Games.Api.Commom;
using FIAPCloudGames.Games.Api.Commom.Interfaces;
using FIAPCloudGames.Games.Api.Infrastructure.Settings;
using Microsoft.Extensions.Options;

namespace FIAPCloudGames.Games.Api.Infrastructure.Services.Elasticsearch;

public class ElasticService : IElasticService
{
    private readonly ElasticsearchClient _client;
    private readonly ElasticSettings _settings;

    public ElasticService(IOptions<ElasticSettings> options)
    {
        _settings = options.Value;

        var settings = new ElasticsearchClientSettings(new Uri(_settings.Uri))
            .Authentication(new ApiKey(_settings.ApiKey));

        _client = new ElasticsearchClient(settings);
    }

    public async Task CreateIndexIfNotExistsAsync(string indexName)
    {
        if (!_client.Indices.Exists(indexName).Exists)
        {
            await _client.Indices.CreateAsync(indexName);
        }
    }

    public async Task<bool> AddOrUpdateAsync(GameLog document, string indexName)
    {
        await CreateIndexIfNotExistsAsync(indexName);

        var response = await _client.IndexAsync(document, i => i.Index(indexName));

        return response.IsValidResponse;
    }

    public async Task<bool> AddOrUpdateBulkAsync(IEnumerable<GameLog> documents, string indexName)
    {
        await CreateIndexIfNotExistsAsync(indexName);

        var response = await _client.BulkAsync(bulk => bulk.Index(indexName).UpdateMany(documents, (ud, u) => ud.Doc(u).DocAsUpsert(true)));

        return response.IsValidResponse;
    }

    public async Task<IReadOnlyCollection<GameLog>?> GetAllAsync(string indexName)
    {
        await CreateIndexIfNotExistsAsync(indexName);

        var response = await _client.SearchAsync<GameLog>(s => s.Indices(indexName));

        return response.IsValidResponse ? response.Documents : default;
    }

    public async Task<GameLog?> GetAsync(string key, string indexName)
    {
        await CreateIndexIfNotExistsAsync(indexName);
        
        var response = await _client.GetAsync<GameLog>(key, idx => idx.Index(indexName));

        return response.Source;
    }

    public async Task<long?> RemoveAllAsync(string indexName)
    {
        var response = await _client.DeleteByQueryAsync<GameLog>(d => d.Indices(indexName));

        return response.IsValidResponse ? response.Deleted : default;
    }

    public async Task<bool> RemoveAsync(GameLog document, string indexName)
    {
        var response = await _client.DeleteAsync(document, d => d.Index(indexName));

        return response.IsValidResponse;
    }

    public async Task<IEnumerable<GameLog>?> RecommendGameAsync(string gameId, int top)
    {
        var response = await _client.SearchAsync<GameLog>(
            search => search
            .Indices(ElasticIndexName.Games)
            .Query(query => query
                .MoreLikeThis(mlt => mlt
                    .Fields(new Field[] { "title", "description" })
                    .Like(l => l.Document(d => d.Id(gameId)))
                    .MinTermFreq(0)
                    .MinDocFreq(1)
                    .MaxQueryTerms(25)
                )
            )
            .Size(top)
        );

        if (response.IsValidResponse)
            return response.Hits.Select(h => h.Source!).ToList();

        throw new Exception($"Failed to get recommendations for game ID {gameId}: {response.DebugInformation}");
    }
}
