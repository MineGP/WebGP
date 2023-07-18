using Discord;
using Discord.WebSocket;

namespace WebGP.Diaka.Discord;

public class GuildDiscordMessage : DiscordMessage
{
    public ulong GuildID;
    public override bool IsGuild => true;
    public GuildDiscordMessage(SocketUserMessage arg) : base(arg) => GuildID = ((IGuildChannel)channel).GuildId;
}
