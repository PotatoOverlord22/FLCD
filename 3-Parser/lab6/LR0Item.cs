using System.Text;

namespace FLCD._3_Parser.lab6
{
    public class LR0Item
    {
        private int _dotIndex;

        public string Lhs { get; set; }

        public List<string> RhsTokens { get; set; }

        public int DotIndex
        {
            get => _dotIndex;
            set
            {
                if (value < 0)
                {
                    _dotIndex = 0;
                    return;
                }

                if (value > RhsTokens.Count)
                {
                    _dotIndex = RhsTokens.Count;
                    return;
                }
            }
        }

        public string? CurrentSymbol
        {
            get => DotIndex < RhsTokens.Count ? RhsTokens[DotIndex] : null;
        }

        public LR0Item(string lhs, List<string> rhsTokens, int dotIndex)
        {
            Lhs = lhs;
            RhsTokens = rhsTokens;
            _dotIndex = dotIndex;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var other = (LR0Item)obj;

            if (!Lhs.Equals(other.Lhs) || DotIndex != other.DotIndex)
            {
                return false;
            }

            if (RhsTokens.Count != other.RhsTokens.Count)
            {
                return false;
            }

            for (int i = 0; i < RhsTokens.Count; i++)
            {
                if (!RhsTokens[i].Equals(other.RhsTokens[i]))
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            int rhsHash = 17;
            foreach (var token in RhsTokens)
            {
                rhsHash = rhsHash * 31 + token.GetHashCode();
            }

            return HashCode.Combine(Lhs, rhsHash, DotIndex);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append(Lhs);
            sb.Append(" -> ");
            for (int i = 0; i < RhsTokens.Count; i++)
            {
                if (i == DotIndex)
                {
                    sb.Append('.');
                }
                sb.Append(RhsTokens[i]);
            }

            if (DotIndex == RhsTokens.Count)
            {
                sb.Append('.');
            }

            return sb.ToString();
        }
    }
}