using System;
using System.Linq;
using System.Threading.Tasks;

namespace AirAlerts.Application;

/// <inheritdoc />
public class AlertsProcessor : IAlertsProcessor
{
  private readonly IAlertsProvider _alertsProvider;
  private readonly IAlertRulesProvider _alertRulesProvider;

  public AlertsProcessor(IAlertsProvider alertsProvider, IAlertRulesProvider alertRulesProvider)
  {
    _alertsProvider = alertsProvider;
    _alertRulesProvider = alertRulesProvider;
  }

  /// <inheritdoc />
  public async Task<int> GetProbability()
  {
    var rules = _alertRulesProvider.GetRules();

    var alerts = await _alertsProvider.GetAlerts();
    var regions = alerts.Regions.ToHashSet();

    // clean up regions
    // remove child districts if any
    foreach (var r in alerts.RegionMap)
    {
      var state = r.Key;
      var districts = r.Value;

      if (regions.Contains(state))
      {
        regions.ExceptWith(districts);
      }
    }

    // total is: states plus Kyiv
    var totalPoints = rules.Where(x => !x.Key.Contains("район"))
      .Sum(x => x.Value);

    if (totalPoints == 0)
    {
      return 0;
    }

    var points = 0;

    foreach (var rule in rules)
    {
      if (regions.Contains(rule.Key))
      {
        points += rule.Value;
      }
    }

    return (int)Math.Floor(100d * points / totalPoints);
  }
}
