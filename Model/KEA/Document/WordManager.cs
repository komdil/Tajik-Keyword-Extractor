using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Model.KEA
{
    public static class WordManagerExtensions
    {
        public static IEnumerable<Document> ReadAllDocuments(this WordManager wordManager)
        {
            List<Document> documents = new List<Document>();

            var files = Directory.GetFiles("Data");

            foreach (var file in files)
            {
                var name = file.Split('\\').Last();
                var content = File.ReadAllText(file).ToLower();
                documents.Add(new Document(wordManager) { Name = name, Content = content });
            }

            return documents;
        }
    }
}
