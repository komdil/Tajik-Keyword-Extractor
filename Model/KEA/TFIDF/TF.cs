using System.Linq;

namespace Model.KEA.TFIDF
{
    public class TF
    {
        public TF(Word termin, Document document)
        {
            Termin = termin;
            Document = document;
        }

        public Word Termin { get; set; }

        public Document Document { get; set; }

        public double TFValue { get; set; }

        public void CalculateTF()
        {
            double countOfWordInDocument = Document.Sentences.SelectMany(a => a.Words).Count(a => a.Value.ToLower() == Termin.Value.ToLower());
            double countOfWordsInDcoument = Document.Sentences.SelectMany(a => a.Words).Count();
            TFValue = countOfWordInDocument / countOfWordsInDcoument;
        }
    }
}
