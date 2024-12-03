using FLCD._3_Parser.lab6;
using FLCD._3_Parser.lab7;
using System.Text;

namespace FLCD._3_Parser.lab5
{
    public class Parser
    {
        private readonly Grammar grammar;

        public Parser(Grammar grammar)
        {
            this.grammar = grammar.GetEnrichedGrammar();
        }

        public State Closure(State initialItems)
        {
            var closure = new State(initialItems);
            bool changed;

            do
            {
                changed = false;
                var newItems = new State();

                foreach (LR0Item item in closure)
                {
                    string currentElement = item.CurrentSymbol;
                    if (currentElement == null)
                    {
                        continue;
                    }

                    if (currentElement == Grammar.Epsilon)
                    {
                        continue;
                    }

                    if (grammar.GrammarData.Terminals.Contains(currentElement))
                    {
                        continue;
                    }

                    foreach (string production in grammar.GrammarData.Productions[currentElement])
                    {
                        var newItem = new LR0Item(currentElement, grammar.ParseRule(production), 0);
                        if (!closure.Contains(newItem))
                        {
                            newItems.Add(newItem);
                            changed = true;
                        }
                    }
                }
                foreach (var newItem in newItems)
                {
                    closure.Add(newItem);
                }

            } while (changed);

            return closure;
        }


        public State Goto(State state, string symbol)
        {
            var nextState = new State();
            foreach (LR0Item item in state)
            {
                if (item.CurrentSymbol == symbol)
                {
                    var stateItem = new LR0Item(item.Lhs, item.RhsTokens, item.DotIndex + 1);
                    nextState.Add(stateItem);
                }
            }

            return Closure(nextState);
        }

        public CanonicalCollection CreateCannonicalCollection()
        {
            var startItem = new LR0Item(grammar.GrammarData.StartSymbol, grammar.ParseRule(grammar.GrammarData.Productions[grammar.GrammarData.StartSymbol].First()), 0);
            var startState = Closure(new State { startItem });

            CanonicalCollection canonicalCollection = new CanonicalCollection(new HashSet<State> { startState });
            bool changed;

            do
            {
                changed = false;
                var newStates = new CanonicalCollection();

                foreach (State state in canonicalCollection)
                {
                    foreach (string symbol in grammar.GrammarData.NonTerminals.Concat(grammar.GrammarData.Terminals))
                    {
                        var nextState = Goto(state, symbol);
                        if (nextState.Count == 0)
                        {
                            continue;
                        }

                        if (!canonicalCollection.Contains(nextState) && !newStates.Contains(nextState))
                        {
                            newStates.Add(nextState);
                            changed = true;
                        }
                    }
                }

                foreach (var state in newStates)
                {
                    canonicalCollection.Add(state);
                }
            }
            while (changed);

            return canonicalCollection;
        }

        public ParsingTable CreateParsingTable()
        {
            ParsingTable parsingTable = new ParsingTable();
            var canonicalCollection = CreateCannonicalCollection();

            foreach(State state in canonicalCollection)
            {
                foreach(LR0Item item in state)
                {
                    if (item.DotIndex >= 0 && item.DotIndex < item.RhsTokens.Count)
                    {
                        parsingTable.AddAction(state, $"shift {item.RhsTokens[item.DotIndex]}");
                        continue;
                    }

                    if (item.DotIndex >= item.RhsTokens.Count)
                    {
                        if (item.Lhs == grammar.GrammarData.StartSymbol)
                        {
                            parsingTable.AddAction(state, "accept");
                        }
                        else
                        {
                            parsingTable.AddAction(state, "r" + grammar.GrammarData.Productions[item.Lhs].ToList().IndexOf(string.Join(" ", item.RhsTokens)) + 1);
                        }
                    }
                }
            }

            return parsingTable;
        }
    }
}