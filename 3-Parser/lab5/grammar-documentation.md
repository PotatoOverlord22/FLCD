Documentation for Grammar Class
The Grammar class provides functionality for handling and analyzing formal grammars. It supports reading grammar data from a file, displaying components of the grammar, retrieving specific productions, and validating if the grammar is context-free.

Supported Operations
Read a Grammar from a File
Load grammar data, including non-terminals, terminals, and productions, from a specified file.

Print the Set of Non-Terminals
Display all non-terminal symbols defined in the grammar.

Print the Set of Terminals
Display all terminal symbols defined in the grammar.

Print the Set of Productions
Display all production rules in the grammar in a readable format.

Retrieve Productions for a Given Non-Terminal
Fetch and display the specific production rules associated with a given non-terminal.

Check if the Grammar is a Context-Free Grammar (CFG)
Validate whether the grammar adheres to the definition of a context-free grammar:

The left-hand side (LHS) of every production must be a single non-terminal.
The right-hand side (RHS) consists of terminals, non-terminals, or ε (epsilon).
Input Files
g1.txt
A simple grammar example provided in the course/seminar. It contains basic grammar structures to test the operations.

g2.txt
A more complex grammar defining the syntax rules of a minilanguage. This grammar corresponds to the syntax rules described in Lab 1b.

Class Structure
Properties
GrammarData:
A GrammarData object containing:
StartSymbol (string): The grammar's start symbol.
NonTerminals (HashSet<string>): The set of non-terminal symbols.
Terminals (HashSet<string>): The set of terminal symbols.
Productions (Dictionary<string, HashSet<string>>): The set of production rules.
Methods
static Grammar LoadFromJson(string filePath)
Reads grammar data from a JSON file and creates a Grammar instance.
Parameters:

filePath: The file path to the grammar JSON file.
Returns:
A Grammar instance populated with data from the file.
void DisplayGrammarData()
Prints the grammar's start symbol, non-terminals, terminals, and productions in a readable format.

string GetProduction(string nonTerminal)
Retrieves and prints all productions for a given non-terminal.
Parameters:

nonTerminal: The non-terminal whose productions are requested.
Returns:
A formatted string of the productions or an error message if the non-terminal is not found.
bool IsCFG(bool parseByWords)
Validates if the grammar is context-free.
Parameters:

parseByWords: If true, the RHS of productions is parsed by words (split by spaces). If false, the RHS is parsed character-by-character.
Returns:
true if the grammar is CFG, otherwise false.
Additional Methods for Required Operations:

void DisplayNonTerminals()
Prints the set of non-terminal symbols.
void DisplayTerminals()
Prints the set of terminal symbols.
void DisplayProductions()
Prints all production rules in the grammar.