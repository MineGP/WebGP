using Discord;
using Discord.Rest;
using Microsoft.Extensions.Configuration;
using System.Collections.Concurrent;
using WebGP.Application.Common.Interfaces;
using WebGP.Diaka.Discord;

namespace WebGP.Diaka;

public class DiakaListener
{
    private static readonly DiscordConfig DiscordConfig = new DiscordConfig()
    {
        LogLevel = LogSeverity.Debug,
        DefaultRetryMode = RetryMode.AlwaysRetry
    };

    private readonly IDonateRepository _repository;

    private readonly string _token;
    private readonly ulong _channelID;

    public static bool IsEnable(IConfiguration configuration) 
        => !string.IsNullOrWhiteSpace(configuration.GetRequiredSection("Diaka").GetValue<string>("Token"));

    public DiakaListener(IConfiguration config, IDonateRepository repository)
    {
        IConfigurationSection diaka = config.GetRequiredSection("Diaka");
        _token = diaka.GetValue<string>("Token")!;
        _channelID = diaka.GetValue<ulong>("ChannelID");

        _repository = repository;
    }

    public async Task StartListen(CancellationToken cancellationToken)
    {
        DiscordRestClient client = new DiscordRestClient(DiscordConfig.GetConfig<DiscordRestConfig>());
        DiscordListener listener = new DiscordListener(DiscordConfig);
        await client.LoginAsync(TokenType.Bot, _token);
        ITextChannel channel = (ITextChannel)await client.GetChannelAsync(_channelID);
        ConcurrentQueue<DonateData> queue = new ConcurrentQueue<DonateData>();
        await foreach (IMessage message in channel.GetMessagesAsync().Flatten())
        {
            if (message is IUserMessage userMessage && userMessage.Author.IsWebhook)
            {
                if (userMessage.Reactions.TryGetFirst(Utils.OK_MARK, out ReactionMetadata m_ok) && m_ok.IsMe) continue;
                if (userMessage.Reactions.TryGetFirst(Utils.ERROR_MARK, out ReactionMetadata m_error) && m_error.IsMe) continue;
                if (DonateData.TryParse(userMessage, out DonateData? data)) queue.Enqueue(data);
                else await userMessage.AddReactionAsync(Utils.ERROR_MARK[0]);
            }
        }
        listener.OnMessageReceived += async (args) =>
        {
            try
            {
                if (await channel.GetMessageAsync(args.MessageID) is not IUserMessage message || !message.Author.IsWebhook) return;
                if (DonateData.TryParse(message, out DonateData? data)) queue.Enqueue(data);
                else await message.AddReactionAsync(Utils.ERROR_MARK[0]);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        };
        await listener.Start(_token);

        while (true)
        {
            try
            {
                await _repository.OnUpdatePreDonateAsync(cancellationToken);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            if (!queue.TryDequeue(out DonateData? data))
            {
                await Task.Delay(1000, cancellationToken);
                continue;
            }

            string userName = data.UserName;
            int amount = data.Amount;
            string? uuid = await Utils.GetUserUUID(data.UserName);
            try
            {
                await data.Message.AddReactionAsync(Utils.OK_MARK[0]);
                Console.WriteLine("New donate: " + data.UserName + " " + data.Amount + "₴");

                await _repository.AddPreDonateAsync(userName, uuid, amount, "DIAKA", cancellationToken);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
