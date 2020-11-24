using Model.DataSet.Json;
using Model.DataSet.SqlServer;
using Model.KEA;
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
            var readBook = @"C:\Users\Owner\Desktop\master\MAQOLA2\IDF\khim.pdf";
            PDFHelper pDFHelper = new PDFHelper();
            var text = pDFHelper.ReadPdfFile(readBook);
            //var sqlContext = new SqlServerContext();
            //var book = sqlContext.BookDataSets.FirstOrDefault(s => s.Name == "11_FIZIKA.pdf");
            //book.Content = book.Content.Replace("Ч,", "Ҷ");
            //book.Content = book.Content.Replace("Ч,", "Ҷ");
            //sqlContext.SaveChanges();
        }

        static void Dpe()
        {
            var sqlContext = new SqlServerContext();
            var txtContent = sqlContext.BookDataSets.FirstOrDefault(s => s.Name == "5_TXT.pdf");
            var doc = new Document(txtContent.Content);
            doc.SplitSentenses();
            foreach (var item in doc.Sentences)
            {
                item.SplitWords();
                item.NormalizeWords(sqlContext);
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
                wordDataSet.WordValue = word.word;
                wordDataSet.Info = word.article;
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
                sqlServerContext.PeshoyandDataSets.Add(new PeshoyandDataSet() { Guid = item.Guid, Info = item.Info, Value = item.WordValue });
            }

            var jonishins = jsonContext.Words.Where(a => a.IsJonishin());
            foreach (var item in jonishins)
            {
                sqlServerContext.JonishinDataSets.Add(new JonishinDataSet() { Guid = item.Guid, Info = item.Info, Value = item.WordValue });
            }

            var bandaks = jsonContext.Words.Where(a => a.IsBandak());
            foreach (var item in bandaks)
            {
                sqlServerContext.BandakDataSets.Add(new BandakDataSet() { Guid = item.Guid, Info = item.Info, Value = item.WordValue });
            }
            sqlServerContext.SaveChanges();


            var allwords = jsonContext.Words.Where(a => !a.IsBandak() && !a.IsJonishin() && !a.IsPeshoyand());
            foreach (var item in allwords)
            {
                sqlServerContext.WordsDataSet.Add(new WordDataSet() { Guid = item.Guid, Info = item.Info, WordValue = item.WordValue });
            }
            sqlServerContext.SaveChanges();


            return sqlServerContext;
        }
    }
}
