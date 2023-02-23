namespace TwitterNetCore.Dtos;

public class List
{
    public int member_count { get; set; }
    public string owner_id { get; set; }
    public int follower_count { get; set; }
    public string description { get; set; }
    public bool @private { get; set; }
    public string id { get; set; }
    public string name { get; set; }
    public DateTime created_at { get; set; }
}