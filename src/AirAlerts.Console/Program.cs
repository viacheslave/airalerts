using AirAlerts.Application;
using AirAlerts.Jobs;
using AirAlerts.TelegramReporter;
using Microsoft.Extensions.DependencyInjection;

var sp = BuildServiceProvider();

var schedulerManager = sp.GetRequiredService<SchedulerManager>();
await schedulerManager.Setup(sp);

Console.WriteLine("Air Alerts chances app initialized");
Console.Write(Environment.NewLine);

Console.ReadLine();

await schedulerManager.Shutdown();

static ServiceProvider BuildServiceProvider()
{
  var services = new ServiceCollection();

  services.AddMemoryCache();
  services.AddSingleton<AppConfiguration>();
  services.AddSingleton<IAlertRulesProvider, AlertRulesProvider>();
  services.AddSingleton<IAlertsProcessor, AlertsProcessor>();
  services.AddSingleton<IAlertsProvider, AlertsProvider>();
  services.AddSingleton<IAlertsReporter, AlertsReporter>();
  services.AddSingleton<IReporter, ConsoleReporter>();
  services.AddSingleton<IReporter, TelegramReporter>();
  services.AddSingleton<SchedulerManager>();
  services.AddSingleton<SyncAlertsJob>();

  return services.BuildServiceProvider();
}
