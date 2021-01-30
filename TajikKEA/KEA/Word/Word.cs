namespace TajikKEA
{
    public class Word
    {
        public Word(string value)
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
