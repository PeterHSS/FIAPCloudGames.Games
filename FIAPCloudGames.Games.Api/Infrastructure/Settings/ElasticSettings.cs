namespace FIAPCloudGames.Games.Api.Infrastructure.Settings;

public class ElasticSettings
{
    public const string SectionName = "Elasticsearch";
    public required string Uri { get; set; }
    public required string ApiKey { get; set; }
}
