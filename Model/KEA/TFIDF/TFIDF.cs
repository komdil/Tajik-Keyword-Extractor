namespace TajikKEA.TFIDF
{
    public class TF_IDF
    {
        public TF_IDF(double tF, double iDF)
        {
            TF = tF;
            IDF = iDF;
        }

        public double TF { get; set; }

        public double IDF { get; set; }

        public double CalculateTF_IDF()
        {
            return TF * IDF;
        }
    }
}