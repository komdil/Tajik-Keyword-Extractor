using System.Collections.Generic;
using System.Linq;
using TajikKEA.DataSet;

namespace TajikKEA.TFIDF
{
    public class TFIDFManager
    {
        public List<TFIDFView> CalculateTFIDF(List<Document.TajikDocument> documentsDataSet, Document.TajikDocument documentToCalculate)
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

        public List<TFIDFView> CalculateTFIDFWithIDF(Document.TajikDocument documentToCalculate, IDFCategory category = null)
        {
            List<TFIDFView> tFIDFViews = new List<TFIDFView>();
            var wordsOfDocumentToCalculate = documentToCalculate.Sentences.SelectMany(s => s.Words).ToList();
            foreach (var wordToCalculate in wordsOfDocumentToCalculate.GroupBy(s => s.Value).Select(s => s.FirstOrDefault()))
            {
                var tFValue = CalCulateTF(wordToCalculate, documentToCalculate);
                double idfValue;
                var res = KEAGlobal.Context.Words.FirstOrDefault(s => s.Content == wordToCalculate.Value);

                if (res != null)
                {
                    if (category != null)
                    {
                        var categoryLink = res.IDFCategoryLinks?.FirstOrDefault(s => s.Category?.Guid == category.Guid);
                        if (categoryLink != null && categoryLink.IDF > 0)
                        {
                            idfValue = categoryLink.IDF;
                        }
                        else
                        {
                            idfValue = res.CommonIDF;
                        }
                    }
                    else
                    {
                        idfValue = res.CommonIDF;
                    }
                }
                else
                {
                    List<Document.TajikDocument> documentsDataSet = new List<Document.TajikDocument> { documentToCalculate };
                    idfValue = CalCulateIDF(documentsDataSet, wordToCalculate);
                }
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

        public double CalCulateTF(TajikWord wordToCalculate, Document.TajikDocument documentToCalculate)
        {
            TF tF = new TF(wordToCalculate, documentToCalculate);
            return tF.CalculateTF();
        }

        public double CalCulateIDF(List<Document.TajikDocument> documentsDataSet, TajikWord wordToCalculate)
        {
            IDF iDF = new IDF(documentsDataSet, wordToCalculate);
            return iDF.CalculateIDF();
        }
    }
}
