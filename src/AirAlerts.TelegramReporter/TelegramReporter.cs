using AirAlerts.Application;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace AirAlerts.TelegramReporter;

/// <inheritdoc />
public class TelegramReporter : IReporter
{
  private readonly TelegramBotClient _botClient;
  private readonly AppConfiguration _appConfiguration;

  public TelegramReporter(AppConfiguration appConfiguration)
  {
    _appConfiguration = appConfiguration;

    _botClient = new TelegramBotClient(_appConfiguration.TelegramApiKey);

    var receiverOptions = new ReceiverOptions
    {
      // receive all update types
      AllowedUpdates = { } 
    };

    _botClient.StartReceiving(new UpdateHandler(), receiverOptions);
  }

  /// <inheritdoc />
  public async Task Report(int value, int prev)
  {
    var message = $"{value}% ({prev}%)";

    if (value > 30)
    {
      message += " Можлива тривога!";
    }

    await _botClient.SendTextMessageAsync(
      new ChatId(_appConfiguration.TelegramChatId),
      message);
  }

  internal class UpdateHandler : IUpdateHandler
  {
    public Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken) => 
      Task.CompletedTask;

    public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
      if (update.Message?.Entities?.Length > 0 && update.Message.Entities[0].Type == MessageEntityType.BotCommand)
      {
        var from = update.Message.From;
        if (from is null)
        {
          return;
        }

        var chatId = new ChatId(from.Id);

        switch (update.Message.Text)
        {
          case "/test":

            await botClient.SendTextMessageAsync(
              chatId,
              "Слава ЗСУ!");

            break;
        }
      }
    }
  }
}
