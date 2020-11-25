using System.Linq;

namespace Model.KEA.TFIDF
{
    public class TF
    {
        public TF(Word termin, Document.Document document)
        {
            Termin = termin;
            Document = document;
        }

        public Word Termin { get; set; }

        public Document.Document Document { get; set; }

        public double CalculateTF()
        {
            double countOfWordInDocument = Document.Sentences.SelectMany(a => a.Words).Count(a => a.Value.ToLower() == Termin.Value.ToLower());
            double countOfWordsInDcoument = Document.Sentences.SelectMany(a => a.Words).Count();
            return countOfWordInDocument / countOfWordsInDcoument;
        }
    }
}
