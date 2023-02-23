# TwitterNetCore

|Description      |Link        |
|-----------------|------------|
|Build            |[![Deploy to NuGet](https://github.com/SebastianSchavon/TwitterNetCore/actions/workflows/main.yml/badge.svg)](https://github.com/SebastianSchavon/TwitterNetCore/actions/workflows/main.yml)|
|Nuget            |![Nuget](https://img.shields.io/nuget/v/TwitterNetCore)|


## About
Easy to use Twitter API .NET SDK. There are currently a few endpoints still not mapped but will be eventually when I get more time. Feel free to contribute.

## Usage
Twitter API documentation
https://developer.twitter.com/en/docs/twitter-api

Authentication documentation
https://developer.twitter.com/en/docs/authentication/oauth-2-0/authorization-code

### Authentication flow that automatically open a web browser to authenticate the user
```
var authenticationClient = new AuthenticationClient()
{
    ClientId = "<CLIENT_ID>",
    Scopes = "<SCOPES>",
    RedirectUri = "<REDIRECT_URI>",
    // Listening port need to match the redirect uri. 
    // If the redirect uri is http://localhost:5000/ then listening port is 5000
    ListeningPort = <LISTENING_PORT>
};

authenticationClient.GetAuthorizationUrl();
await authenticationClient.GetAuthorizationCode(15);
var tokenObject = await authenticationClient.GetAccessTokenViaAuthorizationCode();

var twitterClient = new TwitterClient
{
    AccessToken = tokenObject.access_token
};
```

### Manual authentication flow
```
var authenticationClient = new AuthenticationClient()
{
    ClientId = "<CLIENT_ID>",
    RedirectUri = "<REDIRECT_URI>",
    Scopes = "<SCOPES>"
};

var url = authenticationClient.GetAuthorizationUrl();

// Paste the url in a browser and authenticate to generate authorization code
// The authorization code will be returned in the redirect uri

var tokenObject = await authenticationClient.GetAccessTokenViaAuthorizationCode(<AUTHORIZATION_CODE>);

var twitterClient = new TwitterClient
{
    AccessToken = tokenObject.access_token
};
```
### Post a tweet
```
var body = new
{
    text = "Hello world!"
};

var response = await twitterClient.CreateTweet(body);
```
### Get a single tweet
```
var response = await twitterClient.GetTweet("<TWEET_ID>");

Console.WriteLine(response.data.text);
```

### Pass a QueryFields object to any Get method to define query parameters
```
var fields = new QueryFields
{
    TweetFields = "attachments,author_id,context_annotations,conversation_id,created_at,entities,geo",
    Expansions = "attachments.poll_ids,attachments.media_keys,author_id,geo.place_id,in_reply_to_user_id",
    MediaFields = "duration_ms,height,media_key,preview_image_url,public_metrics,type,url,width",
    PlaceFields = "contained_within,country,country_code,full_name,geo,id,name,place_type",
    PollFields = "duration_minutes,end_datetime,id,options,voting_status",
    UserFields = "created_at,description,id,location,name,pinned_tweet_id,profile_image_url,url,username,verified"
};
```

### Current methods 
* GetTweet
* GetTweets
* CreateTweet
* DeleteTweet
* GetUserById
* GetUserByUsername
* GetUsersById
* GetUsersByUsername
* GetUserFollowers
* GetUserFollowings
* FollowUser
* UnfollowUser
* BlockUser 
* UnblockUser
* GetBlockedUsers
* GetList
* GetUserLists
* CreateList
* UpdateList
* DeleteList
* GetListTweets
* AddListMember
* RemoveListMember
* GetListMembers
* GetUserListMemberships
* FollowList
* UnfollowList
* GetListFollowers
* GetUserFollowedLists
* PinList
* UnpinList
* GetUserPinnedLists
* MuteUser
* UnmuteUser
* GetUserMutes
* GetTweetQuoteTweets
* HideReply
* UnhideReply
* LikeTweet
* UnlikeTweet
* GetUserLikedTweets
* GetUsersLikingTweet
* RetweetTweet
* UndoRetweet
* GetRetweetedByUsers
* GetDirectMessagesByConversation
* GetDirectMessagesByConversationIncludingExactlyTwoParticipants
* GetDirectMessagesByUser

## Links
Twitter API Postman
https://www.postman.com/twitter/workspace/twitter-s-public-workspace/collection/9956214-784efcda-ed4c-4491-a4c0-a26470a67400?ctx=documentation

