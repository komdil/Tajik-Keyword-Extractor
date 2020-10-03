using Model.DataSet.SqlServer;
using Model.KEA;
using Model.KEADataSet;
using Model.KEADataSet.Sqlite;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;

namespace TesterConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            //var wordManager = new WordManager();
            //var name = "Payom_MajlisiOli_20_01_2016.txt";
            //var content = File.ReadAllText("TestData\\" + name);
            //var document = new Document(wordManager, content) { Name = name };
            //document.SplitSentenses();
            //document.Sentences.ToList().ForEach(s => s.SplitWords());
            //File.WriteAllLines("sentences.txt", document.Sentences.Select(a => a.Content).ToArray());
            //var allwords = document.Sentences.SelectMany(a => a.Words).GroupBy(a => a.Value).Select(a => a.FirstOrDefault().Value.ToLower());
            //File.WriteAllLines("words.txt", allwords.ToArray());

            SqlServerContext sqlServerContext = new SqlServerContext();
            var words = sqlServerContext.Words.ToList();
            var json = JsonConvert.SerializeObject(words, Formatting.Indented);
            File.WriteAllText("dataset.json", json);
        }

        static void AddFromSqlite()
        {
            //WordDbSqliteContext wordDbContext = new WordDbSqliteContext();
            //var allwords = wordDbContext.word.Where(s => s.dictionary_id == "2").ToList().GroupBy(g => g.word).Select(s => s.FirstOrDefault());
            //SqlServerContext sqlServerContext = new SqlServerContext();
            //foreach (var word in allwords)
            //{
            //    WordDataSet wordDataSet = new WordDataSet() { Guid = Guid.NewGuid() };
            //    wordDataSet.WordValue = word.word;
            //    wordDataSet.Info = word.article;
            //    sqlServerContext.Add(wordDataSet);
            //}
            //sqlServerContext.SaveChanges();
        }
    }
}
