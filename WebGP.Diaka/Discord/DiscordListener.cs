using Discord;
using Discord.WebSocket;

namespace WebGP.Diaka.Discord;
public class DiscordListener
{

    public DiscordSocketClient client;
    public DiscordListener(DiscordConfig config)
    {
        client = new DiscordSocketClient(config.GetConfig<DiscordSocketConfig>());
        client.MessageReceived += Client_MessageReceived;
        client.ReactionsCleared += Client_ReactionsCleared;
        client.RoleCreated += Client_RoleCreated;
        client.RoleDeleted += Client_RoleDeleted;
        client.RoleUpdated += Client_RoleUpdated;
        client.JoinedGuild += Client_JoinedGuild;
        client.ReactionRemoved += Client_ReactionRemoved;
        client.LeftGuild += Client_LeftGuild;
        client.GuildUnavailable += Client_GuildUnavailable;
        client.GuildMembersDownloaded += Client_GuildMembersDownloaded;
        client.GuildUpdated += Client_GuildUpdated;
        client.UserJoined += Client_UserJoined;
        client.UserLeft += Client_UserLeft;
        client.UserBanned += Client_UserBanned;
        client.UserUnbanned += Client_UserUnbanned;
        client.UserUpdated += Client_UserUpdated;
        client.GuildMemberUpdated += Client_GuildMemberUpdated;
        client.UserVoiceStateUpdated += Client_UserVoiceStateUpdated;
        client.VoiceServerUpdated += Client_VoiceServerUpdated;
        client.CurrentUserUpdated += Client_CurrentUserUpdated;
        client.UserIsTyping += Client_UserIsTyping;
        client.GuildAvailable += Client_GuildAvailable;
        client.ReactionAdded += Client_ReactionAdded;
        client.MessagesBulkDeleted += Client_MessagesBulkDeleted;
        client.MessageUpdated += Client_MessageUpdated;
        client.ChannelCreated += Client_ChannelCreated;
        client.ChannelDestroyed += Client_ChannelDestroyed;
        client.RecipientRemoved += Client_RecipientRemoved;
        client.RecipientAdded += Client_RecipientAdded;
        client.MessageDeleted += Client_MessageDeleted;
        client.ChannelUpdated += Client_ChannelUpdated;
    }

    private async Task TryCatchLog(Task task) { try { await task; } catch (Exception e) { Console.WriteLine(e.ToString()); } }

