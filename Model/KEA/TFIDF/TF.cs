using System.Linq;

namespace Model.KEA.TFIDF
{
    public class TF
    {
        public TF(string termin, Document document)
        {
            Termin = termin;
            Document = document;
        }

        public string Termin { get; set; }

        public Document Document { get; set; }

        public double TFValue { get; set; }

        public void CalculateTF()
        {
            double countOfWordInDocument = GetCountOfWordInDocument();
            double countOfWordsInDcoument = GetCountWordsInDocument();
            TFValue = countOfWordInDocument / countOfWordsInDcoument;
        }

        double GetCountOfWordInDocument()
        {
            return Document.Content.Split(' ').Count(w => w == Termin);
        }

        double GetCountWordsInDocument()
        {
            return Document.Content.Split(' ').Count();
        }
    }
}
