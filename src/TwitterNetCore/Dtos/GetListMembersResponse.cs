namespace TwitterNetCore.Dtos;

public class GetListMembersResponse
{
    public List<User> data { get; set; } = new();
    public Meta meta { get; set; }
}