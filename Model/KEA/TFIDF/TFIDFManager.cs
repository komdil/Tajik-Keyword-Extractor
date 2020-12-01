using System.Collections.Generic;
using System.Linq;

namespace Model.KEA.TFIDF
{
    public class TFIDFManager
    {
        public List<TFIDFView> CalculateTFIDF(List<Document.Document> documentsDataSet, Document.Document documentToCalculate)
        {
            List<TFIDFView> tFIDFViews = new List<TFIDFView>();
            var wordsOfDocumentToCalculate = documentToCalculate.Sentences.SelectMany(s => s.Words).ToList();

            foreach (var wordToCalculate in wordsOfDocumentToCalculate.GroupBy(s => s.Value).Select(s => s.FirstOrDefault()))
            {
                var tFValue = CalCulateTF(wordToCalculate, documentToCalculate);
                double idfValue = CalCulateIDF(documentsDataSet, wordToCalculate);
                tFIDFViews.Add(CalculateTFIDF(wordToCalculate.Value, idfValue, tFValue));
            }

            return tFIDFViews;
        }

        public TFIDFView CalculateTFIDF(string word, double idfValue, double tFValue)
        {
            TF_IDF tF_IDF = new TF_IDF(tFValue, idfValue);
            double tfIdfValue = tF_IDF.CalculateTF_IDF();
            return new TFIDFView(word, idfValue, tFValue, tfIdfValue);
        }

        public double CalCulateTF(Word wordToCalculate, Document.Document documentToCalculate)
        {
            TF tF = new TF(wordToCalculate, documentToCalculate);
            return tF.CalculateTF();
        }

        public double CalCulateIDF(List<Document.Document> documentsDataSet, Word wordToCalculate)
        {
            IDF iDF = new IDF(documentsDataSet, wordToCalculate);
            return iDF.CalculateIDF();
        }
    }
}
