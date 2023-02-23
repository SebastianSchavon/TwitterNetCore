using System.Diagnostics;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using TwitterNetCore.Dtos;

namespace TwitterNetCore;

using static String;

public class AuthenticationClient
{
    private string ResponseType { get; init; } = "code";
    public string ClientId { get; init; } = Empty;
    public string RedirectUri { get; init; } = Empty;
    public string Scopes { get; init; } = Empty;
    public int ListeningPort { get; init; }

    private string _authorizeUrl = Empty;
    private string _authorizationCode = Empty;
    private readonly IHttpRequestClient _httpClient;

    public AuthenticationClient() : this(new HttpRequestClient())
    {
    }

    public AuthenticationClient(IHttpRequestClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Step 1 in OAuth 2.0 Authorization Code Flow with PKCE
    /// <a href="https://developer.twitter.com/en/docs/authentication/oauth-2-0/user-access-token">Twitter Docs</a>
    /// </summary>
    /// <returns>URL to prompt twitter application access to twitter account</returns>
    public string GetAuthorizationUrl()
    {
        _authorizeUrl =
            $"https://twitter.com/i/oauth2/authorize?" +
            $"response_type={ResponseType}&client_id={ClientId}&" +
            $"redirect_uri={RedirectUri}&scope={Scopes}&" +
            $"state=state&code_challenge=challenge&code_challenge_method=plain";

        return _authorizeUrl;
    }

    /// <summary>
    /// Step 2 in OAuth 2.0 Authorization Code Flow with PKCE 
    /// <a href="https://developer.twitter.com/en/docs/authentication/oauth-2-0/user-access-token">Twitter Docs</a>
    /// OBS this method will open a browser window
    /// </summary>
    /// <param name="secondsUntilTimeout">Time the <see cref="HttpListener"/> will listen for a request from the OAuth2 procedure</param>
    /// <exception cref="TimeoutException">Time transpired and <see cref="HttpListener"/> didn't receive request from OAuth2 procedure</exception>
    /// <exception cref="TwitterAuthorizationException">Problem retrieving the authorization code</exception>
    /// <exception cref="SystemInteractionException">Problem occured during interaction with the operative system</exception>
    /// <returns>The authorization code to be used in the http request of step 3 of the authorization code flow</returns>
    public async Task<string> GetAuthorizationCode(int secondsUntilTimeout)
    {
        try
        {
            var setAuthCode = UserBrowserInteraction();

            var completedTask = await Task.WhenAny(setAuthCode, WaitNSeconds(secondsUntilTimeout));

            EnsureCorrectTaskCompleted(setAuthCode, completedTask);

            return _authorizationCode;
        }
        catch (TwitterAuthorizationException)
        {
            throw;
        }
        catch (TimeoutException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new SystemInteractionException
            {
                Error = "Unexpected error occured when application tried to interact with the operative system",
                Message = ex.Message
            };
        }
    }
    
    private async Task UserBrowserInteraction()
    {
        OpenUrlInDefaultBrowser();

        _authorizationCode = await GetAuthorizationCodeFromLocalPort();
    }

    private void OpenUrlInDefaultBrowser()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            Process.Start(new ProcessStartInfo(_authorizeUrl) { UseShellExecute = true });
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            Process.Start(new ProcessStartInfo(_authorizeUrl) { UseShellExecute = true });
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            Process.Start(new ProcessStartInfo(_authorizeUrl) { UseShellExecute = true });
    }

    private static void EnsureCorrectTaskCompleted(Task task, Task completedTask)
    {
        if (task != completedTask)
            throw new TimeoutException(
                $"Defined time passed without user accepting prompt twitter application access to account");
    }

    private async Task<string> GetAuthorizationCodeFromLocalPort()
    {
        var listener = new HttpListener();

        listener.Prefixes.Add($"http://localhost:{ListeningPort}/");

        listener.Start();

        var context = await listener.GetContextAsync();

        Console.Clear();

        listener.Stop();

        var authorizationCode = context.Request.RawUrl?.Split("=")[^1];

        if (IsNullOrEmpty(authorizationCode))
        {
            throw new TwitterAuthorizationException
            {
                Error = "Authorization code is null or empty",
                Message = "Problem retrieving the authorization code from defined redirect uri request"
            };
        }

        return authorizationCode;
    }

