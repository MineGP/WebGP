using Discord;
using System.Diagnostics.CodeAnalysis;

namespace WebGP.Diaka;

public record DonateData(string UserName, int Amount, IMessage Message)
{
    public static bool TryParse(IMessage message, [NotNullWhen(true)] out DonateData? data)
    {
        data = null;
        try
        {
            if (message == null) return false;
            string[] lines = message.Content.Replace("*", "").Split('\n');
            string name = lines[0].Split(':')[1][1..];
            string amount = lines[1].Split(':')[1][1..];
            if (!amount.EndsWith(" UAH")) return false;
            amount = amount[..^4];
            data = new DonateData(name, int.Parse(amount), message);
            return true;
        }
        catch
        {
            return false;
        }
    }
}