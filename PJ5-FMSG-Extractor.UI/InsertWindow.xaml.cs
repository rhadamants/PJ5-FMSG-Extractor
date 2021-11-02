using System;
using System.IO;
using System.Linq;
using System.Windows;

namespace PJ5_FMSG_Extractor.UI
{
    /// <summary>
    /// Lógica interna para InsertWindow.xaml
    /// </summary>
    public partial class InsertWindow : Window
    {
        public InsertWindow()
        {
            InitializeComponent();
        }

        private void BtnInsert_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FileInfo original = new(Helper.FileDialog("Fatal Frame 5 FMSG Text", "*.fmsg"));
                if (original.Exists)
                {
                    FMSG.Import(original.FullName, Helper.GetTextFile(original), Helper.GetImportedFile(original));
                }
            }
            catch (ArgumentException) { }
        }
        private void BtnInsertAll_Click(object sender, RoutedEventArgs e)
        {
            DirectoryInfo originalFolder = new(Helper.FolderDialog());

            if (originalFolder is null) return;

            var files = originalFolder.GetFiles("*.fmsg");

            int count = 0;
            foreach (FileInfo file in files.Where(x => !x.Name.StartsWith("RTP_M10A", StringComparison.InvariantCulture)))
            {
                FMSG.Import(file.FullName, Helper.GetTextFile(file, true), Helper.GetImportedFile(file));
                ++count;
            }
            _ = MessageBox.Show($"Número de arquivos: {files.Length}{Environment.NewLine}Arquivos convertidos: {count}");
        }
    }
}
