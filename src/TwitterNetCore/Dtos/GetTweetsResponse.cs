namespace TwitterNetCore.Dtos;

public class GetTweetsResponse
{
    public List<Tweet> data { get; set; }= new();
    public Includes includes { get; set; }
}