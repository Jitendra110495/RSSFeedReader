using System.Net;
using System.Net.Http.Json;
using RSSFeedReader.UI.Models;

namespace RSSFeedReader.UI.Services;

public class SubscriptionApiService
{
    private readonly HttpClient _httpClient;

    public SubscriptionApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Subscription> AddSubscriptionAsync(string url)
    {
        var response = await _httpClient.PostAsJsonAsync("subscriptions", new { url });

        if (response.IsSuccessStatusCode)
        {
            var subscription = await response.Content.ReadFromJsonAsync<Subscription>();
            return subscription ?? throw new InvalidOperationException("Empty response from subscription API.");
        }

        if (response.StatusCode == HttpStatusCode.BadRequest)
        {
            var error = await response.Content.ReadFromJsonAsync<ErrorResponse>();
            throw new InvalidOperationException(error?.Error ?? "Invalid subscription request.");
        }

        throw new InvalidOperationException($"Unexpected response status code {response.StatusCode}.");
    }

    public async Task<List<Subscription>> GetSubscriptionsAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<Subscription>>("subscriptions") ?? new List<Subscription>();
    }

    private sealed record ErrorResponse(string Error);
}
