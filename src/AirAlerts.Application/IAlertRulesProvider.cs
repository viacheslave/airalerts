using System.Collections.Generic;

namespace AirAlerts.Application;

/// <summary>
///   Rules manager
/// </summary>
public interface IAlertRulesProvider
{
  /// <summary>
  ///   Brings rules for processing
  /// </summary>
  /// <returns></returns>
  Dictionary<string, int> GetRules();
}
