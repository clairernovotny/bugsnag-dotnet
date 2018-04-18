using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Bugsnag.Controllers
{
  [Route("")]
  public class NotifyController : Controller
  {
    private readonly Store _store;

    public NotifyController(Store store)
    {
      _store = store;
    }

    [HttpPost]
    public async void Post()
    {
      using (var reader = new StreamReader(Request.Body))
      {
        var body = await reader.ReadToEndAsync();
        _store.Add(body);
      }
    }
  }

  [Route("requests")]
  public class RequestsController : Controller
  {
    private readonly Store _store;

    public RequestsController(Store store)
    {
      _store = store;
    }

    [HttpGet]
    public IEnumerable<string> Get()
    {
      return _store.Get();
    }

    [HttpDelete]
    public void Delete()
    {
      _store.Clear();
    }
  }

  public class Store
  {
    private readonly IList<string> _items;

    public Store()
    {
      _items = new List<string>();
    }

    public IEnumerable<string> Get()
    {
      return _items;
    }

    public void Add(string item)
    {
      _items.Add(item);
    }

    public void Clear()
    {
      _items.Clear();
    }
  }
}
