Finite Automaton (FA) Program Documentation
Introduction
This program provides functionality to:

Read the elements of a Finite Automaton (FA) from a JSON file.
Display the elements of the FA in a structured format.
For a Deterministic Finite Automaton (DFA), verify if a sequence is accepted.
Detect tokens <identifier> and <integer constant> in a scanner program.
Program Structure
The program is written in C# and consists of the following main classes:

FiniteAutomaton: Represents the automaton, including states, transitions, alphabet, initial state, and final states. It includes methods to load the FA from a JSON file, display its components, check if a sequence is accepted, and detect specific tokens.
Transition: Represents a single transition in the FA with from, to, and on properties.
FiniteAutomatonData: A helper class used to parse the JSON file into structured data.
File Structure
The input file (FA.in) should be written in JSON format, detailing the elements of the finite automaton.

Example:
{
    "states": ["p", "q", "r"],
    "initial_state": "p",
    "final_states": ["r"],
    "alphabet": ["a", "b"],
    "transitions": [
        {"from": "p", "to": "q", "on": "a"},
        {"from": "q", "to": "r", "on": "b"},
        {"from": "r", "to": "r", "on": "a"},
        {"from": "p", "to": "p", "on": "a"},
        {"from": "p", "to": "p", "on": "b"},
        {"from": "q", "to": "q", "on": "a"}
    ]
}
JSON Fields
"states": List of all states in the automaton.
"initial_state": The initial state of the FA.
"final_states": List of final (accepting) states in the FA.
"alphabet": List of symbols in the FA's alphabet.
"transitions": List of transitions, each specifying the source state (from), the target state (to), and the symbol (on) that triggers the transition.

BNF / EBNF Format for FA.in File

<fa> ::= '{' "states" ':' <states> ',' "initial_state" ':' <initial_state> ',' 
              "final_states" ':' <final_states> ',' "alphabet" ':' <alphabet> ',' 
              "transitions" ':' <transitions> '}'

<states> ::= '[' <state> {',' <state>} ']'
<state> ::= '"' <letter> {<letter>} '"'

<initial_state> ::= '"' <state> '"'

<final_states> ::= '[' <state> {',' <state>} ']'

<alphabet> ::= '[' <symbol> {',' <symbol>} ']'
<symbol> ::= '"' <letter> '"'

<transitions> ::= '[' <transition> {',' <transition>} ']'
<transition> ::= '{' "from" ':' <state> ',' "to" ':' <state> ',' "on" ':' <symbol> '}'

Explanation of BNF/EBNF Format
<fa>: Represents the entire FA configuration, which includes states, initial state, final states, alphabet, and transitions.
<states>: A list of states in the automaton.
<initial_state>: The initial state of the FA.
<final_states>: A list of accepting states in the FA.
<alphabet>: A list of symbols in the FA's alphabet.
<transitions>: A list of transitions, each specifying from, to, and on values.

Program Functionality
Read FA from JSON File:
The program reads the FA data from the JSON file FA.in and maps it to the FiniteAutomaton object. This allows the program to initialize the FA’s states, transitions, alphabet, and other attributes from the file.

Display FA Elements:
A menu option enables the user to display:

The set of states
The alphabet
The transitions
The initial state
The final states
Verify Sequence Acceptance (for DFA):
Another option enables the user to input a sequence, and the program checks if this sequence is accepted by the DFA based on its transitions. The process involves:

Starting from the initial state
Following the transitions for each symbol in the sequence
Checking if the final state after processing all symbols is an accepting (final) state.
Token Detection for <identifier> and <integer constant>:
The program also extends to a scanner program where it identifies tokens. Here, <identifier> could match any valid sequence of letters or alphanumeric characters starting with a letter, and <integer constant> matches a sequence of digits.

