namespace Model.KEA
{
    public class Word
    {
        public Word(string name, WordManager manager)
        {
            Name = name;
            WordManager = manager;
        }

        public WordManager WordManager { get; }

        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
