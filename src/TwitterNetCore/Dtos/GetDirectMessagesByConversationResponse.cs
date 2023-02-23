namespace TwitterNetCore.Dtos;

public class GetDirectMessagesByConversationResponse
{
    public List<Message> data { get; set; }
    public Includes includes { get; set; }
    public Meta meta { get; set; }
}