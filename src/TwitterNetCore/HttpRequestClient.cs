using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace TwitterNetCore;

public class HttpRequestClient : IHttpRequestClient
{
    public Task<HttpResponseMessage> RequestAsyncGet(string endpoint, string bearerToken = null)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, endpoint);
        
        return RequestAsync(request, bearerToken);
    }

    public Task<HttpResponseMessage> RequestAsyncPut(string endpoint, StringContent content, string bearerToken = null)
    {
        var request = new HttpRequestMessage(HttpMethod.Put, endpoint);
        
        request.Content = content;
        
        return RequestAsync(request, bearerToken);
    }

    public Task<HttpResponseMessage> RequestAsyncPost(string endpoint, StringContent content, string bearerToken = null)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, endpoint);

        request.Content = content;
        
        return RequestAsync(request, bearerToken);
    }
    
    public Task<HttpResponseMessage> RequestAsyncDelete(string endpoint, string bearerToken = null)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, endpoint);

        return RequestAsync(request, bearerToken);
    }
    
    private async Task<HttpResponseMessage> RequestAsync(HttpRequestMessage request, string bearerToken = null)
    {
        var client = new HttpClient();

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

        return await client.SendAsync(request);
    }
}