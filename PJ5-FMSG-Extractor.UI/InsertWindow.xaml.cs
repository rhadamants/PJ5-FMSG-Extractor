using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
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
            _ = MessageBox.Show("Aponte para o diretório com os arquivos .fmsg!");
            var sb = new StringBuilder();
            var time = new Stopwatch(); ;
            try
            {
                DirectoryInfo originalFolder = new(Helper.FolderDialog("Diretório com arquivos FMSG e com a pasta extracted."));

                if (originalFolder is null) return;

                time.Start();

                FileInfo[] files = originalFolder.GetFiles("*.fmsg");
                if (files.Length == 0)
                {
                    _ = MessageBox.Show("Nenhum arquivo FMSG encontrado!");
                    return;
                }

                int count = 0;
                int minus = 0;
                foreach (FileInfo file in files.Where(x => !x.Name.StartsWith("RTP_M10A", StringComparison.InvariantCulture)))
                {
                    string textFile = Helper.GetTextFile(file, true);
                    if (textFile != null)
                    {
                        FMSG.Import(file.FullName, textFile, Helper.GetImportedFile(file));
                        count++;
                    }
                    else
                    {
                        minus++;
                        _ = sb.AppendLine(file.Name);
                    }
                }
                time.Stop();
                _ = MessageBox.Show($"Número de arquivos: {files.Length}{Environment.NewLine}Arquivos convertidos: {count}{Environment.NewLine}Arquivos não convertidos: {minus}\n\n{sb}\n\nTempo decorrido: {time.Elapsed.TotalSeconds}");
            }
            catch (ArgumentException) { }
        }
    }
}
