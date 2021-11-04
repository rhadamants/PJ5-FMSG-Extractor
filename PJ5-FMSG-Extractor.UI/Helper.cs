using Microsoft.WindowsAPICodePack.Dialogs;
using System.IO;
using System.Windows;

namespace PJ5_FMSG_Extractor.UI
{
    static class Helper
    {
        public static string FolderDialog()
        {
            using (var dialog = new CommonOpenFileDialog { IsFolderPicker = true })
            {
                if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    return dialog.FileName;
                }
            }
            return null;
        }

        public static string FileDialog(string filter, string extension)
        {
            using (var dialog = new CommonOpenFileDialog
            {
                IsFolderPicker = false,
                EnsureFileExists = true
            })
            {
                dialog.Filters.Add(new CommonFileDialogFilter(filter, extension));

                if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    return dialog.FileName;
                }
            }

            return null;
        }

        public static string GetTextFile(FileInfo path, bool check = false)
        {
            string fileName = Path.ChangeExtension(path.Name, ".txt");
            string extractedPath = Path.Combine(path.DirectoryName, "extracted");

            try
            {
                string file = Path.Combine(extractedPath, fileName);
                if (!Directory.Exists(extractedPath)) _ = Directory.CreateDirectory(extractedPath);

                return check ? File.Exists(file) ? file : throw new FileNotFoundException() : file;
            }
            catch (FileNotFoundException)
            {
                _ = MessageBox.Show($"File: {fileName}\r\nNot found in the extracted directory!");
                return null;
            }
        }
        public static string GetImportedFile(FileInfo file)
        {
            string importedPath = Path.Combine(file.DirectoryName, "imported"); ;
            if (!Directory.Exists(importedPath)) _ = Directory.CreateDirectory(importedPath);

            return Path.Combine(importedPath, file.Name);
        }
    }
}
