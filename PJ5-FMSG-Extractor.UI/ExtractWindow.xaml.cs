using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PJ5_FMSG_Extractor.UI
{
    /// <summary>
    /// Lógica interna para ExtractWindow.xaml
    /// </summary>
    public partial class ExtractWindow : Window
    {
        public ExtractWindow()
        {
            InitializeComponent();
        }
        private void BtnExtract_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FileInfo input = new(Helper.FileDialog("Fatal Frame 5 FMSG Text", "*.fmsg"));
                if (input.Exists)
                {
                    string textFile = Helper.GetTextFile(input);
                    string importedFile = Helper.GetImportedFile(input);

                    FMSG.Extract(input.FullName, textFile);
                }
            }
            catch (ArgumentException) { }
        }
        private void BtnExtractAll_Click(object sender, RoutedEventArgs e)
        {
            DirectoryInfo originalFolder = new(Helper.FolderDialog());

            if (originalFolder is null) return;

            FileInfo[] files = originalFolder.GetFiles("*.fmsg");

            int count = 0;
            foreach (var file in files.Where(x => !x.Name.StartsWith("RTP_M10A", StringComparison.InvariantCulture)))
            {
                FMSG.Extract(file.FullName, Helper.GetTextFile(file));
                ++count;
            }
            _ = MessageBox.Show($"Número de arquivos: {files.Length}{Environment.NewLine}Arquivos convertidos: {count}");
        }
    }
}
