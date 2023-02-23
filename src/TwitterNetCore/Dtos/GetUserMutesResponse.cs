namespace TwitterNetCore.Dtos;

public class GetUserMutesResponse
{
    public List<User> data { get; set; }
    public List<Error> errors { get; set; }
    public Meta meta { get; set; }
}