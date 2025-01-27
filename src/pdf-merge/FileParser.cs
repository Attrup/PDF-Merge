using System;
using System.IO;

namespace pdf_merge;

public class FileParser {
    public static void FilesFromDirectory(string dirPath, ref List<string> filePaths) {
        // Check that the provided path is a valid path to a directory
        if (!Directory.Exists(dirPath)) {
            throw new IOException($"Directory path does not exist\nPath: '{dirPath}'");
        }

        // Get all files in the directory, sort them and copy only the PDF file paths
        string[] fileEntries = Directory.GetFiles(dirPath);
        Array.Sort(fileEntries);

        foreach (string file in fileEntries) {
            if (Path.GetExtension(file).Equals(".pdf")) {
                filePaths.Add(file);
            }
        }
    }

    public static void FilesFromList(IEnumerable<string> files, ref List<string> filePaths) {
        foreach (string file in files) {
            // Check that the file is PDF and exists
            if (!Path.GetExtension(file).Equals(".pdf")) {
                throw new IOException($"'{Path.GetFileName(file)}' is not a PDF file");
            }

            if (!File.Exists(file)) {
                throw new IOException($"Cannot find file: '{Path.GetFileName(file)}'");
            }

            // Save file path
            filePaths.Add(file);
        }
    }

}
