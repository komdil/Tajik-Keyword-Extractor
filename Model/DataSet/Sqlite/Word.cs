namespace Model.KEADataSet.Sqlite
{
    public class Word
    {
        public int _id { get; set; }

        public string word { get; set; }

        public string article { get; set; }

        public string dictionary_id { get; set; }
        public virtual Dictionary Dictionary { get; set; }
    }
}
