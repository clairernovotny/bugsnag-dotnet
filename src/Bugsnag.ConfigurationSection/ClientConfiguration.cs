using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Bugsnag.ConfigurationSection
{
  public class ClientConfiguration : IConfiguration
  {
    public ClientConfiguration(Configuration configuration)
    {
      ApiKey = configuration.ApiKey;
      AutoNotify = configuration.AutoNotify;
      ReleaseStage = configuration.ReleaseStage;
      if (configuration.NotifyReleaseStages != null)
      {
        NotifyReleaseStages = configuration.NotifyReleaseStages.Split(',');
      }
      AppVersion = configuration.AppVersion;
      AppType = configuration.AppType;
      if (configuration.ProjectRoots != null)
      {
        ProjectRoots = configuration.ProjectRoots.Split(',');
      }
      if (configuration.ProjectNamespaces != null)
      {
        ProjectNamespaces = configuration.ProjectNamespaces.Split(',');
      }
      if (configuration.IgnoreClasses.Count > 0)
      {
        IgnoreClasses = configuration.IgnoreClasses.Cast<ExtendedIgnoreClass>()
          .Select(c => Type.GetType(c.Name))
          .Where(c => c != null)
          .ToArray();
      }
      if (configuration.GlobalMetadata.Count > 0)
      {
        GlobalMetadata = configuration.GlobalMetadata.Cast<GlobalMetadataItem>()
          .Select(i => new KeyValuePair<string, object>(i.Key, i.Value))
          .ToArray();
      }
      if (configuration.MetadataFilters != null)
      {
        MetadataFilters = configuration.MetadataFilters.Split(',');
      }
      AutoCaptureSessions = configuration.AutoCaptureSessions;
      SessionTrackingInterval = TimeSpan.FromSeconds(60);
      try
      {
        if (!Polyfills.String.IsNullOrWhiteSpace(configuration.ProxyAddress))
        {
          if (!Polyfills.String.IsNullOrWhiteSpace(configuration.ProxyUsername) && !Polyfills.String.IsNullOrWhiteSpace(configuration.ProxyPassword))
          {
            var credential = new NetworkCredential(configuration.ProxyUsername, configuration.ProxyPassword);
            Proxy = new WebProxy(configuration.ProxyAddress, false, null, credential);
          }
          else
          {
            Proxy = new WebProxy(configuration.ProxyAddress);
          }
        }
      }
      catch (UriFormatException)
      {
      }
      MaximumBreadcrumbs = configuration.MaximumBreadcrumbs;
      Endpoints = new Endpoints(configuration.Notify, configuration.Sessions);
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
