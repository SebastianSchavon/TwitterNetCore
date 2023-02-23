using System.Net;

namespace TwitterNetCore;

public class TwitterRequestException : Exception
{
    public HttpStatusCode StatusCode { get; set; }
    public string Error { get; set; }
    public string Message { get; set; }
    public string Endpoint { get; set; }
}