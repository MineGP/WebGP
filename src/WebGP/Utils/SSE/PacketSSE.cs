using Newtonsoft.Json.Linq;

namespace WebGP.Utils.SSE;

public record PacketSSE(int? ID, string? EventName, string Message)
{
    public PacketSSE(int? id, string? eventName, JToken message)
        : this(id, eventName, message.ToString(Newtonsoft.Json.Formatting.None)) { }

    public PacketSSE(string message)
        : this(null, null, message) { }
    public PacketSSE(JToken message)
        : this(null, null, message) { }

    public static implicit operator PacketSSE(string message) => new PacketSSE(message);
    public static implicit operator PacketSSE(JToken message) => new PacketSSE(message);
}
