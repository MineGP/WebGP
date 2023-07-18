using Discord;
using Newtonsoft.Json.Linq;

namespace WebGP.Diaka;

public static class Utils
{
    public static T GetConfig<T>(this DiscordConfig config) where T : DiscordConfig, new() => new T()
    {
        DefaultRetryMode = config.DefaultRetryMode,
        LogLevel = config.LogLevel,
        UseSystemClock = config.UseSystemClock
    };

    public static readonly Emoji WHITE_CHECK_MARK = ":white_check_mark:";
    public static readonly Emoji NEGATIVE_SQUARED_CROSS_MARK = ":negative_squared_cross_mark:";

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
