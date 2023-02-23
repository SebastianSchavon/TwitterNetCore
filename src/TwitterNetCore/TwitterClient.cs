using System.Net;
using TwitterNetCore.Dtos;
using static System.String;

namespace TwitterNetCore;

public class TwitterClient
{
    public string AccessToken { get; init; } = Empty;

    private const string DefaultTwitterApiUrlPrefix = "https://api.twitter.com/2";
    private readonly IHttpRequestClient _httpClient;

    public TwitterClient() : this(new HttpRequestClient())
    {
    }

    public TwitterClient(IHttpRequestClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Returns a variety of information about a single Tweet specified by the requested ID.
    /// <a href="https://developer.twitter.com/en/docs/twitter-api/tweets/lookup/api-reference/get-tweets-id">Twitter Docs</a>
    /// </summary>
    /// <param name="id">Unique identifier of the Tweet to request.</param>
    /// <param name="fields">Data object to construct optional query fields - See documentation for available fields.</param>
    /// <exception cref="TwitterAuthorizationException">The request to Twitters OAuth2 token endpoint did not return success status code.
    /// Or the access token was null or empty</exception>
    /// <exception cref="TwitterRequestException">The request did not return success status code. Or the request failed due to an underlying
    /// issue such as network connectivity, DNS failure, server certificate validation, timeout or missing critical argument.</exception>
    /// <returns></returns>
    public async Task<GetTweetResponse> GetTweet(string id, QueryFields? fields = null)
    {
        var response = await SendRequest(_httpClient.RequestAsyncGet(
            DefaultTwitterApiUrlPrefix + $"/tweets/{id}?{fields ?? new QueryFields()}",
            AccessToken), ResponseParser.ParseGetTweetResponse);

        return response;
    }

    /// <summary>
    /// Returns a variety of information about the Tweet specified by the requested ID or list of IDs.
    /// <a href="https://developer.twitter.com/en/docs/twitter-api/tweets/lookup/api-reference/get-tweets">Twitter Docs</a>
    /// </summary>
    /// <param name="ids">A comma separated list of Tweet IDs. Up to 100 are allowed in a single request. Make sure to not include
    /// a space between commas and fields.</param>
    /// <param name="fields">Data object to construct optional query fields - See documentation for available fields.</param>
    /// <exception cref="TwitterAuthorizationException">The request to Twitters OAuth2 token endpoint did not return success status code.
    /// Or the access token was null or empty.</exception>
    /// <exception cref="TwitterRequestException">The request did not return success status code. Or the request failed due to an underlying
    /// issue such as network connectivity, DNS failure, server certificate validation, timeout or missing critical argument.</exception>
    /// <returns></returns>
    public async Task<GetTweetsResponse> GetTweets(string ids, QueryFields? fields = null)
    {
        var response = await SendRequest(_httpClient.RequestAsyncGet(
            DefaultTwitterApiUrlPrefix + $"/tweets?ids={ids}&{fields ?? new QueryFields()}",
            AccessToken), ResponseParser.ParseGetTweetsResponse);

        return response;
    }

    /// <summary>
    /// Creates a Tweet on behalf of an authenticated user.
    /// <a href="https://developer.twitter.com/en/docs/twitter-api/tweets/manage-tweets/api-reference/post-tweets">Twitter Docs</a>
    /// </summary>
    /// <param name="body">JSON body - See documentation for available parameters.</param>
    /// <exception cref="TwitterAuthorizationException">The request to Twitters OAuth2 token endpoint did not return success status code.
    /// Or the access token was null or empty.</exception>
    /// <exception cref="TwitterRequestException">The request did not return success status code. Or the request failed due to an underlying
    /// issue such as network connectivity, DNS failure, server certificate validation, timeout or missing critical argument.</exception>
    /// <returns></returns>
    public async Task<CreateTweetResponse> CreateTweet(object body)
    {
        var response = await SendRequest(_httpClient.RequestAsyncPost(DefaultTwitterApiUrlPrefix + "/tweets",
                StringContentParser.ApplicationJsonStringContent(body), AccessToken),
            ResponseParser.ParseCreateTweetResponse);

        return response;
    }

    /// <summary>
    /// Allows a user or authenticated user ID to delete a Tweet.
    /// <a href="https://developer.twitter.com/en/docs/twitter-api/tweets/manage-tweets/api-reference/delete-tweets-id">Twitter Docs</a>
    /// </summary>
    /// <param name="id">The Tweet ID you are deleting.</param>
    /// <exception cref="TwitterAuthorizationException">The request to Twitters OAuth2 token endpoint did not return success status code.
    /// Or the access token was null or empty.</exception>
    /// <exception cref="TwitterRequestException">The request did not return success status code. Or the request failed due to an underlying
    /// issue such as network connectivity, DNS failure, server certificate validation, timeout or missing critical argument.</exception>
    /// <returns></returns>
    public async Task<DeleteTweetResponse> DeleteTweet(string id)
    {
        var response = await SendRequest(_httpClient.RequestAsyncDelete(DefaultTwitterApiUrlPrefix + $"/tweets/{id}",
            AccessToken), ResponseParser.ParseDeleteTweetResponse);

        return response;
    }

    /// <summary>
    /// Returns a variety of information about a single user specified by the requested ID.
    /// <a href="https://developer.twitter.com/en/docs/twitter-api/users/lookup/api-reference/get-users-id">Twitter Docs</a>
    /// </summary>
    /// <param name="id">The ID of the user to lookup.</param>
    /// <param name="fields">A data object to construct optional query fields - See documentation for available fields.</param>
    /// <exception cref="TwitterAuthorizationException">The request to Twitters OAuth2 token endpoint did not return success status code.
    /// Or the access token was null or empty.</exception>
    /// <exception cref="TwitterRequestException">The request did not return success status code. Or the request failed due to an underlying
    /// issue such as network connectivity, DNS failure, server certificate validation, timeout or missing critical argument.</exception>
    /// <returns></returns>
    public async Task<GetUserResponse> GetUserById(string id, QueryFields? fields = null)
    {
        var response = await SendRequest(_httpClient.RequestAsyncGet(
            DefaultTwitterApiUrlPrefix + $"/users/{id}?{fields ?? new QueryFields()}",
            AccessToken), ResponseParser.ParseGetUserResponse);

        return response;
    }

    /// <summary>
    /// Returns a variety of information about one or more users specified by their usernames.
    /// <a href="https://developer.twitter.com/en/docs/twitter-api/users/lookup/api-reference/get-users-by-username-username">Twitter Docs</a>
    /// </summary>
    /// <param name="username">The Twitter username (handle) of the user.</param>
    /// <param name="fields">A data object to construct optional query fields - See documentation for available fields.</param>
    /// <exception cref="TwitterAuthorizationException">The request to Twitters OAuth2 token endpoint did not return success status code.
    /// Or the access token was null or empty.</exception>
    /// <exception cref="TwitterRequestException">The request did not return success status code. Or the request failed due to an underlying
    /// issue such as network connectivity, DNS failure, server certificate validation, timeout or missing critical argument.</exception>
    /// <returns></returns>
    public async Task<GetUserResponse> GetUserByUsername(string username, QueryFields? fields = null)
    {
        var response = await SendRequest(_httpClient.RequestAsyncGet(
            DefaultTwitterApiUrlPrefix +
            $"/users/by/username/{username}?{fields ?? new QueryFields()}",
            AccessToken), ResponseParser.ParseGetUserResponse);

        return response;
    }

    /// <summary>
    /// Returns a variety of information about one or more users specified by the requested IDs.
    /// <a href="https://developer.twitter.com/en/docs/twitter-api/users/lookup/api-reference/get-users">Twitter Docs</a>
    /// </summary>
    /// <param name="ids">A comma separated list of user IDs. Up to 100 are allowed in a single request. Make sure to not
    /// include a space between commas and fields.</param>
    /// <param name="fields">Data object to construct optional query fields - See documentation for available fields.</param>
    /// <exception cref="TwitterAuthorizationException">The request to Twitters OAuth2 token endpoint did not return success status code.
    /// Or the access token was null or empty.</exception>
    /// <exception cref="TwitterRequestException">The request did not return success status code. Or the request failed due to an underlying
    /// issue such as network connectivity, DNS failure, server certificate validation, timeout or missing critical argument.</exception>
    /// <returns></returns>
    public async Task<GetUsersResponse> GetUsersById(string ids, QueryFields? fields = null)
    {
        var response = await SendRequest(_httpClient.RequestAsyncGet(
            DefaultTwitterApiUrlPrefix +
            $"/users?ids={ids}&{fields ?? new QueryFields()}",
            AccessToken), ResponseParser.ParseGetUsersResponse);

        return response;
    }

    /// <summary>
    /// Returns a variety of information about one or more users specified by their usernames.
    /// <a href="https://developer.twitter.com/en/docs/twitter-api/users/lookup/api-reference/get-users-by">Twitter Docs</a>
    /// </summary>
    /// <param name="usernames">A comma separated list of Twitter usernames (handles). Up to 100 are allowed in a single request.
    /// Make sure to not include a space between commas and fields.</param>
    /// <param name="fields">Data object to construct optional query fields - See documentation for available fields.</param>
    /// <exception cref="TwitterAuthorizationException">The request to Twitters OAuth2 token endpoint did not return success status code.
    /// Or the access token was null or empty.</exception>
    /// <exception cref="TwitterRequestException">The request did not return success status code. Or the request failed due to an underlying
    /// issue such as network connectivity, DNS failure, server certificate validation, timeout or missing critical argument.</exception>
    /// <returns></returns>
    public async Task<GetUsersResponse> GetUsersByUsername(string usernames, QueryFields? fields = null)
    {
        var response = await SendRequest(_httpClient.RequestAsyncGet(
            DefaultTwitterApiUrlPrefix +
            $"/users/by?usernames={usernames}&{fields ?? new QueryFields()}",
            AccessToken), ResponseParser.ParseGetUsersResponse);

        return response;
    }

    /// <summary>
    /// Returns a list of users who are followers of the specified user ID.
    /// <a href="https://developer.twitter.com/en/docs/twitter-api/users/follows/api-reference/get-users-id-followers">Twitter Docs</a>
    /// </summary>
    /// <param name="id">The user ID whose followers you would like to retrieve.</param>
    /// <param name="fields">Data object to construct optional query fields - See documentation for available fields.</param>
    /// <exception cref="TwitterAuthorizationException">The request to Twitters OAuth2 token endpoint did not return success status code.
    /// Or the access token was null or empty.</exception>
    /// <exception cref="TwitterRequestException">The request did not return success status code. Or the request failed due to an underlying
    /// issue such as network connectivity, DNS failure, server certificate validation, timeout or missing critical argument.</exception>
    /// <returns></returns>
    public async Task<GetUserFollowersResponse> GetUserFollowers(string id, QueryFields? fields = null)
    {
        var response = await SendRequest(_httpClient.RequestAsyncGet(
            DefaultTwitterApiUrlPrefix +
            $"/users/{id}/followers?{fields ?? new QueryFields()}",
            AccessToken), ResponseParser.ParseGetUserFollowersResponse);

        return response;
    }

    /// <summary>
    /// Returns a list of users the specified user ID is following.
    /// <a href="https://developer.twitter.com/en/docs/twitter-api/users/follows/api-reference/get-users-id-following">Twitter Docs</a>
    /// </summary>
    /// <param name="id">The user ID whose following you would like to retrieve.</param>
    /// <param name="fields">Data object to construct optional query fields - See documentation for available fields.</param>
    /// <exception cref="TwitterAuthorizationException">The request to Twitters OAuth2 token endpoint did not return success status code.
    /// Or the access token was null or empty.</exception>
    /// <exception cref="TwitterRequestException">The request did not return success status code. Or the request failed due to an underlying
    /// issue such as network connectivity, DNS failure, server certificate validation, timeout or missing critical argument.</exception>
    /// <returns></returns>
    public async Task<GetUserFollowingsResponse> GetUserFollowings(string id, QueryFields? fields = null)
    {
        var response = await SendRequest(_httpClient.RequestAsyncGet(
            DefaultTwitterApiUrlPrefix +
            $"/users/{id}/following?{fields ?? new QueryFields()}",
            AccessToken), ResponseParser.ParseGetUserFollowingResponse);

        return response;
    }

    /// <summary>
    /// Allows a user ID to follow another user.
    /// If the target user does not have public Tweets, this endpoint will send a follow request.
    /// The request succeeds with no action when the authenticated user sends a request to a user they're already following, or if they're
    /// sending a follower request to a user that does not have public Tweets.
    /// <a href="https://developer.twitter.com/en/docs/twitter-api/users/follows/api-reference/post-users-source_user_id-following">Twitter Docs</a>
    /// </summary>
    /// <param name="id">The user ID who you would like to initiate the follow on behalf of. The user’s ID must correspond to the user ID of the authenticated user.</param>
    /// <param name="target_user_id">The user ID of the user that you would like the <see cref="id"/> to follow.</param>
    /// <exception cref="TwitterAuthorizationException">The request to Twitters OAuth2 token endpoint did not return success status code.
    /// Or the access token was null or empty.</exception>
    /// <exception cref="TwitterRequestException">The request did not return success status code. Or the request failed due to an underlying
    /// issue such as network connectivity, DNS failure, server certificate validation, timeout or missing critical argument.</exception>
    /// <returns></returns>
    public async Task<FollowUserResponse> FollowUser(string id, string target_user_id)
    {
        var body = new
        {
            target_user_id = target_user_id
        };

        var response = await SendRequest(_httpClient.RequestAsyncPost(
            DefaultTwitterApiUrlPrefix +
            $"/users/{id}/following", StringContentParser.ApplicationJsonStringContent(body),
            AccessToken), ResponseParser.ParseFollowUserResponse);

        return response;
    }

    /// <summary>
    /// Allows a user ID to unfollow another user.
    /// The request succeeds with no action when the authenticated user sends a request to a user they're not following or have already unfollowed.
    /// <a href="https://developer.twitter.com/en/docs/twitter-api/users/follows/api-reference/delete-users-source_id-following">Twitter Docs</a>
    /// </summary>
    /// <param name="source_user_id">The user ID who you would like to initiate the unfollow on behalf of. The user’s ID must correspond to the user ID of the authenticated user.</param>
    /// <param name="target_user_id">The user ID of the user that you would like the <see cref="source_user_id"/> to unfollow.</param>
    /// <exception cref="TwitterAuthorizationException">The request to Twitters OAuth2 token endpoint did not return success status code.
    /// Or the access token was null or empty.</exception>
    /// <exception cref="TwitterRequestException">The request did not return success status code. Or the request failed due to an underlying
    /// issue such as network connectivity, DNS failure, server certificate validation, timeout or missing critical argument.</exception>
    /// <returns></returns>
    public async Task<UnfollowUserResponse> UnfollowUser(string source_user_id, string target_user_id)
    {
        var response = await SendRequest(_httpClient.RequestAsyncDelete(
            DefaultTwitterApiUrlPrefix +
            $"/users/{source_user_id}/following/{target_user_id}",
            AccessToken), ResponseParser.ParseUnfollowUserResponse);

        return response;
    }

    /// <summary>
    /// Causes the user to block the target user. The user must match the user Access Tokens being used to authorize the request.
    /// <a href="https://developer.twitter.com/en/docs/twitter-api/users/blocks/api-reference/post-users-user_id-blocking">Twitter Docs</a>
    /// </summary>
    /// <param name="id">The user ID who you would like to initiate the block on behalf of. The user’s ID must correspond to the user ID of the authenticated user.</param>
    /// <param name="target_user_id">The user ID of the user that you would like the <see cref="id"/> to block.</param>
    /// <exception cref="TwitterAuthorizationException">The request to Twitters OAuth2 token endpoint did not return success status code.
    /// Or the access token was null or empty.</exception>
    /// <exception cref="TwitterRequestException">The request did not return success status code. Or the request failed due to an underlying
    /// issue such as network connectivity, DNS failure, server certificate validation, timeout or missing critical argument.</exception>
    /// <returns></returns>
    public async Task<BlockUserResponse> BlockUser(string id, string target_user_id)
    {
        var body = new
        {
            target_user_id = target_user_id
        };

        var response = await SendRequest(_httpClient.RequestAsyncPost(
            DefaultTwitterApiUrlPrefix +
            $"/users/{id}/blocking", StringContentParser.ApplicationJsonStringContent(body),
            AccessToken), ResponseParser.ParseBlockUserResponse);

        return response;
    }

    /// <summary>
    /// Allows a user or authenticated user ID to unblock another user.
    /// The request succeeds with no action when the user sends a request to a user they're not blocking or have already unblocked.
    /// <a href="https://developer.twitter.com/en/docs/twitter-api/users/blocks/api-reference/delete-users-user_id-blocking">Twitter Docs</a>
    /// </summary>
    /// <param name="source_user_id">The user ID who you would like to initiate an unblock on behalf of. The user’s ID must correspond to the user ID of the authenticated user.</param>
    /// <param name="target_user_id">The user ID of the user that you would like the <see cref="source_user_id"/> to unblock.</param>
    /// <exception cref="TwitterAuthorizationException">The request to Twitters OAuth2 token endpoint did not return success status code.
    /// Or the access token was null or empty.</exception>
    /// <exception cref="TwitterRequestException">The request did not return success status code. Or the request failed due to an underlying
    /// issue such as network connectivity, DNS failure, server certificate validation, timeout or missing critical argument.</exception>
    /// <returns></returns>
    public async Task<UnblockUserResponse> UnblockUser(string source_user_id, string target_user_id)
    {
        var response = await SendRequest(_httpClient.RequestAsyncDelete(
            DefaultTwitterApiUrlPrefix +
            $"/users/{source_user_id}/blocking/{target_user_id}",
            AccessToken), ResponseParser.ParseUnblockUserResponse);

        return response;
    }

    /// <summary>
    /// Returns a list of users who are blocked by the specified user ID.
    /// <a href="https://developer.twitter.com/en/docs/twitter-api/users/blocks/api-reference/get-users-blocking">Twitter Docs</a>
    /// </summary>
    /// <param name="id">The user ID whose blocked users you would like to retrieve. The user’s ID must correspond to the user ID of the authenticated user.</param>
    /// <param name="fields">Data object to construct optional query fields - See documentation for available fields.</param>
    /// <exception cref="TwitterAuthorizationException">The request to Twitters OAuth2 token endpoint did not return success status code.
    /// Or the access token was null or empty.</exception>
    /// <exception cref="TwitterRequestException">The request did not return success status code. Or the request failed due to an underlying
    /// issue such as network connectivity, DNS failure, server certificate validation, timeout or missing critical argument.</exception>
    /// <returns></returns>
    public async Task<GetBlockedUsersResponse> GetBlockedUsers(string id, QueryFields? fields = null)
    {
        var response = await SendRequest(_httpClient.RequestAsyncGet(
            DefaultTwitterApiUrlPrefix +
            $"/users/{id}/blocking?{fields ?? new QueryFields()}",
            AccessToken), ResponseParser.ParseGetBlockedUsersResponse);

        return response;
    }


    /// <summary>
    /// Returns the details of a specified List.
    /// <a href="https://developer.twitter.com/en/docs/twitter-api/lists/list-lookup/api-reference/get-lists-id">Twitter Docs</a>
    /// </summary>
    /// <param name="id">The ID of the List to lookup.</param>
    /// <param name="fields">Data object to construct optional query fields - See documentation for available fields.</param>
    /// <exception cref="TwitterAuthorizationException">The request to Twitters OAuth2 token endpoint did not return success status code.
    /// Or the access token was null or empty.</exception>
    /// <exception cref="TwitterRequestException">The request did not return success status code. Or the request failed due to an underlying
    /// issue such as network connectivity, DNS failure, server certificate validation, timeout or missing critical argument.</exception>
    /// <returns></returns>
    public async Task<GetListResponse> GetList(string id, QueryFields? fields = null)
    {
        var response = await SendRequest(_httpClient.RequestAsyncGet(
            DefaultTwitterApiUrlPrefix +
            $"/lists/{id}?{fields ?? new QueryFields()}",
            AccessToken), ResponseParser.ParseGetListResponse);

        return response;
    }

    /// <summary>
    /// Returns all Lists owned by the specified user.
    /// <a href="https://developer.twitter.com/en/docs/twitter-api/lists/list-lookup/api-reference/get-users-id-owned_lists">Twitter Docs</a>
    /// </summary>
    /// <param name="id">The user ID whose owned Lists you would like to retrieve.</param>
    /// <param name="fields">Data object to construct optional query fields - See documentation for available fields.</param>
    /// <exception cref="TwitterAuthorizationException">The request to Twitters OAuth2 token endpoint did not return success status code.
    /// Or the access token was null or empty.</exception>
    /// <exception cref="TwitterRequestException">The request did not return success status code. Or the request failed due to an underlying
    /// issue such as network connectivity, DNS failure, server certificate validation, timeout or missing critical argument.</exception>
    /// <returns></returns>
    public async Task<GetUserListsResponse> GetUserLists(string id, QueryFields? fields = null)
    {
        var response = await SendRequest(_httpClient.RequestAsyncGet(
            DefaultTwitterApiUrlPrefix +
            $"/users/{id}/owned_lists?{fields ?? new QueryFields()}",
            AccessToken), ResponseParser.ParseGetUserListsResponse);

        return response;
    }


    /// <summary>
    /// Enables the authenticated user to create a List.
    /// <a href="https://developer.twitter.com/en/docs/twitter-api/lists/manage-lists/api-reference/post-lists">Twitter Docs</a>
    /// </summary>
    /// <param name="body">JSON body - See documentation for available parameters.</param>
    /// <exception cref="TwitterAuthorizationException">The request to Twitters OAuth2 token endpoint did not return success status code.
    /// Or the access token was null or empty.</exception>
    /// <exception cref="TwitterRequestException">The request did not return success status code. Or the request failed due to an underlying
    /// issue such as network connectivity, DNS failure, server certificate validation, timeout or missing critical argument.</exception>
    /// <returns></returns>
    public async Task<CreateListResponse> CreateList(object body)
    {
        var response = await SendRequest(_httpClient.RequestAsyncPost(
            DefaultTwitterApiUrlPrefix +
            $"/lists", StringContentParser.ApplicationJsonStringContent(body),
            AccessToken), ResponseParser.ParseCreateListResponse);

        return response;
    }

    /// <summary>
    /// Enables the authenticated user to update the meta data of a specified List that they own.
    /// <a href="https://developer.twitter.com/en/docs/twitter-api/lists/manage-lists/api-reference/put-lists-id">Twitter Docs</a>
    /// </summary>
    /// <param name="id">The ID of the List to be updated.</param>
    /// <param name="body">JSON body - See documentation for available parameters.</param>
    /// <exception cref="TwitterAuthorizationException">The request to Twitters OAuth2 token endpoint did not return success status code.
    /// Or the access token was null or empty.</exception>
    /// <exception cref="TwitterRequestException">The request did not return success status code. Or the request failed due to an underlying
    /// issue such as network connectivity, DNS failure, server certificate validation, timeout or missing critical argument.</exception>
    /// <returns></returns>
    public async Task<UpdateListResponse> UpdateList(string id, object body)
    {
        var response = await SendRequest(_httpClient.RequestAsyncPut(
            DefaultTwitterApiUrlPrefix +
            $"/lists/{id}", StringContentParser.ApplicationJsonStringContent(body),
            AccessToken), ResponseParser.ParseUpdateListResponse);

        return response;
    }

    /// <summary>
    /// Enables the authenticated user to delete a List that they own.
    /// <a href="https://developer.twitter.com/en/docs/twitter-api/lists/manage-lists/api-reference/delete-lists-id">Twitter Docs</a>
    /// </summary>
    /// <param name="id">The ID of the List to be deleted.</param>
    /// <exception cref="TwitterAuthorizationException">The request to Twitters OAuth2 token endpoint did not return success status code.
    /// Or the access token was null or empty.</exception>
    /// <exception cref="TwitterRequestException">The request did not return success status code. Or the request failed due to an underlying
    /// issue such as network connectivity, DNS failure, server certificate validation, timeout or missing critical argument.</exception>
    /// <returns></returns>
    public async Task<DeleteListResponse> DeleteList(string id)
    {
        var response = await SendRequest(_httpClient.RequestAsyncDelete(
            DefaultTwitterApiUrlPrefix +
            $"/lists/{id}",
            AccessToken), ResponseParser.ParseDeleteListResponse);

        return response;
    }

    /// <summary>
    /// Returns a list of Tweets from the specified List.
    /// <a href="https://developer.twitter.com/en/docs/twitter-api/lists/list-tweets/api-reference/get-lists-id-tweets">Twitter Docs</a>
    /// </summary>
    /// <param name="id">The ID of the List whose Tweets you would like to retrieve.</param>
    /// <param name="fields">Data object to construct optional query fields - See documentation for available fields.</param>
    /// <exception cref="TwitterAuthorizationException">The request to Twitters OAuth2 token endpoint did not return success status code.
    /// Or the access token was null or empty.</exception>
    /// <exception cref="TwitterRequestException">The request did not return success status code. Or the request failed due to an underlying
    /// issue such as network connectivity, DNS failure, server certificate validation, timeout or missing critical argument.</exception>
    /// <returns></returns>
    public async Task<GetListTweetsResponse> GetListTweets(string id, QueryFields? fields = null)
    {
        var response = await SendRequest(_httpClient.RequestAsyncGet(
                DefaultTwitterApiUrlPrefix +
                $"/lists/{id}/tweets?{fields ?? new QueryFields()}", AccessToken),
            ResponseParser.ParseGetListTweetsResponse);

        return response;
    }

    /// <summary>
    /// Enables the authenticated user to add a member to a List they own.
    /// <a href="https://developer.twitter.com/en/docs/twitter-api/lists/list-members/api-reference/post-lists-id-members">Twitter Docs</a>
    /// </summary>
    /// <param name="id">The ID of the List you are adding a member to.</param>
    /// <param name="user_id">The ID of the user you wish to add as a member of the List.</param>
    /// <exception cref="TwitterAuthorizationException">The request to Twitters OAuth2 token endpoint did not return success status code.
    /// Or the access token was null or empty.</exception>
    /// <exception cref="TwitterRequestException">The request did not return success status code. Or the request failed due to an underlying
    /// issue such as network connectivity, DNS failure, server certificate validation, timeout or missing critical argument.</exception>
    /// <returns></returns>
    public async Task<AddListMemberResponse> AddListMember(string id, string user_id)
    {
        var body = new
        {
            user_id = user_id
        };

        var response = await SendRequest(_httpClient.RequestAsyncPost(
                DefaultTwitterApiUrlPrefix +
                $"/lists/{id}/members", StringContentParser.ApplicationJsonStringContent(body), AccessToken),
            ResponseParser.ParseAddListMemberResponse);

        return response;
    }

    /// <summary>
    /// Enables the authenticated user to remove a member from a List they own.
    /// <a href="https://developer.twitter.com/en/docs/twitter-api/lists/list-members/api-reference/delete-lists-id-members-user_id">Twitter Docs</a>
    /// </summary>
    /// <param name="id">The ID of the List you are removing a member from.</param>
    /// <param name="user_id">The ID of the user you wish to remove as a member of the List.</param>
    /// <exception cref="TwitterAuthorizationException">The request to Twitters OAuth2 token endpoint did not return success status code.
    /// Or the access token was null or empty.</exception>
    /// <exception cref="TwitterRequestException">The request did not return success status code. Or the request failed due to an underlying
    /// issue such as network connectivity, DNS failure, server certificate validation, timeout or missing critical argument.</exception>
    /// <returns></returns>
    public async Task<RemoveListMemberResponse> RemoveListMember(string id, string user_id)
    {
        var response = await SendRequest(_httpClient.RequestAsyncDelete(
                DefaultTwitterApiUrlPrefix +
                $"/lists/{id}/members/{user_id}", AccessToken),
            ResponseParser.ParseRemoveListMemberResponse);

        return response;
    }

    /// <summary>
    /// Returns a list of users who are members of the specified List.
    /// <a href="https://developer.twitter.com/en/docs/twitter-api/lists/list-members/api-reference/get-lists-id-members">Twitter Docs</a>
    /// </summary>
    /// <param name="id">The ID of the List whose members you would like to retrieve.</param>
    /// <param name="fields">Data object to construct optional query fields - See documentation for available fields.</param>
    /// <exception cref="TwitterAuthorizationException">The request to Twitters OAuth2 token endpoint did not return success status code.
    /// Or the access token was null or empty.</exception>
    /// <exception cref="TwitterRequestException">The request did not return success status code. Or the request failed due to an underlying
    /// issue such as network connectivity, DNS failure, server certificate validation, timeout or missing critical argument.</exception>
    /// <returns></returns>
    public async Task<GetListMembersResponse> GetListMembers(string id, QueryFields? fields = null)
    {
        var response = await SendRequest(_httpClient.RequestAsyncGet(
                DefaultTwitterApiUrlPrefix +
                $"/lists/{id}/members?{fields ?? new QueryFields()}", AccessToken),
            ResponseParser.ParseGetListMemberResponse);

        return response;
    }

    /// <summary>
    /// Returns all Lists a specified user is a member of.
    /// <a href="https://developer.twitter.com/en/docs/twitter-api/lists/list-members/api-reference/get-users-id-list_memberships">Twitter Docs</a>
    /// </summary>
    /// <param name="id">The user ID whose List memberships you would like to retrieve.</param>
    /// <param name="fields">Data object to construct optional query fields - See documentation for available fields.</param>
    /// <exception cref="TwitterAuthorizationException">The request to Twitters OAuth2 token endpoint did not return success status code.
    /// Or the access token was null or empty.</exception>
    /// <exception cref="TwitterRequestException">The request did not return success status code. Or the request failed due to an underlying
    /// issue such as network connectivity, DNS failure, server certificate validation, timeout or missing critical argument.</exception>
    /// <returns></returns>
    public async Task<GetUserListMembershipsResponse> GetUserListMemberships(string id, QueryFields? fields = null)
    {
        var response = await SendRequest(_httpClient.RequestAsyncGet(
                DefaultTwitterApiUrlPrefix +
                $"/users/{id}/list_memberships?{fields ?? new QueryFields()}", AccessToken),
            ResponseParser.ParseGetUserListMembershipsResponse);

        return response;
    }

    /// <summary>
    /// Enables the authenticated user to follow a List.
    /// <a href="https://developer.twitter.com/en/docs/twitter-api/lists/list-follows/api-reference/post-users-id-followed-lists">Twitter Docs</a>
    /// </summary>
    /// <param name="id">The user ID who you are following a List on behalf of.</param>
    /// <param name="list_id">The ID of the List that you would like the user <see cref="id"/> to follow.</param>
    /// <exception cref="TwitterAuthorizationException">The request to Twitters OAuth2 token endpoint did not return success status code.
    /// Or the access token was null or empty.</exception>
    /// <exception cref="TwitterRequestException">The request did not return success status code. Or the request failed due to an underlying
    /// issue such as network connectivity, DNS failure, server certificate validation, timeout or missing critical argument.</exception>
    /// <returns></returns>
    public async Task<FollowListResponse> FollowList(string id, string list_id)
    {
        var body = new
        {
            list_id = list_id
        };

        var response = await SendRequest(_httpClient.RequestAsyncPost(
                DefaultTwitterApiUrlPrefix +
                $"/users/{id}/followed_lists", StringContentParser.ApplicationJsonStringContent(body),
                AccessToken),
            ResponseParser.ParseFollowListResponse);

        return response;
    }

    /// <summary>
    /// Enables the authenticated user to unfollow a List.
    /// <a href="https://developer.twitter.com/en/docs/twitter-api/lists/list-follows/api-reference/delete-users-id-followed-lists-list_id">Twitter Docs</a>
    /// </summary>
    /// <param name="id">The user ID who you are unfollowing a List on behalf of.</param>
    /// <param name="list_id">The ID of the List that you would like the user <see cref="id"/> to unfollow.</param>
    /// <exception cref="TwitterAuthorizationException">The request to Twitters OAuth2 token endpoint did not return success status code.
    /// Or the access token was null or empty.</exception>
    /// <exception cref="TwitterRequestException">The request did not return success status code. Or the request failed due to an underlying
    /// issue such as network connectivity, DNS failure, server certificate validation, timeout or missing critical argument.</exception>
    /// <returns></returns>
    public async Task<UnfollowListResponse> UnfollowList(string id, string list_id)
    {
        var response = await SendRequest(_httpClient.RequestAsyncDelete(
                DefaultTwitterApiUrlPrefix +
                $"/users/{id}/followed_lists/{list_id}", AccessToken),
            ResponseParser.ParseUnfollowListResponse);

        return response;
    }

    /// <summary>
    /// Returns a list of users who are followers of the specified List.
    /// <a href="https://developer.twitter.com/en/docs/twitter-api/lists/list-follows/api-reference/get-lists-id-followers">Twitter Docs</a>
    /// </summary>
    /// <param name="id">The ID of the List whose followers you would like to retrieve.</param>
    /// <exception cref="TwitterAuthorizationException">The request to Twitters OAuth2 token endpoint did not return success status code.
    /// Or the access token was null or empty.</exception>
    /// <exception cref="TwitterRequestException">The request did not return success status code. Or the request failed due to an underlying
    /// issue such as network connectivity, DNS failure, server certificate validation, timeout or missing critical argument.</exception>
    /// <returns></returns>
    public async Task<GetListFollowersResponse> GetListFollowers(string id, QueryFields? fields = null)
    {
        var response = await SendRequest(_httpClient.RequestAsyncGet(
                DefaultTwitterApiUrlPrefix +
                $"/lists/{id}/followers?{fields ?? new QueryFields()}", AccessToken),
            ResponseParser.ParseGetListFollowersResponse);

        return response;
    }

    /// <summary>
    /// Returns all Lists a specified user follows.
    /// <a href="https://developer.twitter.com/en/docs/twitter-api/lists/list-follows/api-reference/get-users-id-followed_lists">Twitter Docs</a>
    /// </summary>
    /// <param name="id">The user ID whose followed Lists you would like to retrieve.</param>
    /// <exception cref="TwitterAuthorizationException">The request to Twitters OAuth2 token endpoint did not return success status code.
    /// Or the access token was null or empty.</exception>
    /// <exception cref="TwitterRequestException">The request did not return success status code. Or the request failed due to an underlying
    /// issue such as network connectivity, DNS failure, server certificate validation, timeout or missing critical argument.</exception>
    /// <returns></returns>
    public async Task<GetUserFollowedListsResponse> GetUserFollowedLists(string id, QueryFields? fields = null)
    {
        var response = await SendRequest(_httpClient.RequestAsyncGet(
                DefaultTwitterApiUrlPrefix +
                $"/users/{id}/followed_lists?{fields ?? new QueryFields()}", AccessToken),
            ResponseParser.ParseGetUserFollowedListsResponse);

        return response;
    }
    
    /// <summary>
    /// Enables the authenticated user to pin a List.
    /// <a href="https://developer.twitter.com/en/docs/twitter-api/lists/pinned-lists/api-reference/post-users-id-pinned-lists">Twitter Docs</a>
    /// </summary>
    /// <param name="id">The user ID who you are pinning a List on behalf of. The user’s ID must correspond to the user ID of the authenticated user.</param>
    /// <param name="list_id">The ID of the List that you would like the user <see cref="id"/> to pin</param>
    /// <exception cref="TwitterAuthorizationException">The request to Twitters OAuth2 token endpoint did not return success status code.
    /// Or the access token was null or empty.</exception>
    /// <exception cref="TwitterRequestException">The request did not return success status code. Or the request failed due to an underlying
    /// issue such as network connectivity, DNS failure, server certificate validation, timeout or missing critical argument.</exception>
    /// <returns></returns>
    public async Task<PinListResponse> PinList(string id, string list_id)
    {
        var body = new
        {
            list_id = list_id
        };

        var response = await SendRequest(_httpClient.RequestAsyncPost(
                DefaultTwitterApiUrlPrefix +
                $"/users/{id}/pinned_lists", StringContentParser.ApplicationJsonStringContent(body),
                AccessToken),
            ResponseParser.ParsePinListResponse);

        return response;
    }
    
    /// <summary>
    /// Enables the authenticated user to unpin a List.
    /// <a href="https://developer.twitter.com/en/docs/twitter-api/lists/pinned-lists/api-reference/delete-users-id-pinned-lists-list_id">Twitter Docs</a>
    /// </summary>
    /// <param name="id">The user ID whose pinned Lists you would like to retrieve. The user’s ID must correspond to the user ID of the authenticated user.</param>
    /// <param name="list_id">The ID of the List that you would like the user <see cref="list_id"/> to unpin</param>
    /// <exception cref="TwitterAuthorizationException">The request to Twitters OAuth2 token endpoint did not return success status code.
    /// Or the access token was null or empty.</exception>
    /// <exception cref="TwitterRequestException">The request did not return success status code. Or the request failed due to an underlying
    /// issue such as network connectivity, DNS failure, server certificate validation, timeout or missing critical argument.</exception>
    /// <returns></returns>
    public async Task<UnpinListResponse> UnpinList(string id, string list_id)
    {
        var response = await SendRequest(_httpClient.RequestAsyncDelete(
                DefaultTwitterApiUrlPrefix +
                $"/users/{id}/pinned_lists/{list_id}", AccessToken),
            ResponseParser.ParseUnpinListResponse);

        return response;
    }
    
    /// <summary>
    /// Returns the Lists pinned by a specified user.
    /// <a href="https://developer.twitter.com/en/docs/twitter-api/lists/pinned-lists/api-reference/get-users-id-pinned_lists">Twitter Docs</a>
    /// </summary>
    /// <param name="id">The user ID whose pinned Lists you would like to retrieve. The user’s ID must correspond to the user ID of the authenticated user.</param>
    /// <param name="fields">Data object to construct optional query fields - See documentation for available fields.</param>
    /// <exception cref="TwitterAuthorizationException">The request to Twitters OAuth2 token endpoint did not return success status code.
    /// Or the access token was null or empty.</exception>
    /// <exception cref="TwitterRequestException">The request did not return success status code. Or the request failed due to an underlying
    /// issue such as network connectivity, DNS failure, server certificate validation, timeout or missing critical argument.</exception>
    /// <returns></returns>
    public async Task<GetUserPinnedListsResponse> GetUserPinnedLists(string id, QueryFields? fields = null)
    {
        var response = await SendRequest(_httpClient.RequestAsyncGet(
                DefaultTwitterApiUrlPrefix +
                $"/users/{id}/pinned_lists?{fields ?? new QueryFields()}", AccessToken),
            ResponseParser.ParseGetUserPinnedListsResponse);

        return response;
    }
    
    /// <summary>
    /// Allows an authenticated user ID to mute the target user.
    /// <a href="https://developer.twitter.com/en/docs/twitter-api/users/mutes/api-reference/post-users-user_id-muting">Twitter Docs</a>
    /// </summary>
    /// <param name="id">The user ID who you would like to initiate the mute on behalf of. The user’s ID must correspond to the user ID of the authenticated user.</param>
    /// <param name="target_user_id">The user ID of the user that you would like the <see cref="id"/> to mute.</param>
    /// <exception cref="TwitterAuthorizationException">The request to Twitters OAuth2 token endpoint did not return success status code.
    /// Or the access token was null or empty.</exception>
    /// <exception cref="TwitterRequestException">The request did not return success status code. Or the request failed due to an underlying
    /// issue such as network connectivity, DNS failure, server certificate validation, timeout or missing critical argument.</exception>
    /// <returns></returns>
    public async Task<MuteUserResponse> MuteUser(string id, string target_user_id)
    {
        var body = new
        {
            target_user_id = target_user_id
        };

        var response = await SendRequest(_httpClient.RequestAsyncPost(
                DefaultTwitterApiUrlPrefix +
                $"/users/{id}/muting", StringContentParser.ApplicationJsonStringContent(body),
                AccessToken),
            ResponseParser.ParseMuteUserResponse);

        return response;
    }
    
    /// <summary>
    /// Allows an authenticated user ID to unmute the target user.
    /// The request succeeds with no action when the user sends a request to a user they're not muting or have already unmuted.
    /// <a href="https://developer.twitter.com/en/docs/twitter-api/users/mutes/api-reference/delete-users-user_id-muting">Twitter Docs</a>
    /// </summary>
    /// <param name="source_user_id">The user ID who you would like to initiate an unmute on behalf of. The user’s ID must correspond to the user ID of the authenticated user.</param>
    /// <param name="target_user_id">The user ID of the user that you would like the <see cref="source_user_id"/> to unmute.</param>
    /// <exception cref="TwitterAuthorizationException">The request to Twitters OAuth2 token endpoint did not return success status code.
    /// Or the access token was null or empty.</exception>
    /// <exception cref="TwitterRequestException">The request did not return success status code. Or the request failed due to an underlying
    /// issue such as network connectivity, DNS failure, server certificate validation, timeout or missing critical argument.</exception>
    /// <returns></returns>
    public async Task<UnmuteUserResponse> UnmuteUser(string source_user_id, string target_user_id)
    {
        var response = await SendRequest(_httpClient.RequestAsyncDelete(
                DefaultTwitterApiUrlPrefix +
                $"/users/{source_user_id}/muting/{target_user_id}", AccessToken),
            ResponseParser.ParseUnmuteUserResponse);

        return response;
    }
    
    /// <summary>
    /// Returns a list of users who are muted by the specified user ID.
    /// <a href="https://developer.twitter.com/en/docs/twitter-api/users/mutes/api-reference/get-users-muting">Twitter Docs</a>
    /// </summary>
    /// <param name="id">The user ID whose muted users you would like to retrieve. The user’s ID must correspond to the user ID of the authenticated user.</param>
    /// <param name="fields">Data object to construct optional query fields - See documentation for available fields.</param>
    /// <exception cref="TwitterAuthorizationException">The request to Twitters OAuth2 token endpoint did not return success status code.
    /// Or the access token was null or empty.</exception>
    /// <exception cref="TwitterRequestException">The request did not return success status code. Or the request failed due to an underlying
    /// issue such as network connectivity, DNS failure, server certificate validation, timeout or missing critical argument.</exception>
    /// <returns></returns>
    public async Task<GetUserMutesResponse> GetUserMutes(string id, QueryFields? fields = null)
    {
        var response = await SendRequest(_httpClient.RequestAsyncGet(
                DefaultTwitterApiUrlPrefix +
                $"/users/{id}/muting?{fields ?? new QueryFields()}", AccessToken),
            ResponseParser.ParseGetUserMutesResponse);

        return response;
    }
    
    /// <summary>
    /// Returns Quote Tweets for a Tweet specified by the requested Tweet ID.
    /// The Tweets returned by this endpoint count towards the Project-level
    /// <a href="https://developer.twitter.com/en/docs/twitter-api/tweet-caps">Tweet cap</a>.
    /// <a href="https://developer.twitter.com/en/docs/twitter-api/tweets/quote-tweets/api-reference/get-tweets-id-quote_tweets">Twitter Docs</a>
    /// </summary>
    /// <param name="id">Unique identifier of the Tweet to request.</param>
    /// <param name="fields">Data object to construct optional query fields - See documentation for available fields.</param>
    /// <exception cref="TwitterAuthorizationException">The request to Twitters OAuth2 token endpoint did not return success status code.
    /// Or the access token was null or empty.</exception>
    /// <exception cref="TwitterRequestException">The request did not return success status code. Or the request failed due to an underlying
    /// issue such as network connectivity, DNS failure, server certificate validation, timeout or missing critical argument.</exception>
    /// <returns></returns>
    public async Task<GetTweetQuoteTweetsResponse> GetTweetQuoteTweets(string id, QueryFields? fields = null)
    {
        var response = await SendRequest(_httpClient.RequestAsyncGet(
                DefaultTwitterApiUrlPrefix +
                $"/tweets/{id}/quote_tweets?{fields ?? new QueryFields()}", AccessToken),
            ResponseParser.ParseGetTweetQuoteTweetsResponse);

        return response;
    } 
    
    /// <summary>
    /// Hides or unhides a reply to a Tweet.
    /// <a href="https://developer.twitter.com/en/docs/twitter-api/tweets/hide-replies/api-reference/put-tweets-id-hidden">Twitter Docs</a>
    /// </summary>
    /// <param name="id">Unique identifier of the Tweet to hide. The Tweet must belong to a conversation initiated by
    /// the authenticating user.</param>
    /// <exception cref="TwitterAuthorizationException">The request to Twitters OAuth2 token endpoint did not return success status code.
    /// Or the access token was null or empty.</exception>
    /// <exception cref="TwitterRequestException">The request did not return success status code. Or the request failed due to an underlying
    /// issue such as network connectivity, DNS failure, server certificate validation, timeout or missing critical argument.</exception>
    /// <returns></returns>
    public async Task<HideReplyResponse> HideReply(string id)
    {
        var body = new
        {
            hidden = true
        };
        
        var response = await SendRequest(_httpClient.RequestAsyncPut(
                DefaultTwitterApiUrlPrefix +
                $"/tweets/{id}/hidden", StringContentParser.ApplicationJsonStringContent(body), AccessToken),
            ResponseParser.ParseHideReplyResponse);

        return response;
    }
    
    /// <summary>
    /// Hides or unhides a reply to a Tweet.
    /// <a href="https://developer.twitter.com/en/docs/twitter-api/tweets/hide-replies/api-reference/put-tweets-id-hidden">Twitter Docs</a>
    /// </summary>
    /// <param name="id">Unique identifier of the Tweet to unhide. The Tweet must belong to a conversation initiated by
    /// the authenticating user.</param>
    /// <exception cref="TwitterAuthorizationException">The request to Twitters OAuth2 token endpoint did not return success status code.
    /// Or the access token was null or empty.</exception>
    /// <exception cref="TwitterRequestException">The request did not return success status code. Or the request failed due to an underlying
    /// issue such as network connectivity, DNS failure, server certificate validation, timeout or missing critical argument.</exception>
    /// <returns></returns>
    public async Task<UnhideReplyResponse> UnhideReply(string id)
    {
        var body = new
        {
            hidden = false
        };
        
        var response = await SendRequest(_httpClient.RequestAsyncPut(
                DefaultTwitterApiUrlPrefix +
                $"/tweets/{id}/hidden", StringContentParser.ApplicationJsonStringContent(body), AccessToken),
            ResponseParser.ParseUnhideReplyResponse);

        return response;
    }
    
    /// <summary>
    /// Causes the user ID identified in the path parameter to Like the target Tweet.
    /// <a href="https://developer.twitter.com/en/docs/twitter-api/tweets/likes/api-reference/post-users-id-likes">Twitter Docs</a>
    /// </summary>
    /// <param name="id">The user ID who you are liking a Tweet on behalf of. The user’s ID must correspond to the user ID of the authenticated user.</param>
    /// <param name="tweet_id">The ID of the Tweet that you would like the user <see cref="id"/> to Like.</param>
    /// <exception cref="TwitterAuthorizationException">The request to Twitters OAuth2 token endpoint did not return success status code.
    /// Or the access token was null or empty.</exception>
    /// <exception cref="TwitterRequestException">The request did not return success status code. Or the request failed due to an underlying
    /// issue such as network connectivity, DNS failure, server certificate validation, timeout or missing critical argument.</exception>
    /// <returns></returns> 
    public async Task<LikeTweetResponse> LikeTweet(string id,string tweet_id)
    {
        var body = new
        {
            tweet_id = tweet_id
        };
        
        var response = await SendRequest(_httpClient.RequestAsyncPost(
                DefaultTwitterApiUrlPrefix +
                $"/users/{id}/likes", StringContentParser.ApplicationJsonStringContent(body), AccessToken),
            ResponseParser.ParseLikeTweetResponse);

        return response;
    }

    /// <summary>
    /// Allows a user or authenticated user ID to unlike a Tweet.
    /// The request succeeds with no action when the user sends a request to a user they're not liking the Tweet or have already unliked the Tweet.
    /// <a href="https://developer.twitter.com/en/docs/twitter-api/tweets/likes/api-reference/delete-users-id-likes-tweet_id">Twitter Docs</a>
    /// </summary>
    /// <param name="id">The user ID who you are liking a Tweet on behalf of. The user’s ID must correspond to the user ID of the authenticated user.</param>
    /// <param name="tweet_id">The ID of the Tweet that you would like the user <see cref="id"/> to unlike.</param>
    /// <exception cref="TwitterAuthorizationException">The request to Twitters OAuth2 token endpoint did not return success status code.
    /// Or the access token was null or empty.</exception>
    /// <exception cref="TwitterRequestException">The request did not return success status code. Or the request failed due to an underlying
    /// issue such as network connectivity, DNS failure, server certificate validation, timeout or missing critical argument.</exception>
    /// <returns></returns> 
    public async Task<UnlikeTweetResponse> UnlikeTweet(string id,string tweet_id)
    {
        var response = await SendRequest(_httpClient.RequestAsyncDelete(
                DefaultTwitterApiUrlPrefix +
                $"/users/{id}/likes/{tweet_id}", AccessToken),
            ResponseParser.ParseUnlikeTweetResponse);

        return response;
    }
    
    /// <summary>
    /// Allows you to get information about a user’s liked Tweets.
    /// <a href="https://developer.twitter.com/en/docs/twitter-api/tweets/likes/api-reference/get-users-id-liked_tweets">Twitter Docs</a>
    /// </summary>
    /// <param name="id">User ID of the user to request liked Tweets for.</param>
    /// <param name="fields">Data object to construct optional query fields - See documentation for available fields.</param>
    /// <exception cref="TwitterAuthorizationException">The request to Twitters OAuth2 token endpoint did not return success status code.
    /// Or the access token was null or empty.</exception>
    /// <exception cref="TwitterRequestException">The request did not return success status code. Or the request failed due to an underlying
    /// issue such as network connectivity, DNS failure, server certificate validation, timeout or missing critical argument.</exception>
    /// <returns></returns> 
    public async Task<GetUserLikedTweetsResponse> GetUserLikedTweets(string id, QueryFields? fields = null)
    {
        var response = await SendRequest(_httpClient.RequestAsyncGet(
                DefaultTwitterApiUrlPrefix +
                $"/users/{id}/liked_tweets?{fields ?? new QueryFields()}", AccessToken),
            ResponseParser.ParseGetUserLikedTweetsResponse);
    
        return response;
    }
    
    /// <summary>
    /// Allows you to get information about a Tweet’s liking users.
    /// <a href="https://developer.twitter.com/en/docs/twitter-api/tweets/likes/api-reference/get-tweets-id-liking_users">Twitter Docs</a>
    /// </summary>
    /// <param name="id">Tweet ID of the Tweet to request liking users of.</param>
    /// <param name="fields">Data object to construct optional query fields - See documentation for available fields.</param>
    /// <exception cref="TwitterAuthorizationException">The request to Twitters OAuth2 token endpoint did not return success status code.
    /// Or the access token was null or empty.</exception>
    /// <exception cref="TwitterRequestException">The request did not return success status code. Or the request failed due to an underlying
    /// issue such as network connectivity, DNS failure, server certificate validation, timeout or missing critical argument.</exception>
    /// <returns></returns> 
    public async Task<GetUsersLikingTweetResponse> GetUsersLikingTweet(string id, QueryFields? fields = null)
    {
        var response = await SendRequest(_httpClient.RequestAsyncGet(
                DefaultTwitterApiUrlPrefix +
                $"/tweets/{id}/liking_users?{fields ?? new QueryFields()}", AccessToken),
            ResponseParser.ParseGetUsersLikingTweetResponse);
    
        return response;
    }
    
    /// <summary>
    /// Causes the user ID identified in the parameter to Retweet the target Tweet.
    /// <a href="https://developer.twitter.com/en/docs/twitter-api/tweets/retweets/api-reference/post-users-id-retweets">Twitter Docs</a>
    /// </summary>
    /// <param name="id">The user ID who you are Retweeting a Tweet on behalf of. The user’s ID must correspond to the user ID of the authenticated user.</param>
    /// <param name="tweet_id">The ID of the Tweet that you would like the user <see cref="id"/> to Retweet.</param>
    /// <exception cref="TwitterAuthorizationException">The request to Twitters OAuth2 token endpoint did not return success status code.
    /// Or the access token was null or empty.</exception>
    /// <exception cref="TwitterRequestException">The request did not return success status code. Or the request failed due to an underlying
    /// issue such as network connectivity, DNS failure, server certificate validation, timeout or missing critical argument.</exception>
    /// <returns></returns> 
    public async Task<RetweetTweetResponse> RetweetTweet(string id, string tweet_id)
    {
        var body = new
        {
            tweet_id = tweet_id
        };
        
        var response = await SendRequest(_httpClient.RequestAsyncPost(
                DefaultTwitterApiUrlPrefix +
                $"/users/{id}/retweets", StringContentParser.ApplicationJsonStringContent(body), AccessToken),
            ResponseParser.ParseRetweetTweetResponse);
    
        return response;
    }
    
    /// <summary>
    /// Allows a user or authenticated user ID to remove the Retweet of a Tweet.
    /// The request succeeds with no action when the user sends a request to a user they're not Retweeting the Tweet or have already removed the Retweet of.
    /// <a href="https://developer.twitter.com/en/docs/twitter-api/tweets/retweets/api-reference/delete-users-id-retweets-tweet_id">Twitter Docs</a>
    /// </summary>
    /// <param name="id">The user ID who you are removing a the Retweet of a Tweet on behalf of. The user’s ID must correspond to the user ID of the authenticated user.</param>
    /// <param name="source_tweet_id">The ID of the Tweet that you would like the <see cref="id"/> to remove the Retweet of.</param>
    /// <exception cref="TwitterAuthorizationException">The request to Twitters OAuth2 token endpoint did not return success status code.
    /// Or the access token was null or empty.</exception>
    /// <exception cref="TwitterRequestException">The request did not return success status code. Or the request failed due to an underlying
    /// issue such as network connectivity, DNS failure, server certificate validation, timeout or missing critical argument.</exception>
    /// <returns></returns> 
    public async Task<UndoRetweetResponse> UndoRetweet(string id, string source_tweet_id)
    {
        var response = await SendRequest(_httpClient.RequestAsyncDelete(
                DefaultTwitterApiUrlPrefix +
                $"/users/{id}/retweets/{source_tweet_id}", AccessToken),
            ResponseParser.ParseUndoRetweetResponse);
    
        return response;
    }
    
    /// <summary>
    /// Allows you to get information about who has Retweeted a Tweet.
    /// <a href="https://developer.twitter.com/en/docs/twitter-api/tweets/retweets/api-reference/get-tweets-id-retweeted_by">Twitter Docs</a>
    /// </summary>
    /// <param name="id">Tweet ID of the Tweet to request Retweeting users of.</param>
    /// <param name="fields">Data object to construct optional query fields - See documentation for available fields.</param>
    /// <exception cref="TwitterAuthorizationException">The request to Twitters OAuth2 token endpoint did not return success status code.
    /// Or the access token was null or empty.</exception>
    /// <exception cref="TwitterRequestException">The request did not return success status code. Or the request failed due to an underlying
    /// issue such as network connectivity, DNS failure, server certificate validation, timeout or missing critical argument.</exception>
    /// <returns></returns> 
    public async Task<GetRetweetedByUsersResponse> GetRetweetedByUsers(string id, QueryFields? fields = null)
    {
        var response = await SendRequest(_httpClient.RequestAsyncGet(
                DefaultTwitterApiUrlPrefix +
                $"/tweets/{id}/retweeted_by?{fields ?? new QueryFields()}", AccessToken),
            ResponseParser.ParseGetRetweetedByUsersResponse);
    
        return response;
    }
    
    /// <summary>
    /// Returns a list of Direct Messages within a conversation specified in the <see cref="dm_conversation_id"/> path parameter.
    /// Messages are returned in reverse chronological order.
    /// <a href="https://developer.twitter.com/en/docs/twitter-api/direct-messages/lookup/api-reference/get-dm_conversations-dm_conversation_id-dm_events">Twitter Docs</a>
    /// </summary>
    /// <param name="dm_conversation_id">The id of the Direct Message conversation for which events are being retrieved.</param>
    /// <param name="fields">Data object to construct optional query fields - See documentation for available fields.</param>
    /// <exception cref="TwitterAuthorizationException">The request to Twitters OAuth2 token endpoint did not return success status code.
    /// Or the access token was null or empty.</exception>
    /// <exception cref="TwitterRequestException">The request did not return success status code. Or the request failed due to an underlying
    /// issue such as network connectivity, DNS failure, server certificate validation, timeout or missing critical argument.</exception>
    /// <returns></returns> 
    public async Task<GetDirectMessagesByConversationResponse> GetDirectMessagesByConversation(string dm_conversation_id, QueryFields? fields = null)
    {
        var response = await SendRequest(_httpClient.RequestAsyncGet(
                DefaultTwitterApiUrlPrefix +
                $"/dm_conversations/{dm_conversation_id}/dm_events?{fields ?? new QueryFields()}", AccessToken),
            ResponseParser.ParseGetDirectMessagesByConversationResponse);
    
        return response;
    }
    
    /// <summary>
    /// Returns a list of Direct Messages (DM) events within a 1-1 conversation with the user specified in the <see cref="participant_id"/> path parameter.
    /// Messages are returned in reverse chronological order.
    /// <a href="https://developer.twitter.com/en/docs/twitter-api/direct-messages/lookup/api-reference/get-dm_conversations-with-participant_id-dm_events">Twitter Docs</a>
    /// </summary>
    /// <param name="participant_id">The participant_id of the user that the authenticating user is having a 1-1 conversation with.</param>
    /// <param name="fields">Data object to construct optional query fields - See documentation for available fields.</param>
    /// <exception cref="TwitterAuthorizationException">The request to Twitters OAuth2 token endpoint did not return success status code.
    /// Or the access token was null or empty.</exception>
    /// <exception cref="TwitterRequestException">The request did not return success status code. Or the request failed due to an underlying
    /// issue such as network connectivity, DNS failure, server certificate validation, timeout or missing critical argument.</exception>
    /// <returns></returns> 
    public async Task<GetDirectMessagesByConversationIncludingExactlyTwoParticipantsResponse> GetDirectMessagesByConversationIncludingExactlyTwoParticipants(
        string participant_id, QueryFields? fields = null)
    {
        var response = await SendRequest(_httpClient.RequestAsyncGet(
                DefaultTwitterApiUrlPrefix +
                $"/dm_conversations/with/{participant_id}/dm_events?{fields ?? new QueryFields()}", AccessToken),
            ResponseParser.ParseGetDirectMessagesByConversationIncludingExactlyTwoParticipantsResponse);
    
        return response;
    }
    
    /// <summary>
    /// Returns a list of Direct Messages for the authenticated user, both sent and received. Direct Message events are returned in reverse
    /// chronological order. Supports retrieving events from the previous 30 days.
    /// <a href="https://developer.twitter.com/en/docs/twitter-api/direct-messages/lookup/api-reference/get-dm_events">Twitter Docs</a>
    /// </summary>
    /// <param name="fields">Data object to construct optional query fields - See documentation for available fields.</param>
    /// <exception cref="TwitterAuthorizationException">The request to Twitters OAuth2 token endpoint did not return success status code.
    /// Or the access token was null or empty.</exception>
    /// <exception cref="TwitterRequestException">The request did not return success status code. Or the request failed due to an underlying
    /// issue such as network connectivity, DNS failure, server certificate validation, timeout or missing critical argument.</exception>
    /// <returns></returns> 
    public async Task<GetDirectMessagesByUserResponse> GetDirectMessagesByUser(QueryFields? fields = null)
    {
        var response = await SendRequest(_httpClient.RequestAsyncGet(
                DefaultTwitterApiUrlPrefix +
                $"/dm_events?{fields ?? new QueryFields()}", AccessToken),
            ResponseParser.ParseGetDirectMessagesByUserResponse);
    
        return response;
    }
    
    private async Task<T> SendRequest<T>(Task<HttpResponseMessage> request, Func<string, T> parse)
    {
        try
        {
            EnsureAccessToken();

            var response = await request;

            var responseBodyJson = await response.Content.ReadAsStringAsync();

            EnsureStatusCode(response, responseBodyJson);

            return parse(responseBodyJson);
        }
        catch (TwitterAuthorizationException)
        {
            throw;
        }
        catch (TwitterRequestException ex)
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

    private void EnsureAccessToken()
    {
        if (IsNullOrEmpty(AccessToken))
        {
            throw new TwitterAuthorizationException
            {
                Error = "Access token null or empty",
                Message = "Complete the authorization code flow to set the access token",
                HelpLink = "https://github.com/SebastianSchavon/TwitterNetCore"
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