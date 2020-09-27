using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Model.KEA
{
    public static class WordManagerDocumentExtensions
    {
        public static IEnumerable<Document> ReadAllDocuments(this WordManager wordManager)
        {
            List<Document> documents = new List<Document>();

            var files = Directory.GetFiles("Data");

            foreach (var file in files)
            {
                var name = file.Split('\\').Last();
                var content = File.ReadAllText(file).ToLower();
                documents.Add(new Document(wordManager, content) { Name = name, });
            }

            return documents;
        }

        public static IEnumerable<Sentence> SplitSentencesFromDoc(this WordManager wordManager, Document document)
        {
            List<Sentence> sentenceInstanses = new List<Sentence>();
            var sentences = Regex.Split(document.Content, Statics.SplitSentensePattern).Where(a => a != "");
            foreach (var sentense in sentences)
            {
                sentenceInstanses.Add(new Sentence(wordManager, sentense));
            }

            return sentenceInstanses;
        }
    }
}
