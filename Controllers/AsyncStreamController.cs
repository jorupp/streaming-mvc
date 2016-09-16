using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace StreamingMvc.Controllers
{
    /// <summary>
    /// This uses async/await (which work in a controller action) to avoid blocking a thread for this pass-through.
    /// By returning an EmptyResult, we're promising to do all the necessary work here in the controller, where we can use async/await.
    /// </summary>
    public class AsyncStreamController : AsyncController
    {
        public async Task<ActionResult> Index()
        {
            using (var c = new HttpClient())
            {
                var r = await c.GetAsync("http://google.com");
                var s = await r.Content.ReadAsStreamAsync();

                Response.ContentType = r.Content.Headers.ContentType.ToString();
                await s.CopyToAsync(Response.OutputStream);

                return new EmptyResult();
            }
        }

    }
}