using System;
using System.Threading.Tasks;

namespace AirAlerts.Application;

/// <inheritdoc />
public class ConsoleReporter : IReporter
{
  /// <inheritdoc />
  public async Task Report(int value, int prev) => 
    Console.WriteLine($"{DateTime.Now}: {value}% ({prev}%)");
}