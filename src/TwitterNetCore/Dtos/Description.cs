namespace TwitterNetCore.Dtos;

public class Description
{
    public List<Url> urls { get; set; }= new();
    public List<Hashtag> hashtags { get; set; }= new();
    public List<Mention> mentions { get; set; }= new();
}