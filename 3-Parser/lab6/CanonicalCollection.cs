using System.Text;

namespace FLCD._3_Parser.lab6
{
    public class CanonicalCollection : HashSet<State>
    {
        public CanonicalCollection() : base() { }

        public CanonicalCollection(IEnumerable<State> collection) : base(collection) { }

        public CanonicalCollection(IEqualityComparer<State> comparer) : base(comparer) { }

        public CanonicalCollection(IEnumerable<State> collection, IEqualityComparer<State> comparer)
            : base(collection, comparer) { }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var other = (CanonicalCollection)obj;

            if (this.Count != other.Count)
            {
                return false;
            }

            foreach (var state in this)
            {
                if (!other.Contains(state))
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            int hash = 17;

            foreach (var state in this)
            {
                hash = hash * 31 + state.GetHashCode();
            }

            return hash;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("Canonical Collection:");
            int index = 0;

            foreach (State state in this)
            {
                sb.AppendLine($"State {index++}:");
                foreach (LR0Item item in state)
                {
                    sb.AppendLine(item.ToString());
                }
                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}