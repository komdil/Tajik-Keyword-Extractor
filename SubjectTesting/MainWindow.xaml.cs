using Model.DataSet.SqlServer;
using Model.KEA;
using Model.KEA.Document;
using Model.KEA.TFIDF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
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

namespace SubjectTesting
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public void Notify(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        List<ResultWord> words = new List<ResultWord>();
        public List<ResultWord> Words { get { return words; } set { words = value; Notify("Words"); } }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private ICommand start;
        public ICommand Start
        {
            get
            {
                if (start == null)
                {
                    start = new RelayCommand(param => StartMethod());
                }
                return start;
            }
        }

        void StartMethod()
        {
            SqlServerContext sqlServerContext = new SqlServerContext();
            var himiyaContent = sqlServerContext.BookDataSets.FirstOrDefault(s => s.Name == "5_TXT.pdf");
            var allDocuments = GetDocuments(sqlServerContext);
            var document = new Document(himiyaContent.Content);
        }

        IEnumerable<Document> GetDocuments(SqlServerContext sqlServerContext)
        {
            List<Document> documents = new List<Document>();
            foreach (var item in sqlServerContext.BookDataSets.ToList())
            {
                var document = new Document(item.Content);
                foreach (var sen in document.Sentences)
                {
                    sen.NormalizeWords();
                }
                documents.Add(document);
            }

            return documents;
        }
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
