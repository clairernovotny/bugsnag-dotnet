using Xunit;

namespace Bugsnag.ConfigurationSection.Tests
{
  public class DefaultTests
  {
    public DefaultTests()
    {
      TestConfiguration = new ClientConfiguration(Configuration.Instance);
    }

    private IConfiguration TestConfiguration { get; }

    [Fact]
    public void ConfigurationIsNotNull()
    {
      Assert.NotNull(TestConfiguration);
    }

    [Fact]
    public void AppTypeIsNull()
    {
      Assert.Null(TestConfiguration.AppType);
    }

    [Fact]
    public void AppVersionIsNull()
    {
      Assert.Null(TestConfiguration.AppVersion);
    }

    [Fact]
    public void EndpointIsNotNull()
    {
      Assert.NotNull(TestConfiguration.Endpoints.Notify);
    }

    [Fact]
    public void NotifyReleaseStagesIsNull()
    {
      Assert.Null(TestConfiguration.NotifyReleaseStages);
    }

    [Fact]
    public void ReleaseStageIsNull()
    {
      Assert.Null(TestConfiguration.ReleaseStage);
    }

    [Fact]
    public void ProjectRootsIsNull()
    {
      Assert.Null(TestConfiguration.ProjectRoots);
    }

    [Fact]
    public void ProjectNamespacesIsNull()
    {
      Assert.Null(TestConfiguration.ProjectNamespaces);
    }

    [Fact]
    public void IgnoreClassesIsNull()
    {
      Assert.Null(TestConfiguration.IgnoreClasses);
    }

    [Fact]
    public void MetadataFiltersIsNull()
    {
      Assert.Null(TestConfiguration.MetadataFilters);
    }

    [Fact]
    public void GlobalMetadataIsNull()
    {
      Assert.Null(TestConfiguration.GlobalMetadata);
    }

    [Fact]
    public void AutoCaptureSessionsIsTryue()
    {
      Assert.True(TestConfiguration.AutoCaptureSessions);
    }

    [Fact]
    public void SessionsEndpointIsNotNull()
    {
      Assert.NotNull(TestConfiguration.Endpoints.Sessions);
    }

    [Fact]
    public void ProxyIsNull()
    {
      Assert.Null(TestConfiguration.Proxy);
    }

    [Fact]
    public void AutoNotifyIsTrue()
    {
      Assert.True(TestConfiguration.AutoNotify);
    }

    [Fact]
    public void MaximumBreadcrumbsIsSet()
    {
      Assert.Equal(25, TestConfiguration.MaximumBreadcrumbs);
    }
  }
}
