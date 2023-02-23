namespace TwitterNetCore;

public class SystemInteractionException : Exception
{
    public string Error { get; set; }
    public string Message { get; set; }
}