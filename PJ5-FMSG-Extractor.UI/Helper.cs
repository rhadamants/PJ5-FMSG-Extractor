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

            try
            {
                string file = $"{path.DirectoryName}\\extracted\\{fileName}";
                if (check) return File.Exists(file) ? file : throw new FileNotFoundException();
                return file;
            }
            catch (FileNotFoundException)
            {
                _ = MessageBox.Show($"File: {fileName}\r\nNot found in the extracted directory!");
                return null;
            }
        }
        public static string GetImportedFile(FileInfo file)
        {
            string importedPath = $"{file.DirectoryName}\\imported";
            if (!Directory.Exists(importedPath)) _ = Directory.CreateDirectory(importedPath);

            return $"{file.DirectoryName}\\imported\\{file.Name}";
        }
    }
}
