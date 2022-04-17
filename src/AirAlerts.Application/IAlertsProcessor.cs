using System.Threading.Tasks;

namespace AirAlerts.Application;

/// <summary>
///   Alerts data processing
/// </summary>
public interface IAlertsProcessor
{
  /// <summary>
  ///   Calculates probability of air alert
  /// </summary>
  /// <returns></returns>
  Task<int> GetProbability();
}
