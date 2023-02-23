using System.Net;

namespace TwitterNetCore;

public class TwitterAuthorizationException : Exception
{
    public HttpStatusCode StatusCode { get; set; }
    public string Error { get; set; }
    public string Message { get; set; }
    public string Endpoint { get; set; }
}