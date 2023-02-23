namespace TwitterNetCore.Dtos;

public class GetUserFollowingsResponse
{
    public List<User> data { get; set; } = new();
    public Includes includes { get; set; }
    public List<Error> errors { get; set; } = new();
    public Meta meta { get; set; }
}