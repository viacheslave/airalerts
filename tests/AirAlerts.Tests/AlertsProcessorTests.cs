using AirAlerts.Application;
using NSubstitute;
using Xunit;

namespace AirAlerts.Tests;

public class AlertsProcessorTests
{
  private readonly Alerts _alerts = new()
  {
    Regions = new List<string>
    {
      "Область 1",
      "Область 2", "район 21",
      "район 41",
      "м. Київ"
    },

    RegionMap = new Dictionary<string, IReadOnlyCollection<string>>
    {
      ["Область 1"] = new List<string> { "район 11", "район 12", },
      ["Область 2"] = new List<string> { "район 21", "район 22", "район 23", },
      ["Область 3"] = new List<string> { "район 31", },
      ["Область 4"] = new List<string> { "район 41", },
      ["м. Київ"] = new List<string>(),
    }
  };

  [Fact]
  public async Task GetProbability_Tests()
  {
    var rules = new Dictionary<string, int>
    {
      ["Область 3"] = 100
    };

    await AssertProbability(rules, 0);

    rules = new Dictionary<string, int>
    {
      ["район 31"] = 100
    };

    await AssertProbability(rules, 0);

    rules = new Dictionary<string, int>
    {
      ["м. Київ"] = 100
    };

    await AssertProbability(rules, 100);

    rules = new Dictionary<string, int>
    {
      ["Область 3"] = 100,
      ["район 31"] = 100,

      ["Область 4"] = 200,
      ["район 41"] = 200,
    };

    await AssertProbability(rules, (int) Math.Floor(100.0 * 200 / (200 + 100))); // 66%

    rules = new Dictionary<string, int>
    {
      ["Область 1"] = 100,
      ["район 11"] = 50,
      ["район 12"] = 50,

      ["Область 2"] = 200,
      ["район 21"] = 80,
      ["район 22"] = 70,
      ["район 23"] = 50,

      ["Область 3"] = 1000,
      ["район 31"] = 1000,
    };

    await AssertProbability(rules, (int)Math.Floor(100.0 * (100 + 200) / (1000 + 200 + 100))); // 23%
  }

  private async Task AssertProbability(Dictionary<string, int> rules, int expected)
  {
    var alertsProcessor = GetSUT(rules);

    var result = await alertsProcessor.GetProbability();

    Assert.Equal(expected, result);
  }

  private AlertsProcessor GetSUT(Dictionary<string, int> rules)
  {
    var alertsProvider = Substitute.For<IAlertsProvider>();
    var alertRulesProvider = Substitute.For<IAlertRulesProvider>();

    var alertsProcessor = new AlertsProcessor(alertsProvider, alertRulesProvider);

    alertsProvider.GetAlerts()
      .Returns(_ => Task.FromResult(_alerts));

    alertRulesProvider.GetRules()
      .Returns(rules);

    return alertsProcessor;
  }
}
