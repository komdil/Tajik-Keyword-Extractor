using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TajikKEA;
using TajikKEA.Document;
using TajikKEAJsonContext;

namespace TesterConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //TajikKEAJsonContext.TajikKEAJsonContext jsonContext = new TajikKEAJsonContext.TajikKEAJsonContext();
            //KEAGlobal.Logger.OnLog += Logger_OnLog;
            //KEAGlobal.InitiateKEAGlobal(jsonContext);
            //var file = new DirectoryInfo(@"C:/Users/komdil/Desktop/e").GetFiles().FirstOrDefault();
            //var results = KEAGlobal.KEAManager.GetTFIDFFromFile(file);
            //var c = results.Value.OrderByDescending(s => s.TF_IDF).ToList().Take(30).ToList();
            //MSExcelHelper.ExtractResult("result2.csv", c);

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