    private Task Client_ChannelUpdated(SocketChannel arg1, SocketChannel arg2) => TryCatchLog(OnChannelUpdated?.Invoke(arg1, arg2) ?? Task.CompletedTask);
    private Task Client_MessageDeleted(Cacheable<IMessage, ulong> arg1, Cacheable<IMessageChannel, ulong> arg2) => TryCatchLog(OnMessageDeleted?.Invoke(arg1, arg2) ?? Task.CompletedTask);
    private Task Client_RecipientAdded(SocketGroupUser arg) => TryCatchLog(OnRecipientAdded?.Invoke(arg) ?? Task.CompletedTask);
    private Task Client_RecipientRemoved(SocketGroupUser arg) => TryCatchLog(OnRecipientRemoved?.Invoke(arg) ?? Task.CompletedTask);
    private Task Client_ChannelDestroyed(SocketChannel arg) => TryCatchLog(OnChannelDestroyed?.Invoke(arg) ?? Task.CompletedTask);
    private Task Client_ChannelCreated(SocketChannel arg) => TryCatchLog(OnChannelCreated?.Invoke(arg) ?? Task.CompletedTask);
    private Task Client_MessageUpdated(Cacheable<IMessage, ulong> arg1, SocketMessage arg2, ISocketMessageChannel arg3) => TryCatchLog(OnMessageUpdated?.Invoke(arg1, arg2, arg3) ?? Task.CompletedTask);
    private Task Client_MessagesBulkDeleted(IReadOnlyCollection<Cacheable<IMessage, ulong>> arg1, Cacheable<IMessageChannel, ulong> arg2) => TryCatchLog(OnMessagesBulkDeleted?.Invoke(arg1, arg2) ?? Task.CompletedTask);
    private Task Client_ReactionAdded(Cacheable<IUserMessage, ulong> arg1, Cacheable<IMessageChannel, ulong> arg2, SocketReaction arg3) => TryCatchLog(OnReactionAdded?.Invoke(arg1, arg2, arg3) ?? Task.CompletedTask);
    private Task Client_GuildAvailable(SocketGuild arg) => TryCatchLog(OnGuildAvailable?.Invoke(arg) ?? Task.CompletedTask);
    private Task Client_UserIsTyping(Cacheable<IUser, ulong> arg1, Cacheable<IMessageChannel, ulong> arg2) => TryCatchLog(OnUserIsTyping?.Invoke(arg1, arg2) ?? Task.CompletedTask);
    private Task Client_CurrentUserUpdated(SocketSelfUser arg1, SocketSelfUser arg2) => TryCatchLog(OnCurrentUserUpdated?.Invoke(arg1, arg2) ?? Task.CompletedTask);
    private Task Client_VoiceServerUpdated(SocketVoiceServer arg) => TryCatchLog(OnVoiceServerUpdated?.Invoke(arg) ?? Task.CompletedTask);
    private Task Client_UserVoiceStateUpdated(SocketUser arg1, SocketVoiceState arg2, SocketVoiceState arg3) => TryCatchLog(OnUserVoiceStateUpdated?.Invoke(arg1, arg2, arg3) ?? Task.CompletedTask);
    private Task Client_GuildMemberUpdated(Cacheable<SocketGuildUser, ulong> arg1, SocketGuildUser arg2) => TryCatchLog(OnGuildMemberUpdated?.Invoke(arg1, arg2) ?? Task.CompletedTask);
    private Task Client_UserUpdated(SocketUser arg1, SocketUser arg2) => TryCatchLog(OnUserUpdated?.Invoke(arg1, arg2) ?? Task.CompletedTask);
    private Task Client_UserUnbanned(SocketUser arg1, SocketGuild arg2) => TryCatchLog(OnUserUnbanned?.Invoke(arg1, arg2) ?? Task.CompletedTask);
    private Task Client_UserBanned(SocketUser arg1, SocketGuild arg2) => TryCatchLog(OnUserBanned?.Invoke(arg1, arg2) ?? Task.CompletedTask);
    private Task Client_UserLeft(SocketGuild arg1, SocketUser arg2) => TryCatchLog(OnUserLeft?.Invoke(arg1, arg2) ?? Task.CompletedTask);
    private Task Client_UserJoined(SocketGuildUser arg) => TryCatchLog(OnUserJoined?.Invoke(arg) ?? Task.CompletedTask);
    private Task Client_GuildUpdated(SocketGuild arg1, SocketGuild arg2) => TryCatchLog(OnGuildUpdated?.Invoke(arg1, arg2) ?? Task.CompletedTask);
    private Task Client_GuildMembersDownloaded(SocketGuild arg) => TryCatchLog(OnGuildMembersDownloaded?.Invoke(arg) ?? Task.CompletedTask);
    private Task Client_GuildUnavailable(SocketGuild arg) => TryCatchLog(OnGuildUnavailable?.Invoke(arg) ?? Task.CompletedTask);
    private Task Client_LeftGuild(SocketGuild arg) => TryCatchLog(OnLeftGuild?.Invoke(arg) ?? Task.CompletedTask);
    private Task Client_ReactionRemoved(Cacheable<IUserMessage, ulong> arg1, Cacheable<IMessageChannel, ulong> arg2, SocketReaction arg3) => TryCatchLog(OnReactionRemoved?.Invoke(arg1, arg2, arg3) ?? Task.CompletedTask);
    private Task Client_JoinedGuild(SocketGuild arg) => TryCatchLog(OnJoinedGuild?.Invoke(arg) ?? Task.CompletedTask);
    private Task Client_RoleUpdated(SocketRole arg1, SocketRole arg2) => TryCatchLog(OnRoleUpdated?.Invoke(arg1, arg2) ?? Task.CompletedTask);
    private Task Client_RoleDeleted(SocketRole arg) => TryCatchLog(OnRoleDeleted?.Invoke(arg) ?? Task.CompletedTask);
    private Task Client_RoleCreated(SocketRole arg) => TryCatchLog(OnRoleCreated?.Invoke(arg) ?? Task.CompletedTask);
    private Task Client_ReactionsCleared(Cacheable<IUserMessage, ulong> arg1, Cacheable<IMessageChannel, ulong> arg2) => TryCatchLog(OnReactionsCleared?.Invoke(arg1, arg2) ?? Task.CompletedTask);

