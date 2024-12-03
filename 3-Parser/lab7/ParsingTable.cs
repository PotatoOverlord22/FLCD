using FLCD._3_Parser.lab6;

namespace FLCD._3_Parser.lab7
{
    public class ParsingTable
    {
        private Dictionary<State, string> _actions;
        private Dictionary<(State, string), State> _goto;

        public ParsingTable()
        {
            _actions = new Dictionary<State, string>();
            _goto = new Dictionary<(State, string), State>();
        }

        public void AddAction(State state, string action)
        {
            _actions.Add(state, action);
        }

        public void AddGoto(State state, string symbol, State nextState)
        {
            _goto.Add((state, symbol), nextState);
        }
    }
}