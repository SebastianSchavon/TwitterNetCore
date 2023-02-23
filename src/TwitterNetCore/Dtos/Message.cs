namespace TwitterNetCore.Dtos;

public class Message
{
    public string event_type { get; set; }
    public string dm_conversation_id { get; set; }
    public DateTime created_at { get; set; }
    public string sender_id { get; set; }
    public string text { get; set; }
    public string id { get; set; }
}