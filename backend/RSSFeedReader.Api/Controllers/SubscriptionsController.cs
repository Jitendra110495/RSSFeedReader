using Microsoft.AspNetCore.Mvc;
using RSSFeedReader.Api.Models;
using RSSFeedReader.Api.Services;

namespace RSSFeedReader.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubscriptionsController : ControllerBase
{
    private readonly SubscriptionService _subscriptionService;

    public SubscriptionsController(SubscriptionService subscriptionService)
    {
        _subscriptionService = subscriptionService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Subscription>> GetSubscriptions()
    {
        return Ok(_subscriptionService.GetSubscriptions());
    }

    [HttpPost]
    public ActionResult<Subscription> AddSubscription([FromBody] AddSubscriptionRequest request)
    {
        if (request == null || string.IsNullOrWhiteSpace(request.Url))
        {
            return BadRequest(new { error = "Subscription URL is required." });
        }

        var subscription = _subscriptionService.AddSubscription(request.Url);
        return CreatedAtAction(nameof(GetSubscriptions), null, subscription);
    }
}
