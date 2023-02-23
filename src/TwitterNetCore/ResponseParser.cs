using Newtonsoft.Json;
using TwitterNetCore.Dtos;

namespace TwitterNetCore;

public static class ResponseParser
{
    public static TokenResponse ParseTokenResponse(string source)
    {
        return JsonConvert.DeserializeObject<TokenResponse>(source);
    }
    
    public static GetTweetResponse ParseGetTweetResponse(string source)
    {
        return JsonConvert.DeserializeObject<GetTweetResponse>(source);
    }
    
    public static GetTweetsResponse ParseGetTweetsResponse(string source)
    {
        return JsonConvert.DeserializeObject<GetTweetsResponse>(source);
    }
    
    public static CreateTweetResponse ParseCreateTweetResponse(string source)
    {
        return JsonConvert.DeserializeObject<CreateTweetResponse>(source);
    }
    
    public static DeleteTweetResponse ParseDeleteTweetResponse(string source)
    {
        return JsonConvert.DeserializeObject<DeleteTweetResponse>(source);
    }
    
    public static GetUserResponse ParseGetUserResponse(string source)
    {
        return JsonConvert.DeserializeObject<GetUserResponse>(source);
    }
    
    public static GetUsersResponse ParseGetUsersResponse(string source)
    {
        return JsonConvert.DeserializeObject<GetUsersResponse>(source);
    }

    public static GetUserFollowersResponse ParseGetUserFollowersResponse(string source)
    {
        return JsonConvert.DeserializeObject<GetUserFollowersResponse>(source);
    }
    
    public static GetUserFollowingsResponse ParseGetUserFollowingResponse(string source)
    {
        return JsonConvert.DeserializeObject<GetUserFollowingsResponse>(source);
    }

    public static FollowUserResponse ParseFollowUserResponse(string source)
    {
        return JsonConvert.DeserializeObject<FollowUserResponse>(source);
    }
    
    public static UnfollowUserResponse ParseUnfollowUserResponse(string source)
    {
        return JsonConvert.DeserializeObject<UnfollowUserResponse>(source);
    }
    
    public static BlockUserResponse ParseBlockUserResponse(string source)
    {
        return JsonConvert.DeserializeObject<BlockUserResponse>(source);
    }
    
    public static UnblockUserResponse ParseUnblockUserResponse(string source)
    {
        return JsonConvert.DeserializeObject<UnblockUserResponse>(source);
    }
    
    public static GetBlockedUsersResponse ParseGetBlockedUsersResponse(string source)
    {
        return JsonConvert.DeserializeObject<GetBlockedUsersResponse>(source);
    }
    
    public static GetListResponse ParseGetListResponse(string source)
    {
        return JsonConvert.DeserializeObject<GetListResponse>(source);
    }
    
    public static GetUserListsResponse ParseGetUserListsResponse(string source)
    {
        return JsonConvert.DeserializeObject<GetUserListsResponse>(source);
    }
    
    public static CreateListResponse ParseCreateListResponse(string source)
    {
        return JsonConvert.DeserializeObject<CreateListResponse>(source);
    }
    
    public static UpdateListResponse ParseUpdateListResponse(string source)
    {
        return JsonConvert.DeserializeObject<UpdateListResponse>(source);
    }
    
    public static DeleteListResponse ParseDeleteListResponse(string source)
    {
        return JsonConvert.DeserializeObject<DeleteListResponse>(source);
    }
    
    public static GetListTweetsResponse ParseGetListTweetsResponse(string source)
    {
        return JsonConvert.DeserializeObject<GetListTweetsResponse>(source);
    }
    
    public static AddListMemberResponse ParseAddListMemberResponse(string source)
    {
        return JsonConvert.DeserializeObject<AddListMemberResponse>(source);
    }
    
    public static RemoveListMemberResponse ParseRemoveListMemberResponse(string source)
    {
        return JsonConvert.DeserializeObject<RemoveListMemberResponse>(source);
    }
    
    public static GetListMembersResponse ParseGetListMemberResponse(string source)
    {
        return JsonConvert.DeserializeObject<GetListMembersResponse>(source);
    }
    
    public static GetUserListMembershipsResponse ParseGetUserListMembershipsResponse(string source)
    {
        return JsonConvert.DeserializeObject<GetUserListMembershipsResponse>(source);
    }
    
