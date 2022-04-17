using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirAlerts.Application;

/// <inheritdoc />
public class AlertsReporter : IAlertsReporter
{
  private readonly IEnumerable<IReporter> _reporters;
  private readonly IMemoryCache _memoryCache;

  public AlertsReporter(IEnumerable<IReporter> reporters, IMemoryCache memoryCache)
  {
    _reporters = reporters;
    _memoryCache = memoryCache;
  }

  /// <inheritdoc />
  public async Task Report(int value)
  {
    _memoryCache.TryGetValue("value", out var cached);

    if (cached is null || value != (int)cached)
    {
      foreach (var reporter in _reporters)
      {
        await reporter.Report(value, cached is not null ? (int)cached : default);
      }
    }

    _memoryCache.Set("value", value);
  }
}
