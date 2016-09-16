using System;
using System.IO;
using System.Web.Mvc;

namespace StreamingMvc.Results
{
    /// <summary>
    /// Copies a supplied stream to the response stream until the supplied stream ends.
    /// The supplied stream and extra disposables passed will be disposed.
    /// 
    /// Note: this is not async-enabled, as ActionResult doesn't have an async version.
    /// </summary>
    public class StreamResult : ActionResult
    {
        private readonly Stream _stream;
        private readonly string _contentType;
        private readonly IDisposable[] _disposables;

        public StreamResult(Stream stream, string contentType, params IDisposable[] disposables)
        {
            _stream = stream;
            _contentType = contentType;
            _disposables = disposables;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var response = context.HttpContext.Response;
            response.ContentType = _contentType;
            using (_stream)
            {
                // this *will* block....
                _stream.CopyTo(response.OutputStream);
            }

            foreach (var d in _disposables ?? new IDisposable[0])
            {
                d?.Dispose();
            }
        }
    }
}