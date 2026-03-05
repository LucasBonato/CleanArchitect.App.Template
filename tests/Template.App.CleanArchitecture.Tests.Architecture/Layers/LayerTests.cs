using System.Reflection;
using NetArchTest.Rules;
using Xunit;
using TestResult = NetArchTest.Rules.TestResult;

namespace Template.App.CleanArchitecture.Tests.Architecture.Layers;

public class LayerTests : BaseTest
{
    private static readonly Assembly AppAssembly = typeof(Program).Assembly;

    [Fact]
    public void DomainLayer_ShouldNotHaveDependencyOn_OutsidePackages()
    {
        TestResult result = Types.InAssembly(AppAssembly)
            .That()
            .ResideInNamespace(DomainNamespace)
            .Should()
            .OnlyHaveDependenciesOn(
                "System",
                "System.*",
                DomainNamespace
            )
            .GetResult();

        result.ShouldBeSuccessful("Domain layer should not depend on outside packages");
    }

    [Fact]
    public void DomainLayer_ShouldNotHaveDependencyOn_Application()
    {
        TestResult result = Types.InAssembly(AppAssembly)
            .That()
            .ResideInNamespace(DomainNamespace)
            .ShouldNot()
            .HaveDependencyOn(ApplicationNamespace)
            .GetResult();

        result.ShouldBeSuccessful("Domain layer should not depend on application");
    }

    [Fact]
    public void DomainLayer_ShouldNotHaveDependencyOn_InfrastructureLayer()
    {
        TestResult result = Types.InAssembly(AppAssembly)
            .That()
            .ResideInNamespace(DomainNamespace)
            .ShouldNot()
            .HaveDependencyOn(InfrastructureNamespace)
            .GetResult();

        result.ShouldBeSuccessful("Domain layer should not depend on Infrastructure layer");
    }

    [Fact]
    public void DomainLayer_ShouldNotHaveDependencyOn_PresentationLayer()
    {
        TestResult result = Types.InAssembly(AppAssembly)
            .That()
            .ResideInNamespace(DomainNamespace)
            .ShouldNot()
            .HaveDependencyOn(PresentationNamespace)
            .GetResult();

        result.ShouldBeSuccessful("Domain layer should not depend on Presentation layer");
    }

    [Fact]
    public void ApplicationLayer_ShouldNotHaveDependencyOn_InfrastructureLayer()
    {
        TestResult result = Types.InAssembly(AppAssembly)
            .That()
            .ResideInNamespace(ApplicationNamespace)
            .ShouldNot()
            .HaveDependencyOn(InfrastructureNamespace)
            .GetResult();

        result.ShouldBeSuccessful("Application layer should not depend on Infrastructure layer");
    }

    [Fact]
    public void ApplicationLayer_ShouldNotHaveDependencyOn_PresentationLayer()
    {
        TestResult result = Types.InAssembly(AppAssembly)
            .That()
            .ResideInNamespace(ApplicationNamespace)
            .ShouldNot()
            .HaveDependencyOn(PresentationNamespace)
            .GetResult();

        result.ShouldBeSuccessful("Application layer should not depend on Presentation layer");
    }

    [Fact]
    public void InfrastructureLayer_ShouldNotHaveDependencyOn_PresentationLayer()
    {
        TestResult result = Types.InAssembly(AppAssembly)
            .That()
            .ResideInNamespace(InfrastructureNamespace)
            .ShouldNot()
            .HaveDependencyOn(PresentationNamespace)
            .GetResult();

        result.ShouldBeSuccessful("Infrastructure layer should not depend on Presentation layer");
    }

    [Fact]
    public void PresentationLayer_ShouldNotHaveDependencyOn_InfrastructureLayer()
    {
        TestResult result = Types.InAssembly(AppAssembly)
            .That()
            .ResideInNamespace(PresentationNamespace)
            .ShouldNot()
            .HaveDependencyOn(InfrastructureNamespace)
            .GetResult();

        result.ShouldBeSuccessful("Presentation layer should not depend on Infrastructure layer");
    }
}

