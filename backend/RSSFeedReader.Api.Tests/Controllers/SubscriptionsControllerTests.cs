using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using RSSFeedReader.Api.Models;
using Xunit;

namespace RSSFeedReader.Api.Tests.Controllers;

public class SubscriptionsControllerTests
{
    [Fact]
    public async Task PostSubscriptions_ValidUrl_ReturnsCreatedAndSubscription()
    {
        await using var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();

        var response = await client.PostAsJsonAsync("api/subscriptions", new { url = "https://example.com/feed" });

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var subscription = await response.Content.ReadFromJsonAsync<Subscription>();
        Assert.NotNull(subscription);
        Assert.Equal("https://example.com/feed", subscription!.Url);
        Assert.False(string.IsNullOrWhiteSpace(subscription.AddedAt));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public async Task PostSubscriptions_EmptyUrl_ReturnsBadRequest(string url)
    {
        await using var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();

        var response = await client.PostAsJsonAsync("api/subscriptions", new { url });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task GetSubscriptions_ReturnsAddedSubscription()
    {
        await using var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();

        var postResponse = await client.PostAsJsonAsync("api/subscriptions", new { url = "https://example.com/feed" });
        Assert.Equal(HttpStatusCode.Created, postResponse.StatusCode);

        var subscriptions = await client.GetFromJsonAsync<List<Subscription>>("api/subscriptions");

        Assert.NotNull(subscriptions);
        Assert.Single(subscriptions!);
        Assert.Equal("https://example.com/feed", subscriptions![0].Url);
    }
}
