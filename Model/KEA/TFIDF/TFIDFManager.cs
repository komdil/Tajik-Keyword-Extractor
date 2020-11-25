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
                TF tF = new TF(wordToCalculate, documentToCalculate);
                double tFValue = tF.CalculateTF();
                IDF iDF = new IDF(documentsDataSet, wordToCalculate);
                double idfValue = iDF.CalculateIDF();
                TF_IDF tF_IDF = new TF_IDF(tFValue, idfValue);
                double tfIdfValue = tF_IDF.CalculateTF_IDF();
                tFIDFViews.Add(new TFIDFView { Word = wordToCalculate.Value, IDF = idfValue, TF = tFValue, TF_IDF = tfIdfValue });
            }

            return tFIDFViews;
        }
    }
}
