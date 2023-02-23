namespace TwitterNetCore.Dtos;

public class GetDirectMessagesByConversationIncludingExactlyTwoParticipantsResponse
{
    public List<Message> data { get; set; }
    public Includes includes { get; set; }
    public Meta meta { get; set; }
}