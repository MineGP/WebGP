using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;
using System.Text.Json.Nodes;

namespace WebGP.Controllers;

[Route("test")]
[ApiController]
public class TestController : ControllerBase
{
    private record TimeoutJson(JsonObject Data, DateTime End);
    private static readonly ConcurrentDictionary<string, TimeoutJson> webhookCache = new ConcurrentDictionary<string, TimeoutJson>();

    private static ConcurrentDictionary<string, TimeoutJson> GetWebhookCacheWithTick()
    {
        DateTime now = DateTime.Now;
        foreach ((string key, TimeoutJson value) in webhookCache)
        {
            if (value.End > now) continue;
            webhookCache.TryRemove(KeyValuePair.Create(key, value));
        }
        return webhookCache;
    }

    [HttpPost("cache/webhook")]
    public void PostWebhookCache(
        [FromQuery(Name = "id")] string id)
        => GetWebhookCacheWithTick()[id] = new TimeoutJson(JsonNode.Parse(Request.Body)!.AsObject(), DateTime.Now.AddMinutes(1));

    [HttpGet("cache/webhook")]
    public string GetWebhookCache(
        [FromQuery(Name = "id")] string id)
        => GetWebhookCacheWithTick().TryRemove(id, out var value) ? value.Data.ToJsonString() : "null";
}
