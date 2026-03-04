using NetArchTest.Rules;

namespace ArchitectureTests;

public static class ArchTestExtensions
{
    public static void ShouldBeSuccessful(this TestResult result, string ruleDescription)
    {
        if (result.IsSuccessful)
            return;

        string failing = string.Join("\n", result.FailingTypeNames);

        throw new Xunit.Sdk.XunitException($"""
                                            Architecture rule violated: {ruleDescription}

                                            Failing types:
                                            {failing}
                                            """);
    }
}
