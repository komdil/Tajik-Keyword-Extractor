using Model;
using Model.KEA;
using Model.KEA.Document;
using Model.KEA.TFIDF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private ICommand calculate;

        public event PropertyChangedEventHandler PropertyChanged;

        public void Notify(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public ICommand Calculate
        {
            get
            {
                if (calculate == null)
                {
                    calculate = new RelayCommand(param => CalculateMethod());
                }
                return calculate;
            }
        }

        void CalculateMethod()
        {
            if (InputText == "")
                return;

            List<string> fixedInput = GetListFromText(InputText.ToLower());
            var document = new Document(InputText.ToLower()) { Name = "TestDoc" };
            var docs = new List<Document>();

            foreach (var word in document.Sentences.SelectMany(a => a.Words))
            {
                TF tf = new TF(word, document);
                IDF idf = new IDF(docs, word);
                TF_IDF tF_IDF = new TF_IDF(tf.CalculateTF(), idf.CalculateIDF());
                tF_IDF.CalculateTF_IDF();
                // Words.Add(new ResultWord { Word = word.Value, IDF = idf.IDFValue, TF = tf.TFValue, TF_IDF = tF_IDF.TF_IDFValue });
            }
            Words = Words.Where(a => a.TF != 0).ToList();
        }

        List<string> GetListFromText(string inputText)
        {
            List<string> vs = new List<string>();
            foreach (var item in inputText.Split(' '))
            {
                if (item != "," && item != "" && item != " " && item != "-" && !int.TryParse(item, out int res))
                {
                    vs.Add(item.Replace("!", "").Replace(".", "").Replace(",", ""));
                }
            }

            return vs.GroupBy(a => a).Select(f => f.FirstOrDefault()).ToList();
        }

        public string InputText { get; set; }

        List<ResultWord> words = new List<ResultWord>();
        public List<ResultWord> Words { get { return words; } set { words = value; Notify("Words"); } }
    }

    public class ResultWord
    {
        public string Word { get; set; }
        public double TF { get; set; }
        public double IDF { get; set; }
        public double TF_IDF { get; set; }
    }

    public class RelayCommand : ICommand
    {
        #region Fields

        readonly Action<object> _execute;
        readonly Predicate<object> _canExecute;

        #endregion // Fields

        #region Constructors

        /// <summary>
        /// Creates a new command that can always execute.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        public RelayCommand(Action<object> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// Creates a new command.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            _execute = execute;
            _canExecute = canExecute;
        }

        #endregion // Constructors

        #region ICommand Members

        public bool CanExecute(object parameters)
        {
            return _canExecute == null ? true : _canExecute(parameters);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameters)
        {
            _execute(parameters);
        }

        #endregion // ICommand Members
    }
}
