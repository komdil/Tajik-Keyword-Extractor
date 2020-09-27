namespace Model.KEA
{
    public class Word
    {
        public Word(string value, WordManager manager)
        {
            Value = value;
            WordManager = manager;
        }

        public WordManager WordManager { get; }

        public string Value { get; set; }

        public override string ToString()
        {
            return Value;
        }
    }
}
