using System.Text;
using System.Text.Json;

namespace TwitterNetCore;

public static class StringContentParser
{
    public static StringContent ApplicationJsonStringContent(object body)
    {
        return new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");
    }
    
    public static StringContent ApplicationUrlEncoded(string body)
    {
        return new StringContent(body, Encoding.UTF8, "application/x-www-form-urlencoded");
    }
}