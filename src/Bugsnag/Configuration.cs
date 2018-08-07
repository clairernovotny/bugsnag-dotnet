using System;
using System.Collections.Generic;
using System.Net;

namespace Bugsnag
{
  public class Endpoints : IEndpoints
  {
    public const string DefaultEndpoint = "https://notify.bugsnag.com";

    public const string DefaultSessionEndpoint = "https://sessions.bugsnag.com";

    internal Endpoints() : this(new Uri(DefaultEndpoint), new Uri(DefaultSessionEndpoint))
    {

    }

    public Endpoints(Uri notify, Uri sessions)
    {
      Notify = notify;
      Sessions = sessions;
    }

    public Uri Notify { get; }

    public Uri Sessions { get; }
  }
  /// <summary>
  /// An in memory implementation of the IConfiguration interface, with default
  /// values populated.
  /// </summary>
  public class Configuration : IConfiguration
  {
    public Configuration() : this(string.Empty)
    {

    }

    public Configuration(string apiKey)
    {
      ApiKey = apiKey;
      AutoNotify = true;
      AutoCaptureSessions = true;
      Endpoints = new Endpoints();
      SessionTrackingInterval = TimeSpan.FromSeconds(60);
      MetadataFilters = new[] { "password", "Authorization" };
      MaximumBreadcrumbs = 25;
      ReleaseStage = Environment.GetEnvironmentVariable("BUGSNAG_RELEASE_STAGE");
    }

    public void SetEndpoints(Uri notify, Uri sessions)
    {
      Endpoints = new Endpoints(notify, sessions);
    }

    public string ApiKey { get; set; }

    public bool AutoNotify { get; set; }

    public string ReleaseStage { get; set; }

    public string[] NotifyReleaseStages { get; set; }

    public string AppVersion { get; set; }

    public string AppType { get; set; }

    public string[] ProjectRoots { get; set; }

    public string[] ProjectNamespaces { get; set; }

    public Type[] IgnoreClasses { get; set; }

    public KeyValuePair<string, object>[] GlobalMetadata { get; set; }

    public string[] MetadataFilters { get; set; }

    public bool AutoCaptureSessions { get; set; }

    public TimeSpan SessionTrackingInterval { get; set; }

    public IWebProxy Proxy { get; set; }

    public int MaximumBreadcrumbs { get; set; }

    public IEndpoints Endpoints { get; set; }
  }
}
