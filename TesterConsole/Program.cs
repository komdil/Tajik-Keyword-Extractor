using Model.KEA;
using System;
using System.IO;

namespace TesterConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var wordManager = new WordManager();
            var name = "Payom_MajlisiOli_20_01_2016.txt";
            var content = File.ReadAllText("Data\\" + name);
            var document = new Document(wordManager) { Name = name, Content = content };
        }
    }
}
