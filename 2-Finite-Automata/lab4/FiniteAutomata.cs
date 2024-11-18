using System.Text.Json;

public class FiniteAutomaton
{
    public HashSet<string> States { get; set; }
    public string InitialState { get; set; }
    public HashSet<string> FinalStates { get; set; }
    public HashSet<string> Alphabet { get; set; }
    public List<Transition> Transitions { get; set; }

    public FiniteAutomaton(HashSet<string> states, string initialState, HashSet<string> finalStates,
                           HashSet<string> alphabet, List<Transition> transitions)
    {
        States = states;
        InitialState = initialState;
        FinalStates = finalStates;
        Alphabet = alphabet;
        Transitions = transitions;
    }

    public static FiniteAutomaton LoadFromJson(string filePath)
    {
        var jsonString = File.ReadAllText(filePath);
        FiniteAutomatonData faData = JsonSerializer.Deserialize<FiniteAutomatonData>(jsonString);

        HashSet<string> states = new HashSet<string>(faData.States);
        HashSet<string> alphabet = new HashSet<string>(faData.Alphabet);
        HashSet<string> finalStates = new HashSet<string>(faData.FinalStates);
        List<Transition> transitions = faData.Transitions;

        return new FiniteAutomaton(states, faData.InitialState, finalStates, alphabet, transitions);
    }

    public void Display()
    {
        Console.WriteLine("States: " + string.Join(", ", States));
        Console.WriteLine("Initial State: " + InitialState);
        Console.WriteLine("Final States: " + string.Join(", ", FinalStates));
        Console.WriteLine("Alphabet: " + string.Join(", ", Alphabet));
        Console.WriteLine("Transitions:");

        foreach (var transition in Transitions)
        {
            Console.WriteLine($"  {transition.From} --{transition.On}--> {transition.To}");
        }
    }

    public bool IsAccepted(string sequence)
    {
        string currentState = InitialState;
        foreach (char symbol in sequence)
        {
            string symbolStr = symbol.ToString();
            Transition? transition = Transitions.Find(t => t.From == currentState && t.On == symbolStr);

            if (transition != null)
            {
                currentState = transition.To;
            }
            else
            {
                return false;
            }
        }

        return FinalStates.Contains(currentState);
    }
}