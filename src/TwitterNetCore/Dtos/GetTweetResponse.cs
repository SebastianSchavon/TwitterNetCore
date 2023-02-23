namespace TwitterNetCore.Dtos;

public class GetTweetResponse
{
    public Tweet data { get; set; }
    public Includes includes { get; set; }
}