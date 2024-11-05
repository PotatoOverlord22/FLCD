using System.Text.Json.Serialization;

public class Transition
{
    [JsonPropertyName("from")]
    public string From { get; set; }
    [JsonPropertyName("to")]
    public string To { get; set; }
    [JsonPropertyName("on")]
    public string On { get; set; }
}