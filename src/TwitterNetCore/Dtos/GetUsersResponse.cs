namespace TwitterNetCore.Dtos;

public class GetUsersResponse
{
    public List<User> data { get; set; }= new();
    public Includes includes { get; set; }
}