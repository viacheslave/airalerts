namespace AirAlerts.Application;

public class AppConfiguration
{
  /// <summary>
  ///   Telegram Bot API key
  /// </summary>
  public string TelegramApiKey { get; internal set; } = ""; // Put Telegram Bot API Key here

  /// <summary>
  ///   Telegram chat ID to send updates to
  /// </summary>
  public string TelegramChatId { get; internal set; } = ""; // Put Telegram Chat ID here
}