    private static async Task WaitNSeconds(int seconds)
    {
        await Task.Delay(TimeSpan.FromSeconds(seconds));
    }

    /// <summary>
    /// Step 3 in OAuth 2.0 Authorization Code Flow with PKCE 
    /// <a href="https://developer.twitter.com/en/docs/authentication/oauth-2-0/user-access-token">Twitter Docs</a>
    /// </summary>
    /// <param name="authorizationCode">Required if authorization code is not set</param>
    /// <exception cref="TwitterAuthorizationException">The request to Twitters OAuth2 token endpoint did not return success status code.
    /// Or the access token was null or empty</exception>
    /// <exception cref="TwitterRequestException">The request did not return success status code. Or the request failed due to an underlying
    /// issue such as network connectivity, DNS failure, server certificate validation, timeout or missing critical argument</exception>
    public async Task<TokenResponse> GetAccessTokenViaAuthorizationCode(string? authorizationCode = null)
    {
        var body =
            $"code={authorizationCode ?? _authorizationCode}&" +
            $"grant_type=authorization_code&" +
            $"client_id={ClientId}&" +
            $"redirect_uri={RedirectUri}&" +
            $"code_verifier=challenge";

        var response = await SendRequest(_httpClient.RequestAsyncPost("https://api.twitter.com/2/oauth2/token",
            new StringContent(body, Encoding.UTF8, "application/x-www-form-urlencoded"),
            null), ResponseParser.ParseTokenResponse);

        return response;
    }

    /// <summary>
    /// Step 5 in OAuth 2.0 Authorization Code Flow with PKCE 
    /// <a href="https://developer.twitter.com/en/docs/authentication/oauth-2-0/user-access-token">Twitter Docs</a>
    /// </summary>
    /// <param name="refreshToken"></param>
    public async Task<TokenResponse> GetAccessTokenViaRefreshToken(string? refreshToken = null)
    {
        var body =
            $"refresh_token={refreshToken}&" +
            $"grant_type=refresh_token&" +
            $"client_id={ClientId}&";

        var response = await SendRequest(_httpClient.RequestAsyncPost("https://api.twitter.com/2/oauth2/token",
            StringContentParser.ApplicationUrlEncoded(body),
            null), ResponseParser.ParseTokenResponse);

        return response;
    }

    private async Task<T> SendRequest<T>(Task<HttpResponseMessage> request, Func<string, T> parse)
    {
        try
        {
            var response = await request;

            var responseBodyJson = await response.Content.ReadAsStringAsync();

            EnsureStatusCode(response, responseBodyJson);

            return parse(responseBodyJson);
        }
        catch (TwitterAuthorizationException)
        {
            throw;
        }
        catch (TwitterRequestException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new TwitterRequestException
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Error = "Unexpected error during twitter request",
                Message = ex.Message
            };
        }
    }

    private static void EnsureStatusCode(HttpResponseMessage response, string responseBodyJson)
    {
        if (response.IsSuccessStatusCode) return;

        if (response.RequestMessage.RequestUri.OriginalString.Contains("oauth2"))
        {
            throw new TwitterAuthorizationException
            {
                StatusCode = response.StatusCode,
                Error = "Request did not return success status code",
                Message = responseBodyJson,
                Endpoint = response.RequestMessage.RequestUri.OriginalString,
                HelpLink = "https://developer.twitter.com/en/docs/authentication/oauth-2-0/user-access-token"
            };
        }

        throw new TwitterRequestException
        {
            StatusCode = response.StatusCode,
            Error = "Request did not return success status code",
            Message = responseBodyJson,
            Endpoint = response.RequestMessage.RequestUri.OriginalString
        };
    }
}