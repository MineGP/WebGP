using Discord;
using Discord.WebSocket;

namespace WebGP.Diaka.Discord;

public class DiscordMessage
{
    public ulong ChannelID;
    public ulong MessageID;
    public ulong AuthorID;
    public string Content;
    protected IMessageChannel channel;

    public virtual bool IsGuild => false;

    public DiscordMessage(SocketUserMessage arg)
    {
        MessageID = arg.Id;
        ChannelID = arg.Channel.Id;
        channel = arg.Channel;
        AuthorID = arg.Author.Id;
        Content = arg.Content;
    }
    public static DiscordMessage GetMessage(SocketUserMessage arg) => arg.Channel is IGuildChannel ? new GuildDiscordMessage(arg) : new DiscordMessage(arg);
    public async Task<ulong> SendMessageAsync(string? text = null, bool isTTS = false, Embed? embed = null, RequestOptions? options = null) => (await channel.SendMessageAsync(text, isTTS, embed, options)).Id;
}