namespace TwitterNetCore.Dtos;

public class GetTweetQuoteTweetsResponse
{
    public List<Tweet> data { get; set; }
    public Includes includes { get; set; }
    public Meta meta { get; set; }
}