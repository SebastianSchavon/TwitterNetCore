using System.Net;
using Moq;
using TwitterNetCore;

namespace Tests;

public class MockedHttpClient
{
    private readonly Mock<IHttpRequestClient> _mockClient;
    private readonly Dictionary<string, string> _responseFiles;
    public IHttpRequestClient Object => _mockClient.Object;

    public MockedHttpClient()
    {
        _mockClient = new Mock<IHttpRequestClient>();
        _responseFiles = new Dictionary<string, string>();

        _mockClient.Setup(m => m.RequestAsyncGet(It.IsAny<string>(), 
                It.IsAny<string>()))
            .Returns((string endpoint, string token) => FakeResponse(endpoint, token));

        _mockClient.Setup(m => m.RequestAsyncPut(It.IsAny<string>(), 
                It.IsAny<StringContent>(), It.IsAny<string>()))
            .Returns((string endpoint, object body, string token) => FakeResponse(endpoint, token));

        _mockClient.Setup(m => m.RequestAsyncPost(It.IsAny<string>(), 
                It.IsAny<StringContent>(), It.IsAny<string>()))
            .Returns((string endpoint, object body, string token) => FakeResponse(endpoint, token));
        
        _mockClient.Setup(m => m.RequestAsyncDelete(It.IsAny<string>(), 
                It.IsAny<string>()))
            .Returns((string endpoint, string token) => FakeResponse(endpoint, token));
    }
    
    public void AddResponseFile(string partialEndPoint, string fullResponseFilePath)
    {
        _responseFiles.Add(partialEndPoint, fullResponseFilePath);
    }

    private Task<HttpResponseMessage> FakeResponse(string endpoint, string token)
    {
        var fileName = GetResponseFileName(endpoint);

        return Task.FromResult(new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(File.ReadAllText(fileName))
        });
    }

    private string GetResponseFileName(string endpoint)
    {
        var file = _responseFiles.FirstOrDefault(file => endpoint.Contains(file.Key)).Value;

        if (file == null)
            throw new FileNotFoundException($"No response file for request={endpoint} registered");

        return file;
    }
}