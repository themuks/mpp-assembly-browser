using AssemblyBrowser.ViewModels;

namespace AssemblyBrowser.Views.Windows
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ApplicationViewModel();
        }
    }
}