using Moq;
using NUnit.Framework;
using TwitterNetCore;

namespace Tests;

public class TwitterClientTestBase
{
    private TwitterClient _twitterClient;

    [SetUp]
    public void Setup()
    {
        _twitterClient = SetupClient();
    }
    
    private static TwitterClient SetupClient()
    {
        var mock = new MockedHttpClient();

        mock.AddResponseFile("/2/tweets?ids=1611932793130438656,1611857881808375808",
            Path.Combine(TestContext.CurrentContext.TestDirectory, "Data/GetTweetsResponse.json"));
        mock.AddResponseFile("/2/tweets/1362462549133545160",
            Path.Combine(TestContext.CurrentContext.TestDirectory, "Data/GetTweetResponse.json"));
        mock.AddResponseFile("/2/tweets/4913354516013624625",
            Path.Combine(TestContext.CurrentContext.TestDirectory, "Data/DeleteTweetResponse.json"));
        mock.AddResponseFile("/2/tweets/44916140257536513/quote_tweets",
            Path.Combine(TestContext.CurrentContext.TestDirectory, "Data/GetTweetQuoteTweetsResponse.json"));
        mock.AddResponseFile("/2/tweets/214423424/hidden",
            Path.Combine(TestContext.CurrentContext.TestDirectory, "Data/HideReplyResponse.json"));
        mock.AddResponseFile("/2/tweets/234252341/hidden",
            Path.Combine(TestContext.CurrentContext.TestDirectory, "Data/UnhideReplyResponse.json"));
        mock.AddResponseFile("/2/tweets/123123122313213/liking_users",
            Path.Combine(TestContext.CurrentContext.TestDirectory, "Data/GetUsersLikingTweetResponse.json"));
        mock.AddResponseFile("/2/tweets/112335235243233/retweeted_by",
            Path.Combine(TestContext.CurrentContext.TestDirectory, "Data/GetRetweetedByUsersResponse.json"));
        mock.AddResponseFile("/2/tweets",
            Path.Combine(TestContext.CurrentContext.TestDirectory, "Data/CreateTweetResponse.json"));
        
        mock.AddResponseFile("/2/users/4913136246253545160",
            Path.Combine(TestContext.CurrentContext.TestDirectory, "Data/GetUserResponse.json"));
        mock.AddResponseFile("/2/users/by/username/TwitterDev",
            Path.Combine(TestContext.CurrentContext.TestDirectory, "Data/GetUserResponse.json"));
        mock.AddResponseFile("/2/users?ids=98285056,98285060",
            Path.Combine(TestContext.CurrentContext.TestDirectory, "Data/GetUsersResponse.json"));
        mock.AddResponseFile("/2/users/by?usernames=TwitterDevUno,TwitterDevDos,TwitterDevTres,TwitterDevCuatro",
            Path.Combine(TestContext.CurrentContext.TestDirectory, "Data/GetUsersResponse.json"));
        mock.AddResponseFile("/2/users/44123962357/liked_tweets",
            Path.Combine(TestContext.CurrentContext.TestDirectory, "Data/GetUserLikedTweetsResponse.json"));
        mock.AddResponseFile("/2/users/98285050/followers?",
            Path.Combine(TestContext.CurrentContext.TestDirectory, "Data/GetUserFollowersResponse.json"));
        mock.AddResponseFile("/2/users/44196397/following?",
            Path.Combine(TestContext.CurrentContext.TestDirectory, "Data/GetUserFollowingResponse.json"));
        mock.AddResponseFile("/2/users/1604625413625913345/following",
            Path.Combine(TestContext.CurrentContext.TestDirectory, "Data/FollowUserResponse.json"));
        mock.AddResponseFile("/2/users/1362591334516046254/following/1306004502313010816",
            Path.Combine(TestContext.CurrentContext.TestDirectory, "Data/UnFollowUserResponse.json"));
        mock.AddResponseFile("/2/users/1362545160462549133/blocking",
            Path.Combine(TestContext.CurrentContext.TestDirectory, "Data/BlockUserResponse.json"));
        mock.AddResponseFile("/2/users/1362451604625459133/blocking/2450283574",
            Path.Combine(TestContext.CurrentContext.TestDirectory, "Data/UnblockUserResponse.json"));
        mock.AddResponseFile("/2/users/1362413351604625459/blocking",
            Path.Combine(TestContext.CurrentContext.TestDirectory, "Data/GetBlockedUsersResponse.json"));
        mock.AddResponseFile("/2/users/142352313534/likes",
            Path.Combine(TestContext.CurrentContext.TestDirectory, "Data/LikeTweetResponse.json"));
        mock.AddResponseFile("/2/users/231323553525/likes/1323214538143",
            Path.Combine(TestContext.CurrentContext.TestDirectory, "Data/UnlikeTweetResponse.json"));
        mock.AddResponseFile("/2/users/11348282/owned_lists",
            Path.Combine(TestContext.CurrentContext.TestDirectory, "Data/GetUserListsResponse.json"));
        mock.AddResponseFile("/2/users/9117727484793372155/followed_lists",
            Path.Combine(TestContext.CurrentContext.TestDirectory, "Data/FollowListResponse.json"));
        mock.AddResponseFile("/2/users/2748479137257715391/followed_lists/9127472584771533791",
            Path.Combine(TestContext.CurrentContext.TestDirectory, "Data/UnfollowListResponse.json"));
        mock.AddResponseFile("/2/users/1134828211348/followed_lists",
            Path.Combine(TestContext.CurrentContext.TestDirectory, "Data/GetUserFollowedListsResponse.json"));
        mock.AddResponseFile("/2/users/5913160434562441362/pinned_lists",
            Path.Combine(TestContext.CurrentContext.TestDirectory, "Data/PinListResponse.json"));
        mock.AddResponseFile("/2/users/9131604624413345625/pinned_lists/316059143456213112",
            Path.Combine(TestContext.CurrentContext.TestDirectory, "Data/UnpinListResponse.json"));
        mock.AddResponseFile("/2/users/3160444166225354913/pinned_lists",
            Path.Combine(TestContext.CurrentContext.TestDirectory, "Data/GetUserPinnedListsResponse.json"));
        mock.AddResponseFile("/2/users/23134325/muting",
            Path.Combine(TestContext.CurrentContext.TestDirectory, "Data/MuteUserResponse.json"));
        mock.AddResponseFile("/2/users/2236423222/muting/235363634123",
            Path.Combine(TestContext.CurrentContext.TestDirectory, "Data/UnmuteUserResponse.json"));
        mock.AddResponseFile("/2/users/423536674746767/muting",
            Path.Combine(TestContext.CurrentContext.TestDirectory, "Data/GetUserMutesResponse.json"));
        mock.AddResponseFile("/2/users/123134234234324/retweets",
            Path.Combine(TestContext.CurrentContext.TestDirectory, "Data/RetweetTweetResponse.json"));
        mock.AddResponseFile("/2/users/324523412312123/retweets",
            Path.Combine(TestContext.CurrentContext.TestDirectory, "Data/UndoRetweetResponse.json"));
        
        mock.AddResponseFile("/2/lists/1362413351604625459?",
            Path.Combine(TestContext.CurrentContext.TestDirectory, "Data/GetListResponse.json"));
        mock.AddResponseFile("/2/lists/6241345935160251346",
            Path.Combine(TestContext.CurrentContext.TestDirectory, "Data/UpdateListResponse.json"));
        mock.AddResponseFile("/2/lists/2513624134593516046",
            Path.Combine(TestContext.CurrentContext.TestDirectory, "Data/DeleteListResponse.json"));
        mock.AddResponseFile("/2/lists/1533847917772912745/tweets",
            Path.Combine(TestContext.CurrentContext.TestDirectory, "Data/GetListTweetsResponse.json"));
        mock.AddResponseFile("/2/lists/1533847291274591777/members",
            Path.Combine(TestContext.CurrentContext.TestDirectory, "Data/AddListMemberResponse.json"));
        mock.AddResponseFile("/2/lists/8479177153372912745/members/3372912745177158479",
            Path.Combine(TestContext.CurrentContext.TestDirectory, "Data/RemoveListMemberResponse.json"));
        mock.AddResponseFile("/2/lists/8479177174553372912/members",
            Path.Combine(TestContext.CurrentContext.TestDirectory, "Data/GetListMembersResponse.json"));
        mock.AddResponseFile("/2/users/9127484791771533725/list_memberships",
            Path.Combine(TestContext.CurrentContext.TestDirectory, "Data/GetUserListMembershipsResponse.json"));
        mock.AddResponseFile("/2/lists/1274517917772953384/followers",
            Path.Combine(TestContext.CurrentContext.TestDirectory, "Data/GetListFollowersResponse.json"));
        mock.AddResponseFile("/2/lists",
            Path.Combine(TestContext.CurrentContext.TestDirectory, "Data/CreateListResponse.json"));
        
        mock.AddResponseFile("/2/dm_conversations/1496546669393345240-1603344624413562591/dm_events",
            Path.Combine(TestContext.CurrentContext.TestDirectory, "Data/GetDirectMessagesByConversationResponse.json"));
        mock.AddResponseFile("/2/dm_conversations/with/1274517917772953384/dm_events",
            Path.Combine(TestContext.CurrentContext.TestDirectory, "Data/GetDirectMessagesByConversationIncludingExactlyTwoParticipantsResponse.json"));
        mock.AddResponseFile("/2/dm_events?",
            Path.Combine(TestContext.CurrentContext.TestDirectory, "Data/GetDirectMessagesByUserResponse.json"));

        return new TwitterClient(mock.Object)
            { AccessToken = "Z0xrakFGem1DT3YzcjASPDJJAJ22kdw2DMskxVkNBYzkDWdcaseKEdco23dsDk22dkm2kdsE" };
    }
    
