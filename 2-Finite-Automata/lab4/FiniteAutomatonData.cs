using System.Text.Json.Serialization;

public class FiniteAutomatonData
{
    [JsonPropertyName("states")]
    public List<string> States { get; set; }
    [JsonPropertyName("initial_state")]
    public string InitialState { get; set; }
    [JsonPropertyName("final_states")]
    public List<string> FinalStates { get; set; }
    [JsonPropertyName("alphabet")]
    public List<string> Alphabet { get; set; }
    [JsonPropertyName("transitions")]
    public List<Transition> Transitions { get; set; }
}