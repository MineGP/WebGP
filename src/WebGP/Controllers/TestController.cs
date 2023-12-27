using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace WebGP.Controllers;

[Route("test")]
[ApiController]
public class TestController : ControllerBase
{
    private record TimeoutJson(JsonObject Data, DateTime End);
    private static readonly ConcurrentDictionary<string, TimeoutJson> webhookCache = new ConcurrentDictionary<string, TimeoutJson>();

    private static void WebhookCacheWithTick()
    {
        DateTime now = DateTime.Now;
        foreach ((string key, TimeoutJson value) in webhookCache)
        {
            if (value.End > now) continue;
            webhookCache.TryRemove(KeyValuePair.Create(key, value));
        }
    }

    [HttpPost("cache/webhook/send")]
    public async Task<string> PostWebhookCache(
        [FromQuery(Name = "id")] string id, 
        CancellationToken cancellationToken)
    {
        try
        {
            WebhookCacheWithTick();
            webhookCache[id] = new TimeoutJson((await JsonSerializer.DeserializeAsync<JsonObject>(Request.Body, cancellationToken: cancellationToken))!, DateTime.Now.AddMinutes(1));
            return "ok";
        }
        catch (Exception e)
        {
            return e.ToString();
        }
    }

    [HttpGet("cache/webhook/get")]
    public string GetWebhookCache(
        [FromQuery(Name = "id")] string id)
    {
        try
        {
            WebhookCacheWithTick();
            return webhookCache.TryRemove(id, out var value) ? value.Data.ToJsonString() : "null";
        }
        catch (Exception e)
        {
            return e.ToString();
        }
    }
}
