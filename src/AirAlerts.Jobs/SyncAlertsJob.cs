using AirAlerts.Application;
using Quartz;

namespace AirAlerts.Jobs;

[DisallowConcurrentExecution]
public class SyncAlertsJob : IJob
{
  private readonly IAlertsReporter _alertsReporter;
  private readonly IAlertsProcessor _alertsProcessor;

  public SyncAlertsJob(IAlertsReporter alertsReporter, IAlertsProcessor alertsProcessor)
  {
    _alertsReporter = alertsReporter;
    _alertsProcessor = alertsProcessor;
  }

  public async Task Execute(IJobExecutionContext context)
  {
    var value = await _alertsProcessor.GetProbability();
    
    await _alertsReporter.Report(value);
  }
}
