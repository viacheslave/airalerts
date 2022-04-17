using Quartz;
using Quartz.Spi;

namespace AirAlerts.Jobs;

public class SchedulerJobFactory : IJobFactory
{
  private readonly IServiceProvider _serviceProvider;

  public SchedulerJobFactory(IServiceProvider serviceProvider)
  {
    _serviceProvider = serviceProvider;
  }

  public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
  {
    var job = _serviceProvider.GetService(typeof(SyncAlertsJob));
    return job as IJob;
  }

  public void ReturnJob(IJob job)
  {
    var disposable = job as IDisposable;
    disposable?.Dispose();
  }
}
