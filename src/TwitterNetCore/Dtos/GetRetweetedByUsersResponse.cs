namespace TwitterNetCore.Dtos;

public class GetRetweetedByUsersResponse
{
    public List<User> data { get; set; }
    public Includes includes { get; set; }
    public Meta meta { get; set; }
}