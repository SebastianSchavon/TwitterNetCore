namespace TwitterNetCore.Dtos;

public class User
{
    public DateTime created_at { get; set; }
    public PublicMetrics public_metrics { get; set; }
    public string name { get; set; }
    public string username { get; set; }
    public string id { get; set; }
    public string description { get; set; }
    public bool @protected { get; set; }
    public string profile_image_url { get; set; }
    public bool verified { get; set; }
    public string location { get; set; }
    public Entities entities { get; set; }
    public string url { get; set; }
    public string pinned_tweet_id { get; set; }
}