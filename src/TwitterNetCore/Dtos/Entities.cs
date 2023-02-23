namespace TwitterNetCore.Dtos;

public class Entities
{
    public Url url { get; set; }
    public Description description { get; set; }
    public List<Hashtag> hashtags { get; set; }= new();
    public List<Url> urls { get; set; }= new();
    public List<Annotation> annotations { get; set; }= new();
    public List<Mention> mentions { get; set; }= new();
}