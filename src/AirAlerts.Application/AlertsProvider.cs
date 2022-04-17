using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flurl.Http;

namespace AirAlerts.Application;

/// <inheritdoc />
public class AlertsProvider : IAlertsProvider
{
  /// <inheritdoc />
  public async Task<Alerts> GetAlerts()
  {
    const string uri = "https://emapa.fra1.cdn.digitaloceanspaces.com/statuses.json";

    var alerts = await uri.GetJsonAsync<AlertsDto>();

    var regionMap = alerts.states
      .Select(x => (x.Key, Districts: x.Value.districts.Select(d => d.Key)))
      .ToDictionary(x => x.Key, x => (IReadOnlyCollection<string>)x.Districts.ToList().AsReadOnly());

    var states = alerts.states
      .Where(x => x.Value.enabled)
      .Select(x => x.Key);

    var districts = alerts.states
      .SelectMany(x => x.Value.districts)
      .Where(x => x.Value.enabled)
      .Select(x => x.Key);

    return new Alerts()
    {
      Regions = states.Concat(districts).ToList().AsReadOnly(),
      RegionMap = regionMap
    };
  }
}

internal class AlertsDto
{
  public int version { get; set; }

  public Dictionary<string, StateDto> states { get; set; }
}

internal class StateDto
{
  public string type { get; set; }

  public bool enabled { get; set; }

  public DateTime? enabled_at { get; set; } 

  public DateTime? disabled_at { get; set; } 

  public Dictionary<string, DistrictDto> districts { get; set; }
}

internal class DistrictDto
{
  public string type { get; set; }

  public bool enabled { get; set; }

  public DateTime? enabled_at { get; set; }

  public DateTime? disabled_at { get; set; }
}
