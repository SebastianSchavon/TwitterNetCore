namespace TwitterNetCore.Dtos;

public class GetListFollowersResponse
{
    public List<User> data { get; set; } = new();
    public Includes includes { get; set; }
    public Meta meta { get; set; }
}