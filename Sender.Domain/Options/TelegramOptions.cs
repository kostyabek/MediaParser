namespace Sender.Domain.Options;

/// <summary>
/// Options for Telegram API client.
/// </summary>
public class TelegramOptions
{
    /// <summary>
    /// Gets or sets bot token.
    /// </summary>
    public string BotToken { get; set; }

    /// <summary>
    /// Gets or sets chat id.
    /// </summary>
    public string ChatId { get; set; }
}