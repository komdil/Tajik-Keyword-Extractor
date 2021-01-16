using Model.DataSet;
using Model.KEA.Document;
using Model.KEA.TFIDF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Model.KEA
{
    public class KEAManager
    {
        public IWordContext Context { get; }
        public KEAManager(IWordContext context)
        {
            Context = context;
        }

        public List<string> GetSimpleKeywordsIncludingIDF(string text)
        {
            var document = new Document.Document(text);
            document.Sentences.ForEach(s => s.NormalizeWords());
            var tfIdf = KEAGlobal.TFIDFManager.CalculateTFIDFWithIDF(new List<Document.Document> { document }, document).OrderByDescending(s => s.TF_IDF).ThenByDescending(s => s.TF).ThenByDescending(s => s.IDF);
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
                document.Name = path;
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

        public List<KeyValuePair<string, List<TFIDFView>>> CalculateTFIDFFromFolder(string folderPath)
        {
            var files = new DirectoryInfo(folderPath).GetFiles("*.pdf");
            List<Task<KeyValuePair<string, List<TFIDFView>>>> tasks = new List<Task<KeyValuePair<string, List<TFIDFView>>>>();
            foreach (var file in files)
            {
                var task = Task.Run(() => GetTFIDFFromFile(file));
                tasks.Add(task);
            }
            Task.WaitAll(tasks.ToArray());
            return tasks.Select(s => s.Result).ToList();
        }

        public KeyValuePair<string, List<TFIDFView>> GetTFIDFFromFile(FileInfo file)
        {
            var doc = KEAGlobal.KEAManager.GetDocument(file.FullName, true);
            var sheet = new List<TFIDFView>();
            foreach (var item in doc.Sentences.SelectMany(s => s.Words).GroupBy(s => s.Value).Select(s => s.FirstOrDefault()))
            {
                if (Context.StopWords.Any(s => s.Content == item.Value))
                    continue;
                var word = Context.WordsWithIDF.FirstOrDefault(s => s.Content == item.Value);
                var tf = KEAGlobal.TFIDFManager.CalCulateTF(item, doc);
                var idf = word.IDF;
                var result = KEAGlobal.TFIDFManager.CalculateTFIDF(item.Value, tf, idf);
                sheet.Add(result);
            }
            return new KeyValuePair<string, List<TFIDFView>>(file.Name.Replace(file.Extension, "").Replace(".", ""), sheet);
        }
    }
}
