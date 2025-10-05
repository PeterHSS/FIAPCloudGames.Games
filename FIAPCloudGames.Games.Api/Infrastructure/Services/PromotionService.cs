using FIAPCloudGames.Games.Api.Commom.Interfaces;
using FIAPCloudGames.Games.Api.Features.Games.Queries;
using Serilog;

namespace FIAPCloudGames.Games.Api.Infrastructure.Services;

public class PromotionService(HttpClient httpClient) : IPromotionService
{
    public async Task<IEnumerable<PromotionResponse>> GetActivePromotionsAsync(CancellationToken cancellationToken)
    {
		try
		{
            IEnumerable<PromotionResponse>? enumerable = await httpClient.GetFromJsonAsync<IEnumerable<PromotionResponse>>("promotions", cancellationToken);

            return enumerable ?? [];

        }
        catch (Exception ex)
		{
            Log.Error(ex, "Error fetching active promotions");

            return [];
        }
    }
}
