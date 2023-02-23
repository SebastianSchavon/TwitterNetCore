namespace TwitterNetCore.Dtos;

public class GetUsersLikingTweetResponse
{
    public List<User> data { get; set; }
    public Includes includes { get; set; }
    public Meta meta { get; set; }
}