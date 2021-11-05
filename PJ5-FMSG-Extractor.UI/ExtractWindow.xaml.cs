using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
            var sb = new StringBuilder();
            var time = new Stopwatch(); ;

            try
            {
                DirectoryInfo originalFolder = new(Helper.FolderDialog("Diretório com arquivos FMSG"));

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
                foreach (var file in files.Where(x => !x.Name.StartsWith("RTP_M10A", StringComparison.InvariantCulture)))
                {
                    string textFile = Helper.GetTextFile(file);
                    FMSG.Extract(file.FullName, textFile);
                    //Thread.Sleep(1000);
                    if (File.Exists(textFile))
                    {
                        count++;
                    }
                    else
                    {
                        minus++;
                        _ = sb.AppendLine(Path.GetFileName(textFile));
                    }
                }
                time.Stop();
                _ = MessageBox.Show($"Número de arquivos: {files.Length}{Environment.NewLine}Arquivos convertidos: {count}{Environment.NewLine}Arquivos não convertidos: {minus}\n\n{sb}\n\nTempo decorrido: {time.Elapsed.TotalSeconds}");
            }
            catch (ArgumentException) { }
        }
    }
}
