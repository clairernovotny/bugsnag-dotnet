using System;
using System.Configuration;

namespace Bugsnag.ConfigurationSection
{
  public class Configuration : System.Configuration.ConfigurationSection
  {
    public static Configuration Instance { get; }
      = ConfigurationManager.GetSection("bugsnag") as Configuration
      ?? new Configuration();

    private const string apiKey = "apiKey";

    [ConfigurationProperty(apiKey, IsRequired = true)]
    internal string ApiKey
    {
      get
      {
        switch (ElementInformation.Properties[apiKey].ValueOrigin)
        {
          case PropertyValueOrigin.Inherited:
          case PropertyValueOrigin.SetHere:
            return this[apiKey] as string;
          default:
            return null;
        }
      }
    }

    private const string appType = "appType";

    [ConfigurationProperty(appType, IsRequired = false)]
    internal string AppType
    {
      get
      {
        switch (ElementInformation.Properties[appType].ValueOrigin)
        {
          case PropertyValueOrigin.Inherited:
          case PropertyValueOrigin.SetHere:
            return this[appType] as string;
          default:
            return null;
        }
      }
    }

    private const string appVersion = "appVersion";

    [ConfigurationProperty(appVersion, IsRequired = false)]
    internal string AppVersion
    {
      get
      {
        switch (ElementInformation.Properties[appVersion].ValueOrigin)
        {
          case PropertyValueOrigin.Inherited:
          case PropertyValueOrigin.SetHere:
            return this[appVersion] as string;
          default:
            return null;
        }
      }
    }

    private const string notify = "notify";

    [ConfigurationProperty(notify, IsRequired = false, DefaultValue = Bugsnag.Endpoints.DefaultEndpoint)]
    internal Uri Notify
    {
      get { return this[notify] as Uri; }
    }

    private const string autoNotify = "autoNotify";

    [ConfigurationProperty(autoNotify, IsRequired = false, DefaultValue = true)]
    internal bool AutoNotify
    {
      get
      {
        return (bool)this[autoNotify];
      }
    }

    private const string notifyReleaseStages = "notifyReleaseStages";

    [ConfigurationProperty(notifyReleaseStages, IsRequired = false)]
    internal string NotifyReleaseStages
    {
      get
      {
        switch (ElementInformation.Properties[notifyReleaseStages].ValueOrigin)
        {
          case PropertyValueOrigin.Inherited:
          case PropertyValueOrigin.SetHere:
            return this[notifyReleaseStages] as string;
          default:
            return null;
        }
      }
    }

    private const string releaseStage = "releaseStage";

    [ConfigurationProperty(releaseStage, IsRequired = false)]
    internal string ReleaseStage
    {
      get
      {
        switch (ElementInformation.Properties[releaseStage].ValueOrigin)
        {
          case PropertyValueOrigin.Inherited:
          case PropertyValueOrigin.SetHere:
            return this[releaseStage] as string;
          default:
            return null;
        }
      }
    }

    private const string projectRoots = "projectRoots";

    [ConfigurationProperty(projectRoots, IsRequired = false)]
    internal string ProjectRoots
    {
      get
      {
        switch (ElementInformation.Properties[projectRoots].ValueOrigin)
        {
          case PropertyValueOrigin.Inherited:
          case PropertyValueOrigin.SetHere:
            return this[projectRoots] as string;
          default:
            return null;
        }
      }
    }

    private const string projectNamespaces = "projectNamespaces";

    [ConfigurationProperty(projectNamespaces, IsRequired = false)]
    internal string ProjectNamespaces
    {
      get
      {
        switch (ElementInformation.Properties[projectNamespaces].ValueOrigin)
        {
          case PropertyValueOrigin.Inherited:
          case PropertyValueOrigin.SetHere:
            return this[projectNamespaces] as string;
          default:
            return null;
        }
      }
    }

    private const string ignoreClasses = "ignoreClasses";

    [ConfigurationProperty(ignoreClasses, IsRequired = false)]
    internal ExtendedIgnoreClassCollection IgnoreClasses
    {
      get
      {
        return (ExtendedIgnoreClassCollection)this[ignoreClasses];
      }
    }

    private const string metadataFilters = "metadataFilters";

    [ConfigurationProperty(metadataFilters, IsRequired = false)]
    internal string MetadataFilters
    {
      get
      {
        switch (ElementInformation.Properties[metadataFilters].ValueOrigin)
        {
          case PropertyValueOrigin.Inherited:
          case PropertyValueOrigin.SetHere:
            return this[metadataFilters] as string;
          default:
            return null;
        }
      }
    }

    private const string metadata = "metadata";

    [ConfigurationProperty(metadata, IsRequired = false)]
    internal GlobalMetadataCollection GlobalMetadata
    {
      get { return (GlobalMetadataCollection)this[metadata]; }
    }

    private const string autoCaptureSessions = "autoCaptureSessions";

    [ConfigurationProperty(autoCaptureSessions, IsRequired = false, DefaultValue = true)]
    internal bool AutoCaptureSessions
    {
      get
      {
        return (bool)this[autoCaptureSessions];
      }
    }

    private const string sessions = "sessions";

    [ConfigurationProperty(sessions, IsRequired = false, DefaultValue = Bugsnag.Endpoints.DefaultSessionEndpoint)]
    internal Uri Sessions
    {
      get { return this[sessions] as Uri; }
    }

    private const string proxyAddress = "proxyAddress";

    [ConfigurationProperty(proxyAddress, IsRequired = false)]
    internal string ProxyAddress
    {
      get { return this[proxyAddress] as string; }
    }

    private const string proxyUsername = "proxyUsername";

    [ConfigurationProperty(proxyUsername, IsRequired = false)]
    internal string ProxyUsername
    {
      get { return this[proxyUsername] as string; }
    }

    private const string proxyPassword = "proxyPassword";

    [ConfigurationProperty(proxyPassword, IsRequired = false)]
    internal string ProxyPassword
    {
      get { return this[proxyPassword] as string; }
    }

    private const string maximumBreadcrumbs = "maximumBreadcrumbs";

    [ConfigurationProperty(maximumBreadcrumbs, IsRequired = false, DefaultValue = 25)]
    internal int MaximumBreadcrumbs
    {
      get { return (int)this[maximumBreadcrumbs]; }
    }
  }
}
