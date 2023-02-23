namespace TwitterNetCore.Dtos;

public class QueryFields
{
    public string? Expansions { get; set; }
    public string? Exclude { get; set; }
    public string? TweetFields { get; set; }
    public string? UserFields { get; set; }
    public string? MediaFields { get; set; }
    public string? PlaceFields { get; set; }
    public string? PollFields { get; set; }
    public string? ListFields { get; set; }
    public string? EventTypes { get; set; }
    public string? DmEventFields { get; set; }
    public int? MaxResults { get; set; }
    public string? PaginationToken { get; set; }

    public override string ToString()
    {
        return $"{(Expansions != null ? $"&expansions={Expansions}" : null)}" +
               $"{(Exclude != null ? $"&exclude={Exclude}" : null)}" +
               $"{(TweetFields != null ? $"&tweet.fields={TweetFields}" : null)}" +
               $"{(UserFields != null ? $"&user.fields={UserFields}" : null)}" +
               $"{(ListFields != null ? $"&list.fields={ListFields}" : null)}" +
               $"{(MediaFields != null ? $"&media.fields={MediaFields}" : null)}" +
               $"{(PlaceFields != null ? $"&place.fields={PlaceFields}" : null)}" +
               $"{(PollFields != null ? $"&poll.fields={PollFields}" : null)}" +
               $"{(EventTypes != null ? $"&event_types={EventTypes}" : null)}" +
               $"{(DmEventFields != null ? $"&dm_event.fields={DmEventFields}" : null)}" +
               $"{(MaxResults != null ? $"&max_results={MaxResults}" : null)}" +
               $"{(PaginationToken != null ? $"&pagination_token={PaginationToken}" : null)}";
    }
}