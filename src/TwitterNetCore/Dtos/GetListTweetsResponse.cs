namespace TwitterNetCore.Dtos;

public class GetListTweetsResponse
{
    public List<Tweet> data { get; set; } = new();
    public Includes includes { get; set; }
    public Meta meta { get; set; }
}