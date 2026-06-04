using RSSFeedReader.Api.Models;

namespace RSSFeedReader.Api.Services;

public class SubscriptionService
{
    private readonly List<Subscription> _subscriptions = new();

    public Subscription AddSubscription(string url)
    {
        if (string.IsNullOrWhiteSpace(url))
        {
            throw new ArgumentException("Subscription URL is required.", nameof(url));
        }

        var subscription = new Subscription
        {
            Url = url.Trim(),
            AddedAt = DateTime.UtcNow.ToString("o")
        };

        _subscriptions.Add(subscription);
        return subscription;
    }

    public IReadOnlyCollection<Subscription> GetSubscriptions() => _subscriptions.AsReadOnly();
}
