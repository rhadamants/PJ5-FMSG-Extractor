using System.Windows;

namespace PJ5_FMSG_Extractor.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnExtract_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            ExtractWindow extractW = new();
            extractW.Owner = this;
            _ = extractW.ShowDialog();
            Show();
        }

        private void BtnInsert_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            InsertWindow insertW = new();
            insertW.Owner = this;
            _ = insertW.ShowDialog();
            Show();
        }
    }
}
