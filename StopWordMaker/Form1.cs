using Model.DataSet.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace StopWordMaker
{
    public partial class Form1 : Form
    {
        public const string StopWordsPath = @"..\..\..\Model\DataSet\Json\StopWords.json";

        public List<string> NewStopWords { get; set; } = new List<string>();
        public List<JsonStopWord> StopWords { get; set; }
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            StopWords = GetExistingStopWords();
            foreach (var item in StopWords)
            {
                listBox2.Items.Add(item);
            }
        }

        List<JsonStopWord> GetExistingStopWords()
        {
            try
            {
                var json = File.ReadAllText(StopWordsPath);
                var res = JsonConvert.DeserializeObject<List<JsonStopWord>>(json);
                if (res == null)
                    res = new List<JsonStopWord>();
                return res;
            }
            catch
            {
                return new List<JsonStopWord>();
            }
        }

        public static string ShowDialog(string text, string caption)
        {
            Form prompt = new Form();
            prompt.Width = 500;
            prompt.Height = 150;
            prompt.Text = caption;
            Label textLabel = new Label() { Left = 50, Top = 20, Text = text };
            var inputBox = new TextBox() { Left = 50, Top = 50, Width = 400 };
            Button confirmation = new Button() { Text = "Ok", Left = 350, Width = 100, Top = 75 };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(inputBox);
            var res = prompt.ShowDialog();
            return inputBox.Text;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var newStopWord = ShowDialog("СТОП-КАЛИМА", "Ҳамроҳ кардани калимаи нав").Trim();
            if (newStopWord != "" && newStopWord != null)
            {
                if (!NewStopWords.Any(s => s == newStopWord))
                {
                    NewStopWords.Add(newStopWord);
                    if (!listBox1.Items.Contains(newStopWord))
                    {
                        listBox1.Items.Add(newStopWord);
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBox1.SelectedItem != null)
                {
                    var itemToRemove = listBox1.SelectedItem.ToString();
                    NewStopWords.Remove(itemToRemove);
                    listBox1.Items.Remove(itemToRemove);
                }
            }
            catch
            {
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            OpenFileDialog theDialog = new OpenFileDialog();
            theDialog.Title = "Файлро интихоб кунед";
            theDialog.Filter = "TXT files|*.txt";
            theDialog.InitialDirectory = @"C:\";
            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var path = theDialog.FileName;
                    var lines = File.ReadAllLines(path);
                    foreach (var item in lines)
                    {
                        if (!NewStopWords.Any(s => s == item))
                        {
                            NewStopWords.Add(item);
                            if (!listBox1.Items.Contains(item))
                            {
                                listBox1.Items.Add(item);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Хатогӣ ҳангоми хондани файл: " + ex.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (var item in NewStopWords)
            {
                if (!StopWords.Any(s => s.Content == item))
                {
                    var stopWornew = new JsonStopWord { Guid = Guid.NewGuid(), Content = item, ContentInfo = $"СТОП-КАЛИМА: {item}" };
                    StopWords.Add(stopWornew);
                    listBox2.Items.Add(stopWornew);
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBox2.SelectedItem != null)
                {
                    var itemToRemove = listBox2.SelectedItem as JsonStopWord;
                    StopWords.Remove(itemToRemove);
                    listBox2.Items.Remove(itemToRemove);
                }
            }
            catch
            {
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var text = JsonConvert.SerializeObject(StopWords);
                File.WriteAllText(StopWordsPath, text);
                MessageBox.Show("Сабт карда шуд!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Хатогӣ! " + ex.Message);
            }

        }
    }
}
