namespace TwitterNetCore.Dtos;

public class GetBlockedUsersResponse
{
    public List<User> data { get; set; } = new();
    public List<Error> errors { get; set; }
    public Meta meta { get; set; }
}