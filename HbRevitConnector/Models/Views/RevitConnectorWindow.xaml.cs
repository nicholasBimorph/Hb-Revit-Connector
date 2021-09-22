using System.Reflection;
using System.Windows;
using System.Windows.Input;
using HbRevitConnector.ViewModel;


namespace HbRevitConnector.Models.Views
{
    /// <summary>
    /// Interaction logic for RevitConnectorWindow.xaml
    /// </summary>
    public partial class RevitConnectorWindow : Window
    {
        public RevitConnectorWindow(RevitConnectorViewModel viewModel)
        {
            this.LoadAssembly();

            InitializeComponent();

            this.DataContext = viewModel;
        }

        /// <summary>
        /// Loads the necessary DLLs that are required to run the software.
        /// </summary>
        public void LoadAssembly()
        {
            Assembly.LoadFrom(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                "MaterialDesignThemes.Wpf.dll"));
        }

        private void MainWindow_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Q)
                this.Close();
        }

        private void MainWindow_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