    [Test]
    public async Task TestGetTweet()
    {
        var response = await _twitterClient.GetTweet("1362462549133545160");

        Assert.That(response, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(string.IsNullOrEmpty(response.data.id), Is.False);
            Assert.That(string.IsNullOrEmpty(response.data.text), Is.False);
            Assert.That(string.IsNullOrEmpty(response.data.edit_history_tweet_ids[0]), Is.False);
        });
    }
    
    [Test]
    public async Task TestGetTweets()
    {
        var response = await _twitterClient.GetTweets("1611932793130438656,1611857881808375808");

        Assert.That(response, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(string.IsNullOrEmpty(response.data[0].id), Is.False);
            Assert.That(string.IsNullOrEmpty(response.data[0].text), Is.False);
            Assert.That(string.IsNullOrEmpty(response.data[0].edit_history_tweet_ids[0]), Is.False);
        });
    }

    [Test]
    public async Task TestPostTweet()
    {
        var response = await _twitterClient.CreateTweet(new
        {
            text = "Hello world!"
        });
        
        Assert.That(response, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(string.IsNullOrEmpty(response.data.text), Is.False);
            Assert.That(string.IsNullOrEmpty(response.data.id), Is.False);
        });
    }

    [Test]
    public async Task TestDeleteTweet()
    {
        var response = await _twitterClient.DeleteTweet("4913354516013624625");

        Assert.That(response.data.deleted, Is.EqualTo(true));
    }
    
    [Test]
    public async Task TestGetUserById()
    {
        var response = await _twitterClient.GetUserById("4913136246253545160");

        Assert.That(response, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(string.IsNullOrEmpty(response.data.id), Is.False);
            Assert.That(string.IsNullOrEmpty(response.data.username), Is.False);
        });
    }
    
    [Test]
    public async Task TestGetUserByUsername()
    {
        var response = await _twitterClient.GetUserByUsername("TwitterDev");

        Assert.That(response, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(string.IsNullOrEmpty(response.data.id), Is.False);
            Assert.That(string.IsNullOrEmpty(response.data.username), Is.False);
        });
    }
    
    [Test]
    public async Task TestGetUsersById()
    {
        var response = await _twitterClient.GetUsersById("98285056,98285060");

        Assert.That(response, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(string.IsNullOrEmpty(response.data[0].id), Is.False);
            Assert.That(string.IsNullOrEmpty(response.data[0].username), Is.False);
        });
    }
    
    [Test]
    public async Task TestGetUsersByUsername()
    {
        var response = await _twitterClient.GetUsersByUsername("TwitterDevUno,TwitterDevDos,TwitterDevTres,TwitterDevCuatro");

        Assert.That(response, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(string.IsNullOrEmpty(response.data[0].id), Is.False);
            Assert.That(string.IsNullOrEmpty(response.data[0].username), Is.False);
        });
    }
    
    [Test]
    public async Task TestGetUserFollowers()
    {
        var response = await _twitterClient.GetUserFollowers("98285050");

        Assert.That(response, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(string.IsNullOrEmpty(response.data[1].id), Is.False);
            Assert.That(string.IsNullOrEmpty(response.data[1].username), Is.False);
        });
    }

    [Test]
    public async Task TestGetUserFollowing()
    {
        var response = await _twitterClient.GetUserFollowings("44196397");

        
        Assert.That(response, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(string.IsNullOrEmpty(response.data[1].id), Is.False);
            Assert.That(string.IsNullOrEmpty(response.data[1].username), Is.False);
        });
    }

    [Test]
    public async Task TestFollowUser()
    {
        var response = await _twitterClient.FollowUser("1604625413625913345", "1604624411234513345");

        Assert.That(response, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(response.data.following, Is.EqualTo(true));
            Assert.That(response.data.pending_follow, Is.EqualTo(false));
        });
    }
    
    [Test]
    public async Task TestUnfollowUser()
    {
        var response = await _twitterClient.UnfollowUser("1362591334516046254", "1306004502313010816");

        Assert.That(response, Is.Not.Null);
        Assert.That(response.data.following, Is.EqualTo(false));
    }
    
    [Test]
    public async Task TestBlockUser()
    {
        var response = await _twitterClient.BlockUser("1362545160462549133", "1604624411234513345");

        Assert.That(response, Is.Not.Null);
        Assert.That(response.data.blocking, Is.EqualTo(true));
    }
    
    [Test]
    public async Task TestUnblockUser()
    {
        var response = await _twitterClient.UnblockUser("1362451604625459133", "2450283574");

        Assert.That(response, Is.Not.Null);
        Assert.That(response.data.blocking, Is.EqualTo(false));
    }
    
    [Test]
    public async Task TestGetBlockedUsers()
    {
        var response = await _twitterClient.GetBlockedUsers("1362413351604625459");

        Assert.Multiple(() =>
        {
            Assert.That(string.IsNullOrEmpty(response.data[0].id), Is.False);
            Assert.That(string.IsNullOrEmpty(response.data[0].username), Is.False);
        });
    }

    [Test]
    public async Task TestGetList()
    {
        var response = await _twitterClient.GetList("1362413351604625459");

        Assert.Multiple(() =>
        {
            Assert.That(string.IsNullOrEmpty(response.data.id), Is.False);
            Assert.That(string.IsNullOrEmpty(response.data.name), Is.False);
        });
    }
        
    [Test]
    public async Task TestGetUserLists()
    {
        var response = await _twitterClient.GetUserLists("11348282");

        Assert.Multiple(() =>
        {
            Assert.That(string.IsNullOrEmpty(response.data[0].id), Is.False);
            Assert.That(string.IsNullOrEmpty(response.data[0].name), Is.False);
        });
    }

    [Test]
    public async Task TestPostList()
    {
        var response = await _twitterClient.CreateList(new
        {
            name = "List name"
        });

        Assert.Multiple(() =>
        {
            Assert.That(string.IsNullOrEmpty(response.data.id), Is.False);
            Assert.That(string.IsNullOrEmpty(response.data.name), Is.False);
        });
    }
    
    [Test]
    public async Task TestUpdateList()
    {
        var response = await _twitterClient.UpdateList("6241345935160251346",new
        {
            name = "List name"
        });

        Assert.That(response.data.updated, Is.EqualTo(true));
    }
    
    [Test]
    public async Task TestDeleteList()
    {
        var response = await _twitterClient.DeleteList("2513624134593516046");

        Assert.That(response.data.deleted, Is.EqualTo(true));
    }
    
    [Test]
    public async Task TestGetListTweets()
    {
        var response = await _twitterClient.GetListTweets("1533847917772912745");

        Assert.Multiple(() =>
        {
            Assert.That(string.IsNullOrEmpty(response.data[0].id), Is.False);
            Assert.That(string.IsNullOrEmpty(response.data[0].text), Is.False);
        });
    }
    
    [Test]
    public async Task TestAddListMember()
    {
        var response = await _twitterClient.AddListMember("1533847291274591777","1274153384729591777");

        Assert.That(response.data.is_member, Is.EqualTo(true));
    }
    
    [Test]
    public async Task TestRemoveListMember()
    {
        var response = await _twitterClient.RemoveListMember("8479177153372912745","3372912745177158479");
        
        Assert.That(response.data.is_member, Is.EqualTo(false));
    }
    
    [Test]
    public async Task TestGetListMembers()
    {
        var response = await _twitterClient.GetListMembers("8479177174553372912");
        
        Assert.Multiple(() =>
        {
            Assert.That(string.IsNullOrEmpty(response.data[0].id), Is.False);
            Assert.That(string.IsNullOrEmpty(response.data[0].username), Is.False);
        });
    }
    
    [Test]
    public async Task TestGetUserListMemberships()
    {
        var response = await _twitterClient.GetUserListMemberships("9127484791771533725");
        
        Assert.Multiple(() =>
        {
            Assert.That(string.IsNullOrEmpty(response.data[0].id), Is.False);
            Assert.That(string.IsNullOrEmpty(response.data[0].name), Is.False);
        });
    }

    [Test]
    public async Task TestFollowList()
    {
        var response = await _twitterClient.FollowList("9117727484793372155","9117727493372155847");
        
        Assert.That(response.data.following, Is.EqualTo(true));
    }
    
    [Test]
    public async Task TestUnfollowList()
    {
        var response = await _twitterClient.UnfollowList("2748479137257715391","9127472584771533791");
        
        Assert.That(response.data.following, Is.EqualTo(false));
    }
    
    [Test]
    public async Task TestGetListFollowers()
    {
        var response = await _twitterClient.GetListFollowers("1274517917772953384");
        
        Assert.Multiple(() =>
        {
            Assert.That(string.IsNullOrEmpty(response.data[0].id), Is.False);
            Assert.That(string.IsNullOrEmpty(response.data[0].name), Is.False);
        });
    }
    
    [Test]
    public async Task TestGetUserFollowedLists()
    {
        var response = await _twitterClient.GetUserFollowedLists("1134828211348");
        
        Assert.Multiple(() =>
        {
            Assert.That(string.IsNullOrEmpty(response.data[0].id), Is.False);
            Assert.That(string.IsNullOrEmpty(response.data[0].name), Is.False);
        });
    }
    
    [Test]
    public async Task TestPinList()
    {
        var response = await _twitterClient.PinList("5913160434562441362","211316043412312");
        
        Assert.That(response.data.pinned, Is.EqualTo(true));
    }
    
    [Test]
    public async Task TestUnpinList()
    {
        var response = await _twitterClient.UnpinList("9131604624413345625","316059143456213112");
        
        Assert.That(response.data.pinned, Is.EqualTo(false));
    }
    
    [Test]
    public async Task TestGetUserPinnedLists()
    {
        var response = await _twitterClient.GetUserPinnedLists("3160444166225354913");
        
        Assert.Multiple(() =>
        {
            Assert.That(string.IsNullOrEmpty(response.data[0].id), Is.False);
            Assert.That(string.IsNullOrEmpty(response.data[0].name), Is.False);
        });
    }
    
    [Test]
    public async Task TestMuteUser()
    {
        var response = await _twitterClient.MuteUser("23134325","432432315");
        
        Assert.That(response.data.muting, Is.EqualTo(true));
    }
    
    [Test]
    public async Task TestUnmuteUser()
    {
        var response = await _twitterClient.UnmuteUser("2236423222","235363634123");
        
        Assert.That(response.data.muting, Is.EqualTo(false));
    }
    
    [Test]
    public async Task TestGetUserMutes()
    {
        var response = await _twitterClient.GetUserMutes("423536674746767");
        
        Assert.Multiple(() =>
        {
            Assert.That(string.IsNullOrEmpty(response.data[0].id), Is.False);
            Assert.That(string.IsNullOrEmpty(response.data[0].name), Is.False);
        });
    }
    
    [Test]
    public async Task TestGetTweetQuoteTweets()
    {
        var response = await _twitterClient.GetTweetQuoteTweets("44916140257536513");
        
        Assert.Multiple(() =>
        {
            Assert.That(string.IsNullOrEmpty(response.data[0].id), Is.False);
            Assert.That(string.IsNullOrEmpty(response.data[0].text), Is.False);
        });
    }
    
    [Test]
    public async Task TestHideReply()
    {
        var response = await _twitterClient.HideReply("214423424");
        
        Assert.That(response.data.hidden, Is.EqualTo(true));
    }
    
    [Test]
    public async Task TestUnhideReply()
    {
        var response = await _twitterClient.UnhideReply("234252341");
        
        Assert.That(response.data.hidden, Is.EqualTo(false));
    }
    
    [Test]
    public async Task TestLikeTweet()
    {
        var response = await _twitterClient.LikeTweet("142352313534", "135341423523");
        
        Assert.That(response.data.liked, Is.EqualTo(true));
    }
    
    [Test]
    public async Task TestUnlikeTweet()
    {
        var response = await _twitterClient.UnlikeTweet("231323553525","1323214538143");
        
        Assert.That(response.data.liked, Is.EqualTo(false));
    }
    
    [Test]
    public async Task TestGetUserLikedTweets()
    {
        var response = await _twitterClient.GetUserLikedTweets("44123962357");
        
        Assert.Multiple(() =>
        {
            Assert.That(string.IsNullOrEmpty(response.data[0].id), Is.False);
            Assert.That(string.IsNullOrEmpty(response.data[0].text), Is.False);
        });
    }
    
    [Test]
    public async Task TestGetUsersLikingTweet()
    {
        var response = await _twitterClient.GetUsersLikingTweet("123123122313213");
        
        Assert.Multiple(() =>
        {
            Assert.That(string.IsNullOrEmpty(response.data[0].id), Is.False);
            Assert.That(string.IsNullOrEmpty(response.data[0].name), Is.False);
        });
    }

    [Test]
    public async Task TestRetweetTweet()
    {
        var response = await _twitterClient.RetweetTweet("123134234234324", "123123123123");
        
        Assert.That(response.data.retweeted, Is.EqualTo(true));
    }
    
    [Test]
    public async Task TestUndoRetweet()
    {
        var response = await _twitterClient.UndoRetweet("324523412312123", "123123123123");
        
        Assert.That(response.data.retweeted, Is.EqualTo(false));
    }
    
    [Test]
    public async Task TestGetRetweetedByUsers()
    {
        var response = await _twitterClient.GetRetweetedByUsers("112335235243233");
        
        Assert.Multiple(() =>
        {
            Assert.That(string.IsNullOrEmpty(response.data[0].id), Is.False);
            Assert.That(string.IsNullOrEmpty(response.data[0].name), Is.False);
        });
    }

    [Test]
    public async Task TestGetDirectMessagesByConversation()
    {
        var response = await _twitterClient.GetDirectMessagesByConversation("1496546669393345240-1603344624413562591");
        
        Assert.Multiple(() =>
        {
            Assert.That(string.IsNullOrEmpty(response.data[0].id), Is.False);
            Assert.That(string.IsNullOrEmpty(response.data[0].text), Is.False);
        });
    }
    
    [Test]
    public async Task TestGetDirectMessagesByConversationIncludingExactlyTwoParticipants()
    {
        var response = await _twitterClient.GetDirectMessagesByConversationIncludingExactlyTwoParticipants("1274517917772953384");
        
        Assert.Multiple(() =>
        {
            Assert.That(string.IsNullOrEmpty(response.data[0].id), Is.False);
            Assert.That(string.IsNullOrEmpty(response.data[0].text), Is.False);
        });
    }

    [Test]
    public async Task TestGetDirectMessagesByUser()
    {
        var response = await _twitterClient.GetDirectMessagesByUser();
        
        Assert.Multiple(() =>
        {
            Assert.That(string.IsNullOrEmpty(response.data[0].id), Is.False);
            Assert.That(string.IsNullOrEmpty(response.data[0].text), Is.False);
        });
    }
}