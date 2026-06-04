using RSSFeedReader.Api.Services;
using Xunit;

namespace RSSFeedReader.Api.Tests.Services;

public class SubscriptionServiceTests
{
    [Fact]
    public void AddSubscription_ValidUrl_ReturnsSubscriptionWithAddedAt()
    {
        var service = new SubscriptionService();

        var subscription = service.AddSubscription("https://example.com/feed");

        Assert.NotNull(subscription);
        Assert.Equal("https://example.com/feed", subscription.Url);
        Assert.False(string.IsNullOrWhiteSpace(subscription.AddedAt));
        Assert.True(DateTime.TryParse(subscription.AddedAt, out _));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void AddSubscription_EmptyUrl_ThrowsArgumentException(string url)
    {
        var service = new SubscriptionService();

        Assert.Throws<ArgumentException>(() => service.AddSubscription(url));
    }

    [Fact]
    public void GetSubscriptions_ReturnsAddedSubscriptions()
    {
        var service = new SubscriptionService();

        service.AddSubscription("https://example.com/feed");

        var subscriptions = service.GetSubscriptions();

        Assert.Single(subscriptions);
    }

    [Fact]
    public void AddSubscription_DuplicateUrls_BothStored()
    {
        var service = new SubscriptionService();

        service.AddSubscription("https://example.com/feed");
        service.AddSubscription("https://example.com/feed");

        Assert.Equal(2, service.GetSubscriptions().Count);
    }
}
