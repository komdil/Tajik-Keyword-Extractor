using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Model.KEA
{
    public static class WordManagerDocumentExtensions
    {
        public static IEnumerable<Document> ReadAllDocuments()
        {
            List<Document> documents = new List<Document>();

            var files = Directory.GetFiles("Data");

            foreach (var file in files)
            {
                var name = file.Split('\\').Last();
                var content = File.ReadAllText(file).ToLower();
                var doc = new Document(content) { Name = name, };
                doc.SplitSentenses();
                doc.Sentences.ToList().ForEach(a => a.SplitWords());
                documents.Add(doc);
            }
            return documents;
        }

        public static IEnumerable<Sentence> SplitSentencesFromDoc(Document document)
        {
            List<Sentence> sentenceInstanses = new List<Sentence>();
            var sentences = Regex.Split(document.Content, Statics.SplitSentensePattern).Where(a => a != "");
            foreach (var sentense in sentences)
            {
                sentenceInstanses.Add(new Sentence(sentense));
            }

            return sentenceInstanses;
        }
    }
}
