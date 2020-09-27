using Model.KEA;
using System;
using System.IO;
using System.Linq;

namespace TesterConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var wordManager = new WordManager();
            var name = "Payom_MajlisiOli_20_01_2016.txt";
            var content = File.ReadAllText("TestData\\" + name);
            var document = new Document(wordManager, content) { Name = name };
            document.SplitSentenses();
            document.Sentences.ToList().ForEach(s => s.SplitWords());
        }
    }
}
