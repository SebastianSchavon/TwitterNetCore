namespace TwitterNetCore.Dtos;

public class Tweet
{
    public Attachments attachments { get; set; }
    public string author_id { get; set; }
    public string conversation_id { get; set; }
    public List<ContextAnnotation> context_annotations { get; set; }
    public DateTime created_at { get; set; }
    public EditControls edit_controls { get; set; }
    public List<string> edit_history_tweet_ids { get; set; } = new();
    public Entities entities { get; set; }
    public string id { get; set; }
    public PublicMetrics public_metrics { get; set; }
    public string lang { get; set; }
    public bool possibly_sensitive { get; set; }
    public string reply_settings { get; set; }
    public List<ReferencedTweet> referenced_tweets { get; set; }
    public string text { get; set; }
}