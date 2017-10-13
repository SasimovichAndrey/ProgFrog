using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using ProgFrog.WpfApp.ViewModel;
using AurelienRibon.Ui.SyntaxHighlightBox;
using ProgFrog.Interface.Model;

namespace ProgFrog.WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DoTasksViewModel ViewModel;

        private Dictionary<ProgrammingLanguage, IHighlighter> _highlightersMappings = new Dictionary<ProgrammingLanguage, IHighlighter>();

        public MainWindow()
        {
            InitializeComponent();

            Initialize();
        }

        private void Initialize()
        {
            ProgFrog.IoC.DependencyReolver.Configure();

            _highlightersMappings[ProgrammingLanguage.CSharp] = HighlighterManager.Instance.Highlighters["CSharp"];
            _highlightersMappings[ProgrammingLanguage.Python] = HighlighterManager.Instance.Highlighters["Python"];

            var vm = ProgFrog.IoC.DependencyReolver.Resolve<DoTasksViewModel>();

            this.DataContext = vm;
            this.ViewModel = vm;
            this.Loaded += vm.Initialize;
        }

        public void ProgLangList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var curLang = (ProgrammingLanguage)((ListBox)sender).SelectedItem;

            if (_highlightersMappings.ContainsKey(curLang))
            {
                codeBox.CurrentHighlighter = _highlightersMappings[curLang];
            }
            else
            {
                codeBox.CurrentHighlighter = null;
            }
        }

        public async void Button_Click(object sender, RoutedEventArgs e)
        {
            await ViewModel.RunSelectedTask();
        }
    }
}
