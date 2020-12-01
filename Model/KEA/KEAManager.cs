using Model.DataSet;
using Model.KEA.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Model.KEA
{
    public class KEAManager
    {
        public IWordContext Context { get; }
        public KEAManager(IWordContext context)
        {
            Context = context;
        }

        public List<string> GetSimpleKeywords(string text)
        {
            var document = new Document.Document(text);
            document.Sentences.ForEach(s => s.NormalizeWords());
            var tfIdf = KEAGlobal.TFIDFManager.CalculateTFIDF(new List<Document.Document> { document }, document).OrderByDescending(s => s.TF_IDF).ThenByDescending(s => s.TF).ThenByDescending(s => s.IDF);
            return tfIdf.Select(s => s.Word).ToList();
        }

        public Document.Document GetDocument(string path, bool normalize = false)
        {
            try
            {
                PDFHelper pDFHelper = new PDFHelper();
                var text = pDFHelper.ReadPdfFile(path);
                if (text.Length < 5)
                {
                    KEAGlobal.Logger.Log("Something wrong with this book " + path);
                }
                var document = new Document.Document(text);
                if (normalize)
                {
                    document.Sentences.ForEach(s => s.NormalizeWords());
                }
                return document;
            }
            catch (Exception ex)
            {
                KEAGlobal.Logger.Log("Error on reading book " + path + ex.Message);
                return new Document.Document("No content");
            }
        }
    }
}
