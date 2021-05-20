namespace TajikKEA
{
    public class TajikWord
    {
        public TajikWord(string value)
        {
            Value = value;
        }

        public string Value { get; set; }

        public override string ToString()
        {
            return Value;
        }
    }
}
