using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Bugsnag.AspNet.Core;

namespace aspnetcore
{
  public class Startup
  {
    ILogger _logger;

    public Startup(ILoggerFactory loggerFactory)
    {
      _logger = loggerFactory.CreateLogger<Startup>();
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddBugsnag(configuration => {
        configuration.ApiKey = Environment.GetEnvironmentVariable("MAZE_API_KEY");
        configuration.Endpoint = new Uri(Environment.GetEnvironmentVariable("MAZE_ENDPOINT"));
      });

      services.AddRouting();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      var routes = new RouteBuilder(app);

      routes.MapGet("unhandled", context => {
        throw new System.Exception("wild eeep");
      });

      routes.MapGet("", context => {
        return context.Response.WriteAsync("Hello world");
      });

      app.UseRouter(routes.Build());
    }
  }
}
