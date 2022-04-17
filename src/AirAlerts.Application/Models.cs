using System.Collections.Generic;

namespace AirAlerts.Application;

/// <summary>
///   Current alerts data
/// </summary>
public class Alerts
{
  /// <summary>
  ///   Enabled regions (states and districts)
  /// </summary>
  public IReadOnlyCollection<string> Regions { get; init; }

  /// <summary>
  ///   General map from states to districts
  /// </summary>
  public Dictionary<string, IReadOnlyCollection<string>> RegionMap { get; init; }
}
