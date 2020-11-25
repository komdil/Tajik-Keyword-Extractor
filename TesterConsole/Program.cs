using Model.DataSet.Json;
using Model.DataSet.SqlServer;
using Model.KEA;
using Model.KEA.Document;
using Model.KEADataSet.Sqlite;
using System;
using System.IO;
using System.Linq;
using System.Text;
namespace TesterConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            JsonContext jsonContext = new JsonContext();
            KEAGlobal.InitiateKEAGlobal(jsonContext);
            var myTestText = "Имрӯз ҳаво гарм аст. Дирӯз ҳаво ин қадар гарм сахт гарм набуд";
            var words = KEAGlobal.KEAManager.GetSimpleKeywords(myTestText);
        }

        private static void Logger_OnLog(string log)
        {
            using (StreamWriter wr = new StreamWriter("unknown.txt", true))
            {
                wr.WriteLine(log);
            }
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

            var peshoyands = jsonContext.Words.Where(a => a.IsPeshoyand());
            foreach (var item in peshoyands)
            {
                sqlServerContext.Add(new PeshoyandDataSet() { Guid = item.Guid, ContentInfo = item.ContentInfo, Content = item.Content });
            }

            var jonishins = jsonContext.Words.Where(a => a.IsJonishin());
            foreach (var item in jonishins)
            {
                sqlServerContext.Add(new JonishinDataSet() { Guid = item.Guid, ContentInfo = item.ContentInfo, Content = item.Content });
            }

            var bandaks = jsonContext.Words.Where(a => a.IsBandak());
            foreach (var item in bandaks)
            {
                sqlServerContext.Add(new BandakDataSet() { Guid = item.Guid, ContentInfo = item.ContentInfo, Content = item.Content });
            }
            sqlServerContext.SaveChanges();


            int count = 0;
            var allwords = jsonContext.Words.Where(a => !a.IsBandak() && !a.IsJonishin() && !a.IsPeshoyand());
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
