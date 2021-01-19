using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Orbital.Core
{
    [UsedImplicitly]
    internal class HttpClient : IHttpClient
    {
        readonly System.Net.Http.HttpClient _builtInHttpClient;

        public Uri BaseAddress => _builtInHttpClient.BaseAddress;

        public HttpRequestHeaders DefaultRequestHeaders => _builtInHttpClient.DefaultRequestHeaders;

        public long MaxResponseContentBufferSize => _builtInHttpClient.MaxResponseContentBufferSize;

        public TimeSpan Timeout => _builtInHttpClient.Timeout;

        public HttpClient() { _builtInHttpClient = new System.Net.Http.HttpClient(); }

        public void CancelPendingRequests() { _builtInHttpClient.CancelPendingRequests(); }

        public Task<HttpResponseMessage> DeleteAsync(Uri requestUri) { return _builtInHttpClient.DeleteAsync(requestUri); }

        public Task<HttpResponseMessage> DeleteAsync(Uri requestUri, CancellationToken cancellationToken)
        {
            return _builtInHttpClient.DeleteAsync(requestUri, cancellationToken);
        }

        public Task<HttpResponseMessage> DeleteAsync(string requestUri) { return _builtInHttpClient.DeleteAsync(requestUri); }

        public Task<HttpResponseMessage> DeleteAsync(string requestUri, CancellationToken cancellationToken)
        {
            return _builtInHttpClient.DeleteAsync(requestUri, cancellationToken);
        }

        public void Dispose() { _builtInHttpClient.Dispose(); }

        public Task<HttpResponseMessage> GetAsync(Uri requestUri) { return _builtInHttpClient.GetAsync(requestUri); }

        public Task<HttpResponseMessage> GetAsync(Uri requestUri, CancellationToken cancellationToken)
        {
            return _builtInHttpClient.GetAsync(requestUri, cancellationToken);
        }

        public Task<HttpResponseMessage> GetAsync(Uri requestUri, HttpCompletionOption completionOption)
        {
            return _builtInHttpClient.GetAsync(requestUri, completionOption);
        }

        public Task<HttpResponseMessage> GetAsync(Uri requestUri, HttpCompletionOption completionOption,
            CancellationToken cancellationToken)
        {
            return _builtInHttpClient.GetAsync(requestUri, completionOption, cancellationToken);
        }

        public Task<HttpResponseMessage> GetAsync(string requestUri) { return _builtInHttpClient.GetAsync(requestUri); }

        public Task<HttpResponseMessage> GetAsync(string requestUri, CancellationToken cancellationToken)
        {
            return _builtInHttpClient.GetAsync(requestUri, cancellationToken);
        }

        public Task<HttpResponseMessage> GetAsync(string requestUri, HttpCompletionOption completionOption)
        {
            return _builtInHttpClient.GetAsync(requestUri, completionOption);
        }

        public Task<HttpResponseMessage> GetAsync(string requestUri, HttpCompletionOption completionOption,
            CancellationToken cancellationToken)
        {
            return _builtInHttpClient.GetAsync(requestUri, completionOption, cancellationToken);
        }

        public Task<byte[]> GetByteArrayAsync(string requestUri) { return _builtInHttpClient.GetByteArrayAsync(requestUri); }

        public Task<byte[]> GetByteArrayAsync(Uri requestUri) { return _builtInHttpClient.GetByteArrayAsync(requestUri); }

        public Task<Stream> GetStreamAsync(Uri requestUri) { return _builtInHttpClient.GetStreamAsync(requestUri); }

        public Task<Stream> GetStreamAsync(string requestUri) { return _builtInHttpClient.GetStreamAsync(requestUri); }

        public Task<string> GetStringAsync(string requestUri) { return _builtInHttpClient.GetStringAsync(requestUri); }

        public Task<string> GetStringAsync(Uri requestUri) { return _builtInHttpClient.GetStringAsync(requestUri); }

        public Task<HttpResponseMessage> PatchAsync(Uri requestUri, HttpContent content)
        {
            return _builtInHttpClient.PatchAsync(requestUri, content);
        }

        public Task<HttpResponseMessage> PatchAsync(Uri requestUri, HttpContent content, CancellationToken cancellationToken)
        {
            return _builtInHttpClient.PatchAsync(requestUri, content, cancellationToken);
        }

        public Task<HttpResponseMessage> PatchAsync(string requestUri, HttpContent content)
        {
            return _builtInHttpClient.PatchAsync(requestUri, content);
        }

        public Task<HttpResponseMessage> PatchAsync(string requestUri, HttpContent content, CancellationToken cancellationToken)
        {
            return _builtInHttpClient.PatchAsync(requestUri, content, cancellationToken);
        }

        public Task<HttpResponseMessage> PostAsync(Uri requestUri, HttpContent content)
        {
            return _builtInHttpClient.PostAsync(requestUri, content);
        }

        public Task<HttpResponseMessage> PostAsync(Uri requestUri, HttpContent content, CancellationToken cancellationToken)
        {
            return _builtInHttpClient.PostAsync(requestUri, content, cancellationToken);
        }

        public Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content)
        {
            return _builtInHttpClient.PostAsync(requestUri, content);
        }

        public Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content, CancellationToken cancellationToken)
        {
            return _builtInHttpClient.PostAsync(requestUri, content, cancellationToken);
        }

        public Task<HttpResponseMessage> PutAsync(Uri requestUri, HttpContent content)
        {
            return _builtInHttpClient.PutAsync(requestUri, content);
        }

        public Task<HttpResponseMessage> PutAsync(Uri requestUri, HttpContent content, CancellationToken cancellationToken)
        {
            return _builtInHttpClient.PutAsync(requestUri, content, cancellationToken);
        }

        public Task<HttpResponseMessage> PutAsync(string requestUri, HttpContent content)
        {
            return _builtInHttpClient.PutAsync(requestUri, content);
        }

        public Task<HttpResponseMessage> PutAsync(string requestUri, HttpContent content, CancellationToken cancellationToken)
        {
            return _builtInHttpClient.PutAsync(requestUri, content, cancellationToken);
        }

        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request) { return _builtInHttpClient.SendAsync(request); }

        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, HttpCompletionOption completionOption)
        {
            return _builtInHttpClient.SendAsync(request, completionOption);
        }

        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return _builtInHttpClient.SendAsync(request, cancellationToken);
        }

        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, HttpCompletionOption completionOption,
            CancellationToken cancellationToken)
        {
            return _builtInHttpClient.SendAsync(request, completionOption, cancellationToken);
        }
    }
}