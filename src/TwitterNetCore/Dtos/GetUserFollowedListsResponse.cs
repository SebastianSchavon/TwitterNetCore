namespace TwitterNetCore.Dtos;

public class GetUserFollowedListsResponse
{
    public List<List> data { get; set; }
    public Includes includes { get; set; }
    public Meta meta { get; set; }
}