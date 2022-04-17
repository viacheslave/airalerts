using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace AirAlerts.Application;

/// <inheritdoc />
public class AlertRulesProvider : IAlertRulesProvider
{
  /// <inheritdoc />
  public Dictionary<string, int> GetRules()
  {
    var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
    var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"{assemblyName}.weights.json");

    var rulesContent = new StreamReader(stream).ReadToEnd();
    return JsonConvert.DeserializeObject<Dictionary<string, int>>(rulesContent);
  }
}
