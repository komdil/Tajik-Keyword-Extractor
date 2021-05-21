using TajikKEAJsonContext;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using TajikKEA;
using System.Threading.Tasks;

namespace WPFClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            TajikKEAContext jsonContext = new TajikKEAContext();
            KEAGlobal.InitiateKEAGlobal(jsonContext);
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
            int keywordCount;
            int.TryParse(CountOfKeywords.Text, out keywordCount);
            if (keywordCount == 0)
                keywordCount = 10;

            Task.Run(new Action(() =>
            {
                CalculateButton.Dispatcher.Invoke(new Action(() =>
                {
                    CalculateButton.Content = "Ҳисоб...";
                    CalculateButton.IsEnabled = false;
                }));
                Words = KEAGlobal.KEAManager.GetKeywords(InputText, keywordCount);
                CalculateButton.Dispatcher.Invoke(new Action(() =>
                {
                    CalculateButton.Content = "Ҳисоб";
                    CalculateButton.IsEnabled = true;
                }));
            }));
        }

        public string InputText { get; set; }

        List<string> words = new List<string>();
        public List<string> Words { get { return words; } set { words = value; Notify("Words"); } }
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
