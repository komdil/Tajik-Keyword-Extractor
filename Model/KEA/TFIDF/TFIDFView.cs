namespace Model.KEA.TFIDF
{
    public class TFIDFView
    {
        public TFIDFView(string word, double tf, double idf, double tfIdf)
        {
            Word = word;
            TF = tf;
            IDF = idf;
            TF_IDF = tfIdf;
        }

        public string Word { get; set; }

        public double TF { get; set; }

        public double IDF { get; set; }

        public double TF_IDF { get; set; }

        public override string ToString()
        {
            return Word;
        }
    }
}
