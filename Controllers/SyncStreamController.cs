using System.Net.Http;
using System.Web.Mvc;
using StreamingMvc.Results;

namespace StreamingMvc.Controllers
{
    /// <summary>
    /// Not only does this block a thread while initializing the connection, the ActionResult it returns blocks the thread as well, as it *can't* be async.
    /// </summary>
    public class SyncStreamController : Controller
    {
        public ActionResult Index()
        {
            var c = new HttpClient();
            var r = c.GetAsync("http://google.com").Result;
            var s = r.Content.ReadAsStreamAsync().Result;
            return new StreamResult(s, r.Content.Headers.ContentType.ToString(), c);
        }
    }
}