using System.Threading.Tasks;

namespace AirAlerts.Application;

/// <summary>
///   Alerts data retrieval
/// </summary>
public interface IAlertsProvider
{
  /// <summary>
  ///   Gets current alerts data
  /// </summary>
  /// <returns></returns>
  Task<Alerts> GetAlerts();
}
