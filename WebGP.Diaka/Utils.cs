using Discord;
using Newtonsoft.Json.Linq;
using System.Diagnostics.CodeAnalysis;

namespace WebGP.Diaka;

public static class Utils
{
    public static T GetConfig<T>(this DiscordConfig config) where T : DiscordConfig, new() => new T()
    {
        DefaultRetryMode = config.DefaultRetryMode,
        LogLevel = config.LogLevel,
        UseSystemClock = config.UseSystemClock
    };
    public static bool TryGetFirst<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> data, TKey[] keys, [MaybeNullWhen(false)] out TValue? value)
    {
        foreach (TKey key in keys)
            if (data.TryGetValue(key, out value))
                return true;
        value = default;
        return false;
    }

    public static readonly IEmote[] OK_MARK = new IEmote[] {
        Emote.Parse("<:ok:1154624576844738572>"),
        Emoji.Parse(":white_check_mark:")
    };
    public static readonly IEmote[] ERROR_MARK = new IEmote[] {
        Emote.Parse("<:error:1154624618246713384>"),
        Emoji.Parse(":negative_squared_cross_mark:")
    };
    public static async Task<string?> GetUserUUID(string userName)
    {
        try
        {
            using HttpClient client = new HttpClient();
            return Guid.TryParse(JToken.Parse(await client.GetStringAsync($"{'h'}ttps://api.mojang.com/users/profiles/minecraft/{userName}"))["id"]?.Value<string>(), out Guid guid)
                ? guid.ToString()
                : null;
        }
        catch
        {
            return null;
        }
    }
}