    public static GetListFollowersResponse ParseGetListFollowersResponse(string source)
    {
        return JsonConvert.DeserializeObject<GetListFollowersResponse>(source);
    }

    public static FollowListResponse ParseFollowListResponse(string source)
    {
        return JsonConvert.DeserializeObject<FollowListResponse>(source);
    }
    
    public static UnfollowListResponse ParseUnfollowListResponse(string source)
    {
        return JsonConvert.DeserializeObject<UnfollowListResponse>(source);
    }
    
    public static GetUserFollowedListsResponse ParseGetUserFollowedListsResponse(string source)
    {
        return JsonConvert.DeserializeObject<GetUserFollowedListsResponse>(source);
    }
    
    public static PinListResponse ParsePinListResponse(string source)
    {
        return JsonConvert.DeserializeObject<PinListResponse>(source);
    }
    
    public static UnpinListResponse ParseUnpinListResponse(string source)
    {
        return JsonConvert.DeserializeObject<UnpinListResponse>(source);
    }
    
    public static GetUserPinnedListsResponse ParseGetUserPinnedListsResponse(string source)
    {
        return JsonConvert.DeserializeObject<GetUserPinnedListsResponse>(source);
    }
    
    public static MuteUserResponse ParseMuteUserResponse(string source)
    {
        return JsonConvert.DeserializeObject<MuteUserResponse>(source);
    }
    
    public static UnmuteUserResponse ParseUnmuteUserResponse(string source)
    {
        return JsonConvert.DeserializeObject<UnmuteUserResponse>(source);
    }
    
    public static GetUserMutesResponse ParseGetUserMutesResponse(string source)
    {
        return JsonConvert.DeserializeObject<GetUserMutesResponse>(source);
    }
    
    public static GetTweetQuoteTweetsResponse ParseGetTweetQuoteTweetsResponse(string source)
    {
        return JsonConvert.DeserializeObject<GetTweetQuoteTweetsResponse>(source);
    }
    
    public static HideReplyResponse ParseHideReplyResponse(string source)
    {
        return JsonConvert.DeserializeObject<HideReplyResponse>(source);
    }
    
    public static UnhideReplyResponse ParseUnhideReplyResponse(string source)
    {
        return JsonConvert.DeserializeObject<UnhideReplyResponse>(source);
    }
    
    public static LikeTweetResponse ParseLikeTweetResponse(string source)
    {
        return JsonConvert.DeserializeObject<LikeTweetResponse>(source);
    }
    
    public static UnlikeTweetResponse ParseUnlikeTweetResponse(string source)
    {
        return JsonConvert.DeserializeObject<UnlikeTweetResponse>(source);
    }
    
    public static GetUserLikedTweetsResponse ParseGetUserLikedTweetsResponse(string source)
    {
        return JsonConvert.DeserializeObject<GetUserLikedTweetsResponse>(source);
    }
    
    public static GetUsersLikingTweetResponse ParseGetUsersLikingTweetResponse(string source)
    {
        return JsonConvert.DeserializeObject<GetUsersLikingTweetResponse>(source);
    }
    
    public static RetweetTweetResponse ParseRetweetTweetResponse(string source)
    {
        return JsonConvert.DeserializeObject<RetweetTweetResponse>(source);
    }
    
    public static UndoRetweetResponse ParseUndoRetweetResponse(string source)
    {
        return JsonConvert.DeserializeObject<UndoRetweetResponse>(source);
    }
    
    public static GetRetweetedByUsersResponse ParseGetRetweetedByUsersResponse(string source)
    {
        return JsonConvert.DeserializeObject<GetRetweetedByUsersResponse>(source);
    }
    
    public static GetDirectMessagesByConversationResponse ParseGetDirectMessagesByConversationResponse(string source)
    {
        return JsonConvert.DeserializeObject<GetDirectMessagesByConversationResponse>(source);
    }
    
    public static GetDirectMessagesByConversationIncludingExactlyTwoParticipantsResponse ParseGetDirectMessagesByConversationIncludingExactlyTwoParticipantsResponse(string source)
    {
        return JsonConvert.DeserializeObject<GetDirectMessagesByConversationIncludingExactlyTwoParticipantsResponse>(source);
    }
    
    public static GetDirectMessagesByUserResponse ParseGetDirectMessagesByUserResponse(string source)
    {
        return JsonConvert.DeserializeObject<GetDirectMessagesByUserResponse>(source);
    }
}