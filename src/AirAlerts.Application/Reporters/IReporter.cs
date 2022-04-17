using System.Threading.Tasks;

namespace AirAlerts.Application;

/// <summary>
///  Reporter contract
/// </summary>
public interface IReporter
{
  /// <summary>
  ///   Reports current and previous value
  /// </summary>
  /// <param name="value">Current value</param>
  /// <param name="prev">Previous value</param>
  /// <returns></returns>
  Task Report(int value, int prev);
}
