namespace TwitterNetCore;

public interface IHttpRequestClient
{
    Task<HttpResponseMessage> RequestAsyncGet(string endpoint, string bearerToken = null);
    Task<HttpResponseMessage> RequestAsyncPut(string endpoint, StringContent content, string bearerToken = null);
    Task<HttpResponseMessage> RequestAsyncPost(string endpoint, StringContent content, string bearerToken = null);
    Task<HttpResponseMessage> RequestAsyncDelete(string endpoint, string bearerToken = null);
}