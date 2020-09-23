using Model;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace TesterConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            WordManager wordManager = new WordManager();

            var name = "Payom_MajlisiOli_20_01_2016.txt";
            var content = File.ReadAllText("Data\\" + name);
            var document = new Document { Name = name, Content = content };

            var fixedInput = Regex.Replace(content, "[^a-zA-Z0-9% ._]", string.Empty);

            var docs = wordManager.ReadAllDocuments();
        }
    }
}
