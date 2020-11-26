using System;
using System.Windows.Forms;

namespace StopWordMaker
{
    public partial class Form1 : Form
    {
        public const string StopWordsPath = @"..\..\..\Model\DataSet\Json\StopWords.json";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
