namespace TwitterNetCore.Dtos;

public class GetUserListsResponse
{
    public List<List> data { get; set; }= new();
    public Includes includes { get; set; }
    public Meta meta { get; set; }
}