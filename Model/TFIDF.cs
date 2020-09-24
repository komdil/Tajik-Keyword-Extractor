namespace Model
{
    public class TF_IDF
    {
        public TF_IDF(TF tF, IDF iDF)
        {
            TF = tF;
            IDF = iDF;
        }

        public TF TF { get; set; }

        public IDF IDF { get; set; }

        public double TF_IDFValue { get; set; }

        public void CalculateTF_IDF()
        {
            TF_IDFValue = TF.TFValue * IDF.IDFValue;
        }
    }
}