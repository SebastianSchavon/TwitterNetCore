using TwitterNetCore;
using TwitterNetCore.Dtos;

var authenticationClient = new AuthenticationClient()
{
    ClientId = "<CLIENT_ID>",
    RedirectUri = "http://localhost:3017/",
    Scopes = "tweet.read tweet.write users.read",
    ListeningPort = 3017
};

authenticationClient.GetAuthorizationUrl();
await authenticationClient.GetAuthorizationCode(15);
var tokenObject = await authenticationClient.GetAccessTokenViaAuthorizationCode();

var twitterClient = new TwitterClient
{
    AccessToken = tokenObject.access_token
};

var body = new
{
    text = "Hello world!"
};

var createTweetResponse = await twitterClient.CreateTweet(body);

var getTweetResponse = await twitterClient.GetTweet(createTweetResponse.data.id, new QueryFields
{
    TweetFields =
        "created_at,attachments",
    Expansions =
        "author_id"
});

Console.WriteLine(getTweetResponse.data.text);

Console.ReadKey();