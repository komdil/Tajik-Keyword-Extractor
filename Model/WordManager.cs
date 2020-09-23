using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Model
{
    public class WordManager
    {
        public IEnumerable<Document> ReadAllDocuments()
        {
            List<Document> documents = new List<Document>();

            var files = Directory.GetFiles("Data");

            foreach (var file in files)
            {
                var name = file.Split('\\').Last();
                var content = File.ReadAllText(file).ToLower();
                documents.Add(new Document { Name = name, Content = content });
            }

            return documents;
        }
    }
}
