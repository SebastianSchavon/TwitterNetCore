namespace TwitterNetCore.Dtos;

public class GetUserPinnedListsResponse
{
    public List<List> data { get; set; }
    public Includes includes { get; set; }
    public Meta meta { get; set; }
}