using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TajikKEA;
using TajikKEAJsonContext;

namespace WordTool
{
    public partial class Form1 : Form
    {
        public const string StopWordsPath = @"..\..\..\Model\DataSet\Json\StopWords.json";
        public const string WordsPath = @"..\..\..\Model\DataSet\Json\Words.json";
        public const string ReplaceMent = @"..\..\..\Model\DataSet\Json\ReplaceMent.json";

        public List<string> NewStopWords { get; set; } = new List<string>();
        public List<TajikJsonStopWord> StopWords { get; set; }
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tabControl1.Width = this.Width;
            tabControl1.Height = this.Height;
            StopWords = GetExistingStopWords();
            foreach (var item in StopWords)
            {
                listBox2.Items.Add(item);
            }
        }

        List<TajikJsonStopWord> GetExistingStopWords()
        {
            try
            {
                var json = File.ReadAllText(StopWordsPath);
                var res = JsonConvert.DeserializeObject<List<TajikJsonStopWord>>(json);
                if (res == null)
                    res = new List<TajikJsonStopWord>();
                return res;
            }
            catch
            {
                return new List<TajikJsonStopWord>();
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
                    var stopWornew = new TajikJsonStopWord { Guid = Guid.NewGuid(), Content = item, ContentInfo = $"СТОП-КАЛИМА: {item}" };
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
                    var itemToRemove = listBox2.SelectedItem as TajikJsonStopWord;
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
                var text = JsonConvert.SerializeObject(StopWords, Formatting.Indented);
                File.WriteAllText(StopWordsPath, text);
                MessageBox.Show("Сабт карда шуд!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Хатогӣ! " + ex.Message);
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                var newWord = textBox2.Text.Trim().ToLower();
                var info = textBox1.Text.Trim().ToLower();
                var textJson = File.ReadAllText(WordsPath);
                var list = JsonConvert.DeserializeObject<List<TajikJsonWord>>(textJson);
                list.Add(new TajikJsonWord()
                {
                    Guid = Guid.NewGuid(),
                    Content = newWord,
                    ContentInfo = info
                });
                var newText = JsonConvert.SerializeObject(list, Formatting.Indented);
                File.WriteAllText(WordsPath, newText);
                MessageBox.Show("Ҳамроҳ карда шуд!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Хатогӣ " + ex.Message);
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                var textFrom = textBox3.Text.ToLower();
                var textTo = textBox4.Text.ToLower();
                var json = File.ReadAllText(ReplaceMent);
                var replaceMent = JsonConvert.DeserializeObject<List<ReplaceMent>>(json);
                replaceMent.Add(new ReplaceMent { ReplaceFrom = textFrom.Trim(), ReplaceTo = textTo.Trim() });
                json = JsonConvert.SerializeObject(replaceMent, Formatting.Indented);
                File.WriteAllText(ReplaceMent, json);
                MessageBox.Show("Ҳамроҳ карда шуд");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Хатогӣ дар вақти ҳамроҳкунӣ\n" + ex.Message);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            var newCategory = textBox5.Text;

        }
    }
}
