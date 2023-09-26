namespace WebGP.Utils.SSE;

public static class SSEUtils
{
    //event - The event type, such as message, or another defined by your application
    //data - The field data itself
    //retry - An integer value indicating the reconnection time in case of a disconnect
    //id - Message id value

    public static async Task SSEInitAsync(this HttpContext ctx, CancellationToken cancellationToken = default)
    {
        IHeaderDictionary headers = ctx.Response.Headers;
        headers.ContentType = "text/event-stream";
        headers.CacheControl = "no-cache";
        headers.Connection = "keep-alive";
        await ctx.Response.Body.FlushAsync(cancellationToken);
    }
    public static async Task SSESendAsync(this HttpContext ctx, PacketSSE packet, CancellationToken cancellationToken = default)
    {
        if (packet.ID is int id)
            await ctx.Response.WriteAsync($"id: {id}\r", cancellationToken: cancellationToken);
        if (packet.EventName is string eventName)
            await ctx.Response.WriteAsync($"event: {eventName}\r", cancellationToken: cancellationToken);

        await ctx.Response.WriteAsync($"data: {packet.Message}\r\r", cancellationToken: cancellationToken);
        await ctx.Response.Body.FlushAsync(cancellationToken);
    }
    public static async Task SSESendAsync(this HttpContext ctx, IEnumerable<PacketSSE> packets, CancellationToken cancellationToken = default)
    {
        foreach (PacketSSE packet in packets)
            await ctx.SSESendAsync(packet, cancellationToken);
    }
    public static async Task SSESendAsync(this HttpContext ctx, IAsyncEnumerable<PacketSSE> packets, CancellationToken cancellationToken = default)
    {
        await foreach (PacketSSE packet in packets)
            await ctx.SSESendAsync(packet, cancellationToken);
    }
}