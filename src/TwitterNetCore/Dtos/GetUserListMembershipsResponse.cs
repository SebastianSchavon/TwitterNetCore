namespace TwitterNetCore.Dtos;

public class GetUserListMembershipsResponse
{
    public List<List> data { get; set; } = new();
    public Includes includes { get; set; }
    public Meta meta { get; set; }
}