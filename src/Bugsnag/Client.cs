using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Bugsnag.Payload;

namespace Bugsnag
{
  public class Client : IClient
  {
    private readonly IConfiguration _configuration;

    private readonly IDelivery _delivery;

    private readonly IBreadcrumbs _breadcrumbs;

    private readonly ISessionTracker _sessionTracking;

    private readonly List<Middleware> _middleware;

    private readonly object _middlewareLock = new object();

    protected static Middleware[] DefaultInternalMiddleware = new Middleware[] {
      Bugsnag.InternalMiddleware.ReleaseStageFilter,
      Bugsnag.InternalMiddleware.RemoveIgnoredExceptions,
      Bugsnag.InternalMiddleware.RemoveProjectRoots,
      Bugsnag.InternalMiddleware.DetectInProjectNamespaces,
      Bugsnag.InternalMiddleware.AttachGlobalMetadata,
      Bugsnag.InternalMiddleware.DetermineDefaultContext,
    };

    public Client(IConfiguration configuration) : this(configuration, ThreadQueueDelivery.Instance, new Breadcrumbs(configuration), new SessionTracker(configuration))
    {

    }

    public Client(IConfiguration configuration, IDelivery delivery, IBreadcrumbs breadcrumbs, ISessionTracker sessionTracking)
    {
      _configuration = configuration;
      _delivery = delivery;
      _breadcrumbs = breadcrumbs;
      _sessionTracking = sessionTracking;
      _middleware = new List<Middleware>();

      UnhandledException.Instance.ConfigureClient(this, configuration);
    }

    public IConfiguration Configuration => _configuration;

    public IBreadcrumbs Breadcrumbs => _breadcrumbs;

    public ISessionTracker SessionTracking => _sessionTracking;

    public void BeforeNotify(Middleware middleware)
    {
      lock (_middlewareLock)
      {
        _middleware.Add(middleware);
      }
    }

    protected Middleware[] InternalMiddleware => DefaultInternalMiddleware;

    public void Notify(System.Exception exception, Request request = null)
    {
      Notify(exception, HandledState.ForHandledException(), request);
    }

    public void Notify(System.Exception exception, Severity severity, Request request = null)
    {
      Notify(exception, HandledState.ForUserSpecifiedSeverity(severity), request);
    }

    public void Notify(System.Exception exception, HandledState severity, Request request = null)
    {
      var report = new Report(_configuration, exception, severity, Breadcrumbs.Retrieve().ToArray(), SessionTracking.CurrentSession, request);

      Notify(report);
    }

    public void Notify(Report report)
    {
      foreach (var middleware in InternalMiddleware)
      {
        try
        {
          middleware(report);
        }
        catch (System.Exception exception)
        {
          Trace.WriteLine(exception);
        }
      }

      lock (_middlewareLock)
      {
        foreach (var middleware in _middleware)
        {
          try
          {
            middleware(report);
          }
          catch (System.Exception exception)
          {
            Trace.WriteLine(exception);
          }
        }
      }

      if (!report.Ignored)
      {
        Bugsnag.InternalMiddleware.ApplyMetadataFilters(report);

        _delivery.Send(report);

        Breadcrumbs.Leave(Breadcrumb.FromReport(report));

        if (SessionTracking.CurrentSession != null)
        {
          SessionTracking.CurrentSession.AddException(report);
        }
      }
    }
  }
}
