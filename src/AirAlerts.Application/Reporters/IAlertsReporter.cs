using System.Threading.Tasks;

namespace AirAlerts.Application;

/// <summary>
///   Reporting
/// </summary>
public interface IAlertsReporter
{
  /// <summary>
  ///   Triggers reporting
  /// </summary>
  /// <param name="value">Current probability value</param>
  /// <returns></returns>
  Task Report(int value);
}
