using Moq;
using NUnit.Framework;
using TwitterNetCore;

namespace Tests;

public class AuthClientTestBase
{
    private AuthenticationClient _authenticationClient;

    [SetUp]
    public void Setup()
    {
        _authenticationClient = SetupClient();
    }
    
    private static AuthenticationClient SetupClient()
    {
        var mock = new MockedHttpClient();

        mock.AddResponseFile("/2/oauth2/token",
            Path.Combine(TestContext.CurrentContext.TestDirectory, "Data/GetAccessAndRefreshTokenResponse.json"));

        return new AuthenticationClient(mock.Object);
    }

    [Test]
    public async Task TestGetAccessTokenViaAuthorizationCode()
    {
        var response = await _authenticationClient.GetAccessTokenViaAuthorizationCode(
            "cWhsRkxwVV9jX1RmUjhBa2hfeXhvSkRfODZ2cktycUtPM2tpNVl6MFdmMW94OjE2NzI3NjY4OTAxNTQ6MTowOmFjOjE");

        Assert.Multiple(() =>
        {
            Assert.That(string.IsNullOrEmpty(response.access_token), Is.False);
            Assert.That(string.IsNullOrEmpty(response.refresh_token), Is.False);
        });
    }
    
    [Test]
    public async Task TestGetAccessTokenViaRefreshToken()
    {
        var response = await _authenticationClient.GetAccessTokenViaRefreshToken(
            "cWhsRkxwVV9jX1RmUjhBa2hfeXhvSkRfODZ2cktycUtPM2tpNVl6MFdmMW94OjE2NzI3NjY4OTAxNTQ6MTowOmFjOjE");

        Assert.Multiple(() =>
        {
            Assert.That(string.IsNullOrEmpty(response.access_token), Is.False);
            Assert.That(string.IsNullOrEmpty(response.refresh_token), Is.False);
        });
    }

}