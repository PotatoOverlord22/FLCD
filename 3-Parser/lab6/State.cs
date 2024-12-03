namespace FLCD._3_Parser.lab6
{
    public class State : HashSet<LR0Item>
    {
        public State() : base() { }

        public State(IEnumerable<LR0Item> collection) : base(collection) { }

        public State(IEqualityComparer<LR0Item> comparer) : base(comparer) { }

        public State(IEnumerable<LR0Item> collection, IEqualityComparer<LR0Item> comparer)
            : base(collection, comparer) { }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var other = (State)obj;

            if (this.Count != other.Count)
            {
                return false;
            }

            foreach (var item in this)
            {
                if (!other.Contains(item))
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            int hash = 17;

            foreach (var item in this)
            {
                hash = hash * 31 + item.GetHashCode();
            }

            return hash;
        }
    }
}