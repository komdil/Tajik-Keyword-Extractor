using Model.DataSet.Json;
using Model.DataSet.SqlServer;
using Model.KEA;
using Model.KEA.Document;
using Model.KEA.TFIDF;
using Model.KEADataSet.Sqlite;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesterConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            JsonContext jsonContext = new JsonContext();
            KEAGlobal.Logger.OnLog += Logger_OnLog;
            KEAGlobal.InitiateKEAGlobal(jsonContext);
            MSExcelHelper.ExtractResult($"WordsWithIDF.csv", jsonContext.WordsWithIDF.OrderByDescending(d => d.IDF).ToList());

            //Task.Run(new Action(() =>
            //{
            //    var results = KEAGlobal.KEAManager.CalculateTFIDFFromFolder(@"D:\master degree\master\MAQOLA2\MainWork\Fizika");
            //    foreach (var item in results)
            //    {
            //        var sheet = item.Value.OrderByDescending(s => s.TF_IDF).ToList().Take(30);
            //        MSExcelHelper.ExtractResult($"{item.Key}.csv", sheet.ToList());
            //    }
            //    Console.WriteLine("Finished");
            //}));
            Console.ReadLine();
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

        static Task<Document>[] ReadBooks()
        {
            var Files = new DirectoryInfo(@"D:\master degree\master\MAQOLA2\IDF");
            List<Task<Document>> tasks = new List<Task<Document>>();
            var allFiles = Files.GetFiles();
            KEAGlobal.Logger.Log($"Count of file: {allFiles.Count()}");
            foreach (var item in allFiles)
            {
                var task = new Task<Document>(() =>
                {
                    var doc = KEAGlobal.KEAManager.GetDocument(item.FullName);
                    KEAGlobal.Logger.Log($"Readed file: {item.FullName}");
                    return doc;
                });
                task.Start();
                tasks.Add(task);
            }

            return tasks.ToArray();
        }

        static void Splitwords()
        {
            var AllWords = JsonConvert.DeserializeObject<List<Model.DataSet.Json.Word>>(File.ReadAllText("DataSet\\Json\\dataset.json"));

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

        static void AddBooks()
        {
            var sqlContext = new SqlServerContext();
            var Files = new DirectoryInfo(@"C:\Users\Owner\Desktop\master\MAQOLA2\IDF");
            var books = sqlContext.BookDataSets.ToList();
            foreach (var item in Files.GetFiles())
            {
                if (books.Any(a => a.Name == item.Name))
                    continue;
                PDFHelper pDFHelper = new PDFHelper();
                var text = pDFHelper.ReadPdfFile(item.FullName);
                sqlContext.BookDataSets.Add(new BookDataSet { Guid = Guid.NewGuid(), Name = item.Name, Content = text });
                sqlContext.SaveChanges();
            }
        }

        static void AddFromSqlite()
        {
            WordDbSqliteContext wordDbContext = new WordDbSqliteContext();
            var allwords = wordDbContext.word.Where(s => s.dictionary_id == "2").ToList().GroupBy(g => g.word).Select(s => s.FirstOrDefault());
            SqlServerContext sqlServerContext = new SqlServerContext();
            foreach (var word in allwords)
            {
                WordDataSet wordDataSet = new WordDataSet() { Guid = Guid.NewGuid() };
                wordDataSet.Content = word.word;
                wordDataSet.ContentInfo = word.article;
                sqlServerContext.Add(wordDataSet);
            }
            sqlServerContext.SaveChanges();
        }

        static SqlServerContext ConvertFromJsonToSQLServer()
        {
            SqlServerContext sqlServerContext = new SqlServerContext();
            JsonContext jsonContext = new JsonContext();

            var peshoyands = jsonContext.Peshoyands;
            foreach (var item in peshoyands)
            {
                sqlServerContext.Add(new PeshoyandDataSet() { Guid = item.Guid, ContentInfo = item.ContentInfo, Content = item.Content });
            }

            var jonishins = jsonContext.Jonishins;
            foreach (var item in jonishins)
            {
                sqlServerContext.Add(new JonishinDataSet() { Guid = item.Guid, ContentInfo = item.ContentInfo, Content = item.Content });
            }

            var bandaks = jsonContext.Bandaks;
            foreach (var item in bandaks)
            {
                sqlServerContext.Add(new BandakDataSet() { Guid = item.Guid, ContentInfo = item.ContentInfo, Content = item.Content });
            }
            sqlServerContext.SaveChanges();


            int count = 0;
            var allwords = jsonContext.Words;
            foreach (var item in allwords)
            {
                if (count > 300)
                {
                    sqlServerContext.SaveChanges();
                    count = 0;
                }
                sqlServerContext.Add(new WordDataSet() { Guid = item.Guid, ContentInfo = item.ContentInfo, Content = item.Content });
                count++;
            }
            sqlServerContext.SaveChanges();
            return sqlServerContext;
        }
    }
}
