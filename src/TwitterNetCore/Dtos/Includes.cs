namespace TwitterNetCore.Dtos;

public class Includes
{
    public List<Medium> media { get; set; }= new();
    public List<User> users { get; set; }= new();
    public List<Tweet> tweets { get; set; }= new();
}