    public async Task Start(string token)
    {
        await client.LoginAsync(TokenType.Bot, token);
        await client.StartAsync();
    }
    private async Task Client_MessageReceived(SocketMessage arg)
    {
        if (arg is SocketUserMessage userMessage)
            await (OnMessageReceived?.Invoke(DiscordMessage.GetMessage(userMessage)) ?? Task.CompletedTask);
    }

    public event Func<DiscordMessage, Task>? OnMessageReceived;

    public event Func<Cacheable<IUserMessage, ulong>, Cacheable<IMessageChannel, ulong>, Task>? OnReactionsCleared;
    public event Func<SocketRole, Task>? OnRoleCreated;
    public event Func<SocketRole, Task>? OnRoleDeleted;
    public event Func<SocketRole, SocketRole, Task>? OnRoleUpdated;
    public event Func<SocketGuild, Task>? OnJoinedGuild;
    public event Func<Cacheable<IUserMessage, ulong>, Cacheable<IMessageChannel, ulong>, SocketReaction, Task>? OnReactionRemoved;
    public event Func<SocketGuild, Task>? OnLeftGuild;
    public event Func<SocketGuild, Task>? OnGuildUnavailable;
    public event Func<SocketGuild, Task>? OnGuildMembersDownloaded;
    public event Func<SocketGuild, SocketGuild, Task>? OnGuildUpdated;
    public event Func<SocketGuildUser, Task>? OnUserJoined;
    public event Func<SocketGuild, SocketUser, Task>? OnUserLeft;
    public event Func<SocketUser, SocketGuild, Task>? OnUserBanned;
    public event Func<SocketUser, SocketGuild, Task>? OnUserUnbanned;
    public event Func<SocketUser, SocketUser, Task>? OnUserUpdated;
    public event Func<Cacheable<SocketGuildUser, ulong>, SocketGuildUser, Task>? OnGuildMemberUpdated;
    public event Func<SocketUser, SocketVoiceState, SocketVoiceState, Task>? OnUserVoiceStateUpdated;
    public event Func<SocketVoiceServer, Task>? OnVoiceServerUpdated;
    public event Func<SocketSelfUser, SocketSelfUser, Task>? OnCurrentUserUpdated;
    public event Func<Cacheable<IUser, ulong>, Cacheable<IMessageChannel, ulong>, Task>? OnUserIsTyping;
    public event Func<SocketGuild, Task>? OnGuildAvailable;
    public event Func<Cacheable<IUserMessage, ulong>, Cacheable<IMessageChannel, ulong>, SocketReaction, Task>? OnReactionAdded;
    public event Func<IReadOnlyCollection<Cacheable<IMessage, ulong>>, Cacheable<IMessageChannel, ulong>, Task>? OnMessagesBulkDeleted;
    public event Func<Cacheable<IMessage, ulong>, SocketMessage, ISocketMessageChannel, Task>? OnMessageUpdated;
    public event Func<SocketChannel, Task>? OnChannelCreated;
    public event Func<SocketChannel, Task>? OnChannelDestroyed;
    public event Func<SocketGroupUser, Task>? OnRecipientRemoved;
    public event Func<SocketGroupUser, Task>? OnRecipientAdded;
    public event Func<Cacheable<IMessage, ulong>, Cacheable<IMessageChannel, ulong>, Task>? OnMessageDeleted;
    public event Func<SocketChannel, SocketChannel, Task>? OnChannelUpdated;
}