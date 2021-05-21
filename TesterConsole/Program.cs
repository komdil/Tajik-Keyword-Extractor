using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TajikKEA;
using TajikKEA.DataSet;
using TajikKEA.Document;
using TajikKEAHelper;
using TajikKEAJsonContext;

namespace TesterConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            TajikKEAContext jsonContext = new TajikKEAContext();
            KEAGlobal.InitiateKEAGlobal(jsonContext);
            PDFHelper pDFHelper = new PDFHelper();

            var badei = new IDFCategory() { Guid = Guid.NewGuid(), Name = "Бадеӣ" };
            var badeiDocs = GetDocuments(pDFHelper, @"C:\Users\dilshodk\Desktop\for me\Бадеи");

            var gumanitari = new IDFCategory() { Guid = Guid.NewGuid(), Name = "Гуманитарӣ" };
            var gumanitariDocs = GetDocuments(pDFHelper, @"C:\Users\dilshodk\Desktop\for me\Гуманитари");

            var иқтисодӣ = new IDFCategory() { Guid = Guid.NewGuid(), Name = "Иқтисодӣ" };
            var иқтисодӣDocs = GetDocuments(pDFHelper, @"C:\Users\dilshodk\Desktop\for me\Иқтисодӣ");

            var илмидақиқ = new IDFCategory() { Guid = Guid.NewGuid(), Name = "Илми дақиқ" };
            var илмидақиқDocs = GetDocuments(pDFHelper, @"C:\Users\dilshodk\Desktop\for me\Илми дакик");

            var сиёсӣ = new IDFCategory() { Guid = Guid.NewGuid(), Name = "Сиёсӣ" };
            var сиёсӣDocs = GetDocuments(pDFHelper, @"C:\Users\dilshodk\Desktop\for me\Сиёси");

            var техникӣ = new IDFCategory() { Guid = Guid.NewGuid(), Name = "Техникӣ" };
            var техникӣDocs = GetDocuments(pDFHelper, @"C:\Users\dilshodk\Desktop\for me\Техники");

            var тиб = new IDFCategory() { Guid = Guid.NewGuid(), Name = "Тиб" };
            var тибDocs = GetDocuments(pDFHelper, @"C:\Users\dilshodk\Desktop\for me\Тиб");

            var физика = new IDFCategory() { Guid = Guid.NewGuid(), Name = "Физика" };
            var физикаDocs = GetDocuments(pDFHelper, @"C:\Users\dilshodk\Desktop\for me\Физика");

            var химия = new IDFCategory() { Guid = Guid.NewGuid(), Name = "Химия" };
            var химияDocs = GetDocuments(pDFHelper, @"C:\Users\dilshodk\Desktop\for me\Химия");

            List<TajikDocument> allDocuments = badeiDocs.ToList();
            allDocuments.AddRange(gumanitariDocs);
            allDocuments.AddRange(иқтисодӣDocs);
            allDocuments.AddRange(илмидақиқDocs);
            allDocuments.AddRange(сиёсӣDocs);
            allDocuments.AddRange(техникӣDocs);
            allDocuments.AddRange(тибDocs);
            allDocuments.AddRange(физикаDocs);
            allDocuments.AddRange(химияDocs);
            var minimum = 0.00000000000000000000000112;
            foreach (var item in jsonContext.Words)
            {
                CalculateCategory(badei, item, badeiDocs);
                CalculateCategory(gumanitari, item, gumanitariDocs);
                CalculateCategory(иқтисодӣ, item, иқтисодӣDocs);
                CalculateCategory(илмидақиқ, item, илмидақиқDocs);
                CalculateCategory(сиёсӣ, item, сиёсӣDocs);
                CalculateCategory(техникӣ, item, техникӣDocs);
                CalculateCategory(тиб, item, тибDocs);
                CalculateCategory(физика, item, физикаDocs);
                CalculateCategory(химия, item, химияDocs);

                var word = new TajikWord(item.Content);
                var idf = KEAGlobal.TFIDFManager.CalCulateIDF(allDocuments, word);
                if (idf == 0)
                {
                    idf = minimum;
                }
                item.CommonIDF = idf;
            }
            foreach (var item in jsonContext.Words)
            {
                foreach (var item2 in item.IDFCategoryLinks)
                {
                    if (item2.IDF == 0)
                    {
                        item2.IDF = minimum;
                    }
                }
            }
            var text = JsonConvert.SerializeObject(jsonContext.Words, Formatting.Indented);
            File.WriteAllText("WordAllIDF.json", text);
            Console.ReadLine();
        }

        static void CalculateCategory(IDFCategory category, IWordDataSet wordsData, List<TajikDocument> documents)
        {
            var word = new TajikWord(wordsData.Content);
            var idf = KEAGlobal.TFIDFManager.CalCulateIDF(documents, word);
            if (wordsData.IDFCategoryLinks == null)
            {
                wordsData.IDFCategoryLinks = new List<IDFCategoryLink>();
            }
            wordsData.IDFCategoryLinks.Add(new IDFCategoryLink() { Category = category, IDF = idf });
        }

        static List<TajikDocument> GetDocuments(PDFHelper pDFHelper, string path)
        {
            var files = Directory.GetFiles(path);
            List<TajikDocument> documents = new List<TajikDocument>();
            foreach (var item in files)
            {
                var doctext = pDFHelper.ReadPdfFile(item);
                var document = new TajikDocument(doctext);
                documents.Add(document);
            }
            return documents;
        }

        static void PreviousMain()
        {
            //var text = File.ReadAllText("CalIDF.json");
            //var words = JsonConvert.DeserializeObject<List<Model.DataSet.Json.Word>>(text);
            //var random = new Random();
            //foreach (var item in words.Where(s => s.IDF == 0))
            //{
            //    double idf = random.Next(1, 20);
            //    double delimiter = 10000;
            //    item.IDF = idf / delimiter;
            //}
            //text = JsonConvert.SerializeObject(words, Formatting.Indented);
            //File.WriteAllText("CalIDF2.json", text);

            //JsonContext jsonContext = new JsonContext();
            //KEAGlobal.Logger.OnLog += Logger_OnLog;
            //KEAGlobal.InitiateKEAGlobal(jsonContext);
            //var tasks = ReadBooks();
            //Task.WaitAll(tasks);
            //Console.WriteLine("Thats all");
            //Console.ReadLine();

            //JsonContext jsonContext = new JsonContext();
            //KEAGlobal.Logger.OnLog += Logger_OnLog;
            //KEAGlobal.InitiateKEAGlobal(jsonContext);
            //var TFIDFManager = KEAGlobal.TFIDFManager;
            //var tasks = ReadBooks();
            //Task.WaitAll(tasks);
            //KEAGlobal.Logger.Log("Start calculating IDF");
            //var allDocuments = tasks.Select(s => s.Result);
            //var allwords = jsonContext.Words.Cast<Model.DataSet.Json.Word>().ToList();

            //int delay = 2000;
            //int counter = 0;
            //List<Task> idfTasks = new List<Task>();
            //foreach (var item in allwords)
            //{
            //    var task = Task.Run(new Action(() =>
            //      {
            //          try
            //          {
            //              double idf = TFIDFManager.CalCulateIDF(allDocuments.ToList(), new Model.KEA.Word(item.Content));
            //              item.IDF = idf;
            //          }
            //          catch
            //          {

            //          }
            //      }));
            //    idfTasks.Add(task);
            //    if (counter >= delay)
            //    {
            //        Task.WaitAll(idfTasks.ToArray());
            //        KEAGlobal.Logger.Log("Delay");
            //        idfTasks.Clear();
            //        counter = 0;
            //    }
            //    counter++;
            //}
            //var text = JsonConvert.SerializeObject(allwords, Formatting.Indented);
            //File.WriteAllText("CalIDF.json", text);
        }

        private static void Logger_OnLog(string log)
        {
            Console.WriteLine(log);
            //using (StreamWriter wr = new StreamWriter("unknown.txt", true))
            //{
            //    wr.WriteLine(log);
            //}
        }

        //static Task<TajikDocument>[] ReadBooks()
        //{
        //    var Files = new DirectoryInfo(@"D:\master degree\master\MAQOLA2\IDF");
        //    List<Task<TajikDocument>> tasks = new List<Task<TajikDocument>>();
        //    var allFiles = Files.GetFiles();
        //    KEAGlobal.Logger.Log($"Count of file: {allFiles.Count()}");
        //    foreach (var item in allFiles)
        //    {
        //        var task = new Task<TajikDocument>(() =>
        //        {
        //            var doc = KEAGlobal.KEAManager.GetDocument(item.FullName);
        //            KEAGlobal.Logger.Log($"Readed file: {item.FullName}");
        //            return doc;
        //        });
        //        task.Start();
        //        tasks.Add(task);
        //    }

        //    return tasks.ToArray();
        //}

        static void Splitwords()
        {
            var AllWords = JsonConvert.DeserializeObject<List<TajikJsonWord>>(File.ReadAllText("DataSet\\Json\\dataset.json"));

            var bandaks = AllWords.Where(w => w.IsBandak());
            var text = JsonConvert.SerializeObject(bandaks, Formatting.Indented);
            File.WriteAllText("Bandaks.json", text);

            var jonishin = AllWords.Where(s => s.IsJonishin());
            text = JsonConvert.SerializeObject(jonishin, Formatting.Indented);
            File.WriteAllText("Jonishins.json", text);

            var peshoyand = AllWords.Where(f => f.IsPeshoyand());
            text = JsonConvert.SerializeObject(peshoyand, Formatting.Indented);
            File.WriteAllText("Peshoyands.json", text);

            var words = AllWords.Where(s => !s.IsBandak() && !s.IsJonishin() && !s.IsPeshoyand());
            text = JsonConvert.SerializeObject(words, Formatting.Indented);
            File.WriteAllText("Words.json", text);
        }
    }
}